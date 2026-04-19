using './main.bicep'

param resourceGroupName = 'ifesenko-com'
param location = 'centralus'
param staticWebAppLocation = 'centralus'
param staticWebAppName = 'swa-ifesenko'
param sku = 'Standard'

// Leave empty for the initial deployment. Add custom domains after DNS
// records (TXT validation + CNAME/ALIAS) are configured with the registrar.
param customDomains = []

param deployManagedIdentity = true
param managedIdentityName = 'id-ifesenko-github'
param githubRepository = 'Ky7m/ifesenko.com'
param federatedCredentials = [
  { name: 'github-branch-main',  subject: 'ref:refs/heads/main' }
]

param tags = {
  app: 'ifesenko.com'
  managedBy: 'bicep'
}
