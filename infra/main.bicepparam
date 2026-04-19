using './main.bicep'

param resourceGroupName = 'ifesenko-com'
param location = 'centralus'
param staticWebAppLocation = 'centralus'
param staticWebAppName = 'swa-ifesenko'
param sku = 'Standard'

// Leave empty for the initial deployment. Add custom domains after DNS
// records (TXT validation + CNAME/ALIAS) are configured with the registrar.
param customDomains = []

param tags = {
  app: 'ifesenko.com'
  managedBy: 'bicep'
}
