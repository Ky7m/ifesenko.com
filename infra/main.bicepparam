using './main.bicep'

param resourceGroupName = 'ifesenko-com'
param location = 'centralus'
param staticWebAppLocation = 'centralus'
param staticWebAppName = 'swa-ifesenko'
param sku = 'Standard'

param customDomains = [
  'ifesenko.com'
  'www.ifesenko.com'
]

param deployDnsZone = true
param domainName = 'ifesenko.com'

param deployManagedIdentity = true
// The deploying principal (OIDC service principal) only has Contributor, which
// cannot create role assignments. Set to true once the principal has Owner or
// User Access Administrator, or create the role assignment manually via:
//   az role assignment create --assignee <managed-identity-principal-id> \
//     --role Contributor --scope /subscriptions/<subscription-id>
param deployRoleAssignment = false
param managedIdentityName = 'id-ifesenko-github'
param githubRepository = 'Ky7m/ifesenko.com'
param federatedCredentials = [
  {
    name: 'github-env-infra'
    subject: 'environment:infra'
  }
]

param tags = {
  app: 'ifesenko.com'
  managedBy: 'bicep'
}
