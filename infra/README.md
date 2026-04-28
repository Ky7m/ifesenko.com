# infra

Bicep templates that provision the Azure Static Web App hosting [ifesenko.com](https://ifesenko.com).

## Layout

| File | Purpose |
| --- | --- |
| `main.bicep` | Subscription-scoped entry point. Creates the resource group, the SWA, and (optionally) the GitHub Actions OIDC identity. |
| `main.bicepparam` | Default parameters for the deployment. |
| `modules/staticWebApp.bicep` | `Microsoft.Web/staticSites` resource (Standard SKU, enterprise-grade CDN enabled) and optional custom domain bindings. |
| `modules/identity.bicep` | User-assigned managed identity + federated identity credentials for GitHub Actions OIDC. |
| `modules/dnsZone.bicep` | Azure DNS zone with apex alias A record (→ SWA) and `www` CNAME record. |

The Static Web App is deployed **without** a repository binding (`provider: 'None'`). Deployments are pushed from GitHub Actions using the deployment token, which keeps per-PR preview environments working cleanly and avoids coupling the resource to a specific branch.

## GitHub Actions OIDC identity

The deployment provisions a **user-assigned managed identity** (`id-ifesenko-github` by default) in the same resource group as the Static Web App, registers federated credentials that trust this repository, and grants the identity the built-in **Contributor** role at subscription scope. The `infra.yml` workflow uses this identity via [`azure/login@v3`](https://github.com/Azure/login#login-with-openid-connect-oidc-recommended) — no client secrets are stored.

Federated credentials are defined in `main.bicepparam` under `federatedCredentials`. Each entry's `subject` is automatically prefixed with `repo:<githubRepository>:`, so `environment:infra` becomes `repo:Ky7m/ifesenko.com:environment:infra`. Add more entries if other workflows / branches / PRs need OIDC access — e.g.:

```bicep
param federatedCredentials = [
  { name: 'github-env-infra',    subject: 'environment:infra' }
  { name: 'github-branch-main',  subject: 'ref:refs/heads/main' }
  { name: 'github-pull-request', subject: 'pull_request' }
]
```
