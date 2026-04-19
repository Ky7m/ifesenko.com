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

@description('Tags applied to all resources created by this deployment.')
param tags object = {
  app: 'ifesenko.com'
  managedBy: 'bicep'
}

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

output resourceGroupName string = rg.name
output staticWebAppName string = swa.outputs.name
output staticWebAppDefaultHostname string = swa.outputs.defaultHostname
