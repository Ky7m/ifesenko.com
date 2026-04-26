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

## One-time deployment

```bash
# 1. Log in and pick the target subscription
az login
az account set --subscription <SUBSCRIPTION_ID>

# 2. Deploy (subscription-scoped — creates the resource group, SWA, and UAMI).
#    Requires Owner on the subscription because it also creates a role assignment.
az deployment sub create \
  --name ifesenko-swa \
  --location centralus \
  --template-file infra/main.bicep \
  --parameters infra/main.bicepparam

# 3. Capture the outputs you need for GitHub.
CLIENT_ID=$(az deployment sub show --name ifesenko-swa --query properties.outputs.managedIdentityClientId.value -o tsv)
TENANT_ID=$(az deployment sub show --name ifesenko-swa --query properties.outputs.managedIdentityTenantId.value -o tsv)
SUBSCRIPTION_ID=$(az account show --query id -o tsv)

TOKEN=$(az staticwebapp secrets list \
  --name swa-ifesenko \
  --resource-group ifesenko-com \
  --query properties.apiKey -o tsv)

# 4. Store the secrets in GitHub.
gh secret set AZURE_CLIENT_ID       --body "$CLIENT_ID"
gh secret set AZURE_TENANT_ID       --body "$TENANT_ID"
gh secret set AZURE_SUBSCRIPTION_ID --body "$SUBSCRIPTION_ID"
gh secret set AZURE_STATIC_WEB_APPS_API_TOKEN --body "$TOKEN"

# 5. Create the `infra` GitHub environment (its name is the federated subject).
gh api -X PUT "repos/$(gh repo view --json nameWithOwner -q .nameWithOwner)/environments/infra" >/dev/null
```

After the first deployment, subsequent runs of the `Infra` GitHub Actions workflow (`.github/workflows/infra.yml`) authenticate as the UAMI and can re-deploy the template without any human interaction. The Contributor role assignment is idempotent — re-runs via the workflow do **not** require Owner.

## Custom domains

Custom domain bindings on the SWA are defined in `customDomains` in `main.bicepparam`. DNS is hosted in Azure DNS (see below).

## Azure DNS

When `deployDnsZone = true`, the deployment creates an Azure DNS zone for the domain and configures:

- **Apex** (`ifesenko.com`): alias A record pointing to the SWA resource (no static IP needed).
- **www**: CNAME record pointing to the SWA default hostname.

### Migrating from GoDaddy (one-time)

After the first deployment with `deployDnsZone = true`:

1. Get the Azure DNS nameservers from the deployment output:
   ```bash
   az deployment sub show --name ifesenko-swa \
     --query properties.outputs.dnsNameServers.value -o tsv
   ```
   You'll see four nameservers like `ns1-03.azure-dns.com.`, `ns2-03.azure-dns.net.`, etc.

2. Log into GoDaddy → **My Domains** → **ifesenko.com** → **DNS** → **Nameservers** → **Change Nameservers** → enter the four Azure DNS nameservers.

3. Wait for propagation (usually minutes, up to 48 hours). Verify with:
   ```bash
   dig ifesenko.com NS +short
   ```

Once the nameservers point to Azure DNS, all DNS management happens through the Bicep templates. The GoDaddy account only needs to keep the domain registration active.

## Decommissioning the old App Service

After the SWA is live and DNS has been cut over, remove the legacy resources:

```bash
# Delete any leftover PR slots
az webapp deployment slot list \
  --name app-ifesenko-centralus \
  --resource-group ifesenko.com \
  --query "[].name" -o tsv | \
  xargs -I{} az webapp deployment slot delete \
    --name app-ifesenko-centralus \
    --resource-group ifesenko.com \
    --slot {}

# Delete the web app and its plan
az webapp delete --name app-ifesenko-centralus --resource-group ifesenko.com
az appservice plan delete --name <plan-name> --resource-group ifesenko.com --yes

# Optional: delete the old resource group entirely if nothing else is in it
az group delete --name ifesenko.com --yes --no-wait
```
