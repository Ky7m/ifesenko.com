# infra

Bicep templates that provision the Azure Static Web App hosting [ifesenko.com](https://ifesenko.com).

## Layout

| File | Purpose |
| --- | --- |
| `main.bicep` | Subscription-scoped entry point. Creates the resource group and deploys the SWA module. |
| `main.bicepparam` | Default parameters for the deployment. |
| `modules/staticWebApp.bicep` | `Microsoft.Web/staticSites` resource (Standard SKU, enterprise-grade CDN enabled) and optional custom domain bindings. |

The Static Web App is deployed **without** a repository binding (`provider: 'None'`). Deployments are pushed from GitHub Actions using the deployment token, which keeps per-PR preview environments working cleanly and avoids coupling the resource to a specific branch.

## One-time deployment

```bash
# 1. Log in and pick the target subscription
az login
az account set --subscription <SUBSCRIPTION_ID>

# 2. Deploy (subscription-scoped — creates the resource group)
az deployment sub create \
  --name ifesenko-swa \
  --location centralus \
  --template-file infra/main.bicep \
  --parameters infra/main.bicepparam

# 3. Fetch the deployment token and store it as a GitHub secret
TOKEN=$(az staticwebapp secrets list \
  --name swa-ifesenko \
  --resource-group ifesenko-com \
  --query properties.apiKey -o tsv)
gh secret set AZURE_STATIC_WEB_APPS_API_TOKEN --body "$TOKEN"
```

Subsequent template updates are also safe to run via `az deployment sub create` — Bicep will diff and only apply changes.

## Custom domains

1. At your DNS provider, add:
   - `TXT _dnsauth.ifesenko.com` with the validation token shown in the Azure Portal (Static Web App → Custom domains).
   - `ALIAS` / `A` record for `ifesenko.com` → the SWA default hostname.
   - `CNAME www.ifesenko.com` → the SWA default hostname.
2. Update `customDomains` in `main.bicepparam`:
   ```bicep
   param customDomains = [ 'ifesenko.com', 'www.ifesenko.com' ]
   ```
3. Re-run the deployment.

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
