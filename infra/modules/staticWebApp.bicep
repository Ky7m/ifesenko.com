@description('Name of the Static Web App resource.')
param name string

@description('Region for the Static Web App. Use a region that supports Microsoft.Web/staticSites.')
param location string

@description('SKU tier for the Static Web App.')
@allowed([
  'Free'
  'Standard'
])
param sku string = 'Standard'

@description('Custom domains to bind to the Static Web App (e.g. ifesenko.com, www.ifesenko.com). DNS records must be configured separately.')
param customDomains array = []

@description('Tags applied to the Static Web App resource.')
param tags object = {}

// Deployments are driven from GitHub Actions using the deployment token,
// so no repositoryUrl/branch/token is bound to the resource itself.
// This also keeps preview environments per-PR working cleanly.
resource staticSite 'Microsoft.Web/staticSites@2024-04-01' = {
  name: name
  location: location
  tags: tags
  sku: {
    name: sku
    tier: sku
  }
  properties: {
    allowConfigFileUpdates: true
    stagingEnvironmentPolicy: 'Enabled'
    enterpriseGradeCdnStatus: sku == 'Standard' ? 'Enabled' : 'Disabled'
    provider: 'None'
  }
}

resource domains 'Microsoft.Web/staticSites/customDomains@2024-04-01' = [for domain in customDomains: {
  parent: staticSite
  name: domain
  properties: {}
}]

output name string = staticSite.name
output id string = staticSite.id
output defaultHostname string = staticSite.properties.defaultHostname
