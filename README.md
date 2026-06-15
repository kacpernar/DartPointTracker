# DartPointTracker

A darts scorekeeping **Progressive Web App**, built **twice** — once in **Blazor WebAssembly** and once in **Vue.js** — on top of a single shared backend.

The project is the practical artefact of a master's thesis comparing PWA development in **Blazor WebAssembly (.NET 8)** against **Vue.js (Vue 3 + Vite)**. By implementing identical functionality on a shared API, any difference that surfaces is attributable to the framework rather than the design.

## What it does

DartPointTracker helps players keep score during a game of darts:

- **Game modes** — 301 / 501; players reduce their score to exactly zero.
- **Live scoring** — per-throw input with automatic calculation of remaining points, turn handling, and a final game summary with standings.
- **Elo ranking** — every player starts at an Elo of `1000`; results are sent to the server and ratings are recalculated after each game.
- **PWA features** — installable to the home screen via a Web App Manifest, with offline scorekeeping and asset caching through a service worker.

## Architecture

Both front-ends talk to one backend over HTTPS, so they remain directly comparable.

```
                ┌─────────────────────────┐
                │   DartPointTracker.Api   │   ASP.NET Core (.NET 8) minimal API
                │   EF Core + SQLite       │   players, games, Elo
                └────────────┬─────────────┘
                             │ HTTPS / REST
            ┌────────────────┴────────────────┐
            │                                  │
 ┌──────────────────────┐         ┌──────────────────────┐
 │   DartPointTracker    │         │  DartPointTrackerVue  │
 │  Blazor WebAssembly   │         │   Vue 3 + Vite (PWA)  │
 │   (.NET 8, PWA)       │         │   Pinia · Vue Router  │
 └──────────────────────┘         └──────────────────────┘
```

## Repository structure

| Path | Description |
|------|-------------|
| `DartPointTracker/` | Blazor WebAssembly client (PWA — `sw.js`, `workbox-config.js`). |
| `DartPointTrackerVue/` | Vue 3 + Vite client (PWA via `vite-plugin-pwa`, Pinia, Vue Router, Bootstrap 5, TypeScript). |
| `DartPointTracker.Api/` | ASP.NET Core minimal API — EF Core + SQLite, Basic Authentication, Swagger, CORS. |
| `TestProject/` | xUnit tests. |
| `DartPointTracker.sln` | Visual Studio solution. |
| `.github/workflows/` | CI/CD — builds and deploys the API to Azure App Service. |

## Tech stack

- **Backend:** ASP.NET Core (.NET 8) minimal API, Entity Framework Core, SQLite, Swashbuckle (Swagger), Basic Authentication.
- **Blazor client:** Blazor WebAssembly (.NET 8), Workbox service worker.
- **Vue client:** Vue 3.4, Vite 5, Pinia, Vue Router, Bootstrap 5, TypeScript, `vite-plugin-pwa`.
- **Testing:** xUnit.
- **Deployment:** API → Azure App Service (GitHub Actions); front-ends → Firebase Hosting.

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 18.3+ (for the Vue client)

### 1. Run the API

```bash
cd DartPointTracker.Api
dotnet restore
dotnet run
```

The API listens on `http://localhost:5144` (and `https://localhost:7276`). In development, Swagger UI is available at `/swagger`. The SQLite database file is created automatically on first run.

#### API endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/Game` | Submit a finished game's results; recalculates player Elo. |
| `POST` | `/Player?playerName=...` | Create a new player (default Elo 1000). |
| `GET` | `/Players` | List all players and their Elo rankings. |
| `POST` | `/GeneratePlayers/{count}` | Seed `count` random players (testing/benchmarks). |
| `DELETE` | `/DeletePlayers/{count}` | Remove `count` players. |

### 2. Run the Blazor client

```bash
cd DartPointTracker
dotnet run
```

### 3. Run the Vue client

```bash
cd DartPointTrackerVue
npm install
npm run dev      # start the Vite dev server
npm run build    # production build
npm run preview  # preview the production build
```

> Point each client at your API base URL before running (check the client's configuration/service files).

## Running with Docker

The API ships with a Dockerfile (exposing ports 8080/8081):

```bash
docker build -t dartpointtracker-api -f DartPointTracker.Api/Dockerfile .
docker run -p 8080:8080 dartpointtracker-api
```

## Tests

```bash
dotnet test
```

## Thesis findings

Building the same app in both frameworks surfaced clear trade-offs:

- **Code volume** — the Blazor implementation had **20–30% fewer lines**, helped by C#'s conciseness, built-in dependency injection, and convention-based routing.
- **App size** — the Blazor PWA cached **over 10 MB** (≈6.7 MB of which is the .NET runtime), versus **~530 KB** for the tree-shaken Vue build.
- **Runtime performance** — Vue was substantially faster where work crosses the WebAssembly↔JavaScript boundary: up to **~40× faster** HTTP calls and **30–38× faster** UI diffing. For pure computation at scale (sorting thousands of players), WebAssembly pulled *ahead*.

**Conclusion:** Blazor WebAssembly fully meets PWA requirements (installability, offline, background sync) and yields a smaller, more maintainable codebase — making it a strong choice for .NET-experienced teams and in-house tools where development speed and maintainability outweigh raw runtime performance.

## License

[MIT](LICENSE)
