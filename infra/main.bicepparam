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

param deployManagedIdentity = true
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
