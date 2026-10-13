# Build and Deployment

## Build Pipeline

### CI/CD

**Workflow:** `.github/workflows/dotnet.yml`

**Triggers:**
- Push to `master` branch
- Pull request to `master` branch

**Runner:** `ubuntu-latest`

**Steps:**
1. Checkout repository (`actions/checkout@v7`)
2. Setup .NET 10.0.x (`actions/setup-dotnet@v5`)
3. Restore dependencies (`dotnet restore`)
4. Build (`dotnet build --no-restore`)
5. Test (`dotnet test --no-build --verbosity normal`)

### Local Build

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build NuciText.Censorship.English

# Build in Release configuration
dotnet build -c Release
```

### Local Test

```bash
dotnet test NuciText.Censorship.English.slnx
```

### Local Pack

```bash
dotnet pack NuciText.Censorship.English -c Release
```

## Deployment

### Distribution Channel

- **NuGet:** `NuciText.Censorship.English` package
- **GitHub Releases:** Source releases

### Versioning

| Component | Version |
|-----------|---------|
| Library | 1.0.0 |
| NuciText.Censorship dependency | 1.0.0 |
| .NET target | net10.0 |

### Environments

| Environment | Description |
|-------------|-------------|
| Development | Local machine; `dotnet build` |
| CI | GitHub Actions; `ubuntu-latest` |
| Production | NuGet package consumption |

## Process Topology

- **In-process library:** Runs in host application's process
- **No separate service:** No deployment unit beyond the NuGet package
- **No persistent state:** No database, file system, or cache requirements

## Scaling

- **Stateless:** Scales with host application
- **Thread-safe:** No coordination needed across threads
- **No coordination:** No distributed state or locking

## Startup

- **Static initialization:** On first `Censor` call
- **Cost:** ~1-5ms (resource loading + regex compilation)
- **No warmup required:** Lazy initialization

## Shutdown

- **No cleanup required:** No `IDisposable`, no finalizers, no unmanaged resources

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Deployment and operations section
- [Root README.md](../README.md) — Development instructions
- [dependencies.md](./dependencies.md) — External dependencies