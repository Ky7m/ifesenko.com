@description('DNS domain name to host in Azure DNS (e.g. ifesenko.com).')
param domainName string

@description('Resource ID of the Static Web App. Used as the alias target for the apex A record.')
param staticWebAppResourceId string

@description('Default hostname of the Static Web App (e.g. <name>.azurestaticapps.net). Used as the CNAME target for www.')
param staticWebAppDefaultHostname string

@description('Tags applied to the DNS zone.')
param tags object = {}

resource zone 'Microsoft.Network/dnsZones@2018-05-01' = {
  name: domainName
  location: 'global'
  tags: tags
}

// Apex (root) domain — alias A record pointing to the Static Web App resource.
// Azure DNS resolves this dynamically so no static IP is needed.
resource apexRecord 'Microsoft.Network/dnsZones/A@2018-05-01' = {
  parent: zone
  name: '@'
  properties: {
    TTL: 3600
    targetResource: {
      id: staticWebAppResourceId
    }
  }
}

// www subdomain — standard CNAME to the SWA default hostname.
resource wwwRecord 'Microsoft.Network/dnsZones/CNAME@2018-05-01' = {
  parent: zone
  name: 'www'
  properties: {
    TTL: 3600
    CNAMERecord: {
      cname: staticWebAppDefaultHostname
    }
  }
}

output zoneName string = zone.name
output nameServers array = zone.properties.nameServers
