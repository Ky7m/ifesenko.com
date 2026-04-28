targetScope = 'subscription'

@description('Name of the resource group that will contain the Static Web App.')
param resourceGroupName string = 'ifesenko-com'

@description('Azure region for the resource group.')
param location string = 'centralus'

@description('Azure region for the Static Web App. Must be one of the regions that support Microsoft.Web/staticSites.')
@allowed([
  'westus2'
  'centralus'
  'eastus2'
  'westeurope'
  'eastasia'
])
param staticWebAppLocation string = 'centralus'

@description('Name of the Static Web App resource.')
param staticWebAppName string = 'swa-ifesenko'

@description('SKU tier for the Static Web App.')
@allowed([
  'Free'
  'Standard'
])
param sku string = 'Standard'

@description('Custom domains to bind to the Static Web App (e.g. ifesenko.com, www.ifesenko.com). DNS records must be configured out-of-band.')
param customDomains array = []

@description('Whether to provision an Azure DNS zone for the domain. When true, domainName must be set.')
param deployDnsZone bool = false

@description('Domain name to host in Azure DNS (e.g. ifesenko.com). Only used when deployDnsZone is true.')
param domainName string = ''

@description('Whether to provision a user-assigned managed identity for GitHub Actions OIDC login.')
param deployManagedIdentity bool = true

@description('Whether to create a subscription-scoped Contributor role assignment for the managed identity. Requires the deploying principal to have Owner or User Access Administrator role. Set to false when the deploying principal only has Contributor permissions and handle the role assignment out-of-band.')
param deployRoleAssignment bool = true

@description('Name of the user-assigned managed identity used by GitHub Actions.')
param managedIdentityName string = 'id-ifesenko-github'

@description('GitHub repository ("owner/repo") that will federate with the managed identity.')
param githubRepository string = 'Ky7m/ifesenko.com'

@description('Federated identity credentials to create on the managed identity. See modules/identity.bicep for the expected shape.')
param federatedCredentials array = [
  {
    name: 'github-env-infra'
    subject: 'environment:infra'
  }
]

@description('Tags applied to all resources created by this deployment.')
param tags object = {
  app: 'ifesenko.com'
  managedBy: 'bicep'
}

// Built-in "Contributor" role. Needed so the UAMI can run subscription-scoped
// Bicep deployments (creating resource groups, SWA, etc.) from GitHub Actions.
var contributorRoleDefinitionId = 'b24988ac-6180-42a0-ab88-20f7382dd24c'

resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
  tags: tags
}

module swa 'modules/staticWebApp.bicep' = {
  name: 'staticWebApp'
  scope: rg
  params: {
    name: staticWebAppName
    location: staticWebAppLocation
    sku: sku
    customDomains: customDomains
    tags: tags
  }
}

module identity 'modules/identity.bicep' = if (deployManagedIdentity) {
  name: 'managedIdentity'
  scope: rg
  params: {
    name: managedIdentityName
    location: location
    tags: tags
    githubRepository: githubRepository
    federatedCredentials: federatedCredentials
  }
}

module dns 'modules/dnsZone.bicep' = if (deployDnsZone) {
  name: 'dnsZone'
  scope: rg
  params: {
    domainName: domainName
    staticWebAppResourceId: swa.outputs.id
    staticWebAppDefaultHostname: swa.outputs.defaultHostname
    tags: tags
  }
}

// Subscription-scoped Contributor role assignment for the UAMI.
// The assignment name is a deterministic GUID so repeat deployments are idempotent.
// Requires the deploying principal to have Owner or User Access Administrator role.
resource identityContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (deployManagedIdentity && deployRoleAssignment) {
  name: guid(subscription().id, managedIdentityName, contributorRoleDefinitionId)
  properties: {
    principalId: identity!.outputs.principalId
    principalType: 'ServicePrincipal'
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', contributorRoleDefinitionId)
  }
}

output resourceGroupName string = rg.name
output staticWebAppName string = swa.outputs.name
output staticWebAppDefaultHostname string = swa.outputs.defaultHostname
output managedIdentityClientId string = deployManagedIdentity ? identity!.outputs.clientId : ''
output managedIdentityPrincipalId string = deployManagedIdentity ? identity!.outputs.principalId : ''
output managedIdentityTenantId string = deployManagedIdentity ? identity!.outputs.tenantId : ''
output dnsNameServers array = deployDnsZone ? dns!.outputs.nameServers : []
