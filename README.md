# ifesenko.com

Personal website built with **Blazor WebAssembly** (.NET 10) and hosted on **Azure Static Web Apps**.

## Architecture

- **Frontend**: Blazor WebAssembly standalone app (`src/PersonalWebApp/`).
- **Hosting**: Azure Static Web Apps (Standard SKU). Global CDN, free TLS, per-PR preview environments.
- **Infra as Code**: Bicep (`infra/`). See [`infra/README.md`](infra/README.md) for one-time provisioning.
- **CI/CD**: GitHub Actions (`.github/workflows/`).

## Repo layout

```
infra/                       # Bicep templates for Azure Static Web Apps
src/PersonalWebApp/          # Blazor WASM app
  Program.cs
  App.razor
  Pages/                     # Razor page components (routed)
  Components/                # Reusable Razor components
  Layout/                    # MainLayout
  Services/                  # IStorageService + in-memory event data
  Models/
  EventsList/                # Hard-coded yearly event lists
  wwwroot/
    index.html
    css/app.css              # Pre-compiled from legacy SCSS
    js/site.js               # Vanilla JS invoked via JS interop
    staticwebapp.config.json # Routes, CSP, anti-bot redirects, responseOverrides
.github/workflows/
  ci-cd.yml                  # Build + deploy on push/PR (creates SWA preview per PR)
  close-pr.yml               # Tears down the preview environment when the PR closes
  infra.yml                  # Manual (workflow_dispatch) Bicep deployment
  codeql.yml                 # Scheduled CodeQL scan
```

## Local development

Requires the .NET 10 SDK.

```bash
dotnet run --project src/PersonalWebApp
```

The Blazor dev server starts on `https://localhost:5001` by default.

## Deployment

### One-time provisioning

1. Deploy the SWA resource via Bicep — see [`infra/README.md`](infra/README.md).
2. Retrieve the deployment token:

   ```bash
   az staticwebapp secrets list \
     --name <swa-name> \
     --resource-group <rg-name> \
     --query properties.apiKey -o tsv
   ```

3. Store it as the repository secret **`AZURE_STATIC_WEB_APPS_API_TOKEN`**.
4. (Optional) For `infra.yml` OIDC deploys, also configure `AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, `AZURE_SUBSCRIPTION_ID` secrets and the matching federated credentials on the app registration.

### Automatic deploys

- Every push to `main` → production deploy.
- Every PR → preview deploy at `https://<name>-<pr>.<region>.azurestaticapps.net`. The URL is posted as a PR comment by the SWA action.
- Closing / merging the PR tears the preview environment down automatically.

### Custom domains

Custom domains (`ifesenko.com`, `www.ifesenko.com`) are added via the Azure portal or `az staticwebapp hostname set` after DNS validation. They are intentionally not managed by Bicep to keep DNS ownership explicit.

## Decommissioning the legacy App Service

After the SWA is verified in production, delete the old App Service resources:

```bash
az webapp delete --name app-ifesenko-centralus --resource-group <old-rg>
az appservice plan delete --name <plan-name> --resource-group <old-rg>
# Optionally delete the resource group if it only contained these resources:
# az group delete --name <old-rg> --yes
```

Also remove any orphaned Application Insights / CDN resources that referenced the old site.
