# Coding Agent Instructions

## Build & Run

Requires the **.NET 10 SDK**. The solution (`PersonalWebApp.slnx`, the newer XML format) contains a single project; there is no separate test project, so there is no test suite to run.

```bash
# Compile-check (fastest way to validate a change)
dotnet build

# Run locally (dev server at https://localhost:5001)
dotnet run --project src/PersonalWebApp

# Publish for deployment
dotnet publish src/PersonalWebApp/PersonalWebApp.csproj -c Release -o publish --nologo
```

## Architecture

This is a **Blazor WebAssembly** standalone app (no server-side component) hosted on **Azure Static Web Apps**. The entire site is a single-page app with hash-based section navigation (`#home`, `#events`, `#certifications`). The `#home` section is a combined landing/profile hero (portrait avatar, static tagline, social links, and bio).

**Data flow**: Event/speaking data is hardcoded in C# — there is no database or API. `InMemoryStorageService` aggregates yearly event lists and serves them filtered by a `?period=` query parameter (year number or `"all"`). It is registered as a singleton via DI.

**JS interop**: Blazor calls into `window.ifesenkoShell.init()` / `.dispose()` (defined in `wwwroot/js/site.js`) for DOM work that Blazor doesn't handle — navbar/menu behavior, smooth scrolling (same-page guarded), section reveal animation, the interactive theme toggle (persists to `localStorage` under `theme-preference`, with OS-preference sync), and active-nav scrollspy behavior. The `Home.razor` page manages the interop lifecycle via `OnAfterRenderAsync` and `IDisposable`.

**Theming (two files, don't confuse them)**: `wwwroot/js/theme.js` is a tiny script loaded in `<head>` *before* Blazor. It applies the stored/OS theme immediately (sets `data-theme` on `<html>` and the `theme-color` meta) purely to avoid a flash of the wrong theme — it deliberately does **not** persist. Persistence and the interactive toggle live in `site.js` (`ifesenkoShell`). Both share the `theme-preference` key and the `data-theme` attribute, so keep those names in sync if you touch either file.

**Infrastructure**: `infra/` contains Bicep templates deployed via the `infra.yml` workflow (manual trigger, OIDC auth). The SWA uses `provider: 'None'` — deployments are pushed from CI, not pulled by Azure.

**Static Web App config** (`wwwroot/staticwebapp.config.json`): Defines the Blazor navigation fallback, security headers, MIME types for WebAssembly, and anti-bot redirect routes. The 404 override rewrites to `index.html` so Blazor handles all routing.

## Key Conventions

### Adding events

Each year has a dedicated file `src/PersonalWebApp/EventsList/Events20XX.cs` exposing a `static EventModel[] List` property. To add a new year:

1. Create `EventsYYYY.cs` following the existing pattern.
2. Register it in `InMemoryStorageService.AllEvents` dictionary.

Use `CommonStrings` constants for recurring location names and collateral labels (Recording, PowerPoint, Demo Code). `EventModelItem` has a two-arg constructor (description + collateral dictionary) and a one-arg shortcut when there's no collateral.

### CSS

`wwwroot/css/app.css` is pre-compiled from legacy SCSS. There is no SCSS build step in the repo — edit the CSS file directly.

### CI/CD

- Push to `main` → production deploy.
- Every PR → preview environment at a unique Azure URL (posted as a PR comment). Closing the PR tears it down (`close-pr.yml`).
- Fork PRs skip deployment (no access to secrets) but still build to validate.
