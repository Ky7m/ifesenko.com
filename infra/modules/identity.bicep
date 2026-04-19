@description('Name of the user-assigned managed identity.')
param name string

@description('Azure region for the managed identity.')
param location string = resourceGroup().location

@description('Tags applied to the managed identity.')
param tags object = {}

@description('GitHub repository in "owner/repo" form that will exchange tokens with this identity.')
param githubRepository string

@description('Federated identity credentials to register on the UAMI. Each entry produces a subject of the form "repo:<githubRepository>:<subject>". See https://docs.github.com/actions/deployment/security-hardening-your-deployments/about-security-hardening-with-openid-connect#example-subject-claims for valid subject formats (environment:<name>, ref:refs/heads/<branch>, pull_request, etc.).')
param federatedCredentials array = [
  {
    name: 'github-env-infra'
    subject: 'environment:infra'
  }
]

resource uami 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: name
  location: location
  tags: tags
}

resource federatedCreds 'Microsoft.ManagedIdentity/userAssignedIdentities/federatedIdentityCredentials@2023-01-31' = [for cred in federatedCredentials: {
  parent: uami
  name: cred.name
  properties: {
    issuer: 'https://token.actions.githubusercontent.com'
    subject: 'repo:${githubRepository}:${cred.subject}'
    audiences: [
      'api://AzureADTokenExchange'
    ]
  }
}]

output id string = uami.id
output name string = uami.name
output clientId string = uami.properties.clientId
output principalId string = uami.properties.principalId
output tenantId string = uami.properties.tenantId
