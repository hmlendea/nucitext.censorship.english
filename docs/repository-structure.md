# Repository Structure

## Source Tree Layout

```
nucitext.censorship.english/
├── .github/
│   └── workflows/
│       └── dotnet.yml              # CI/CD: build, test on push/PR to master
├── docs/                           # Generated documentation (this directory)
├── NuciText.Censorship.English/    # Main library project
│   ├── Data/
│   │   ├── bad.txt                 # Offensive English words (embedded resource)
│   │   ├── hosts.txt               # Known unwanted domain hostnames (embedded resource)
│   │   └── tlds.txt                # Top-level domains for URL matching (embedded resource)
│   ├── EnglishTextCensor.cs        # Core implementation
│   └── NuciText.Censorship.English.csproj
├── NuciText.Censorship.English.UnitTests/  # Test project
│   ├── EnglishTextCensorTests.cs   # NUnit test cases
│   └── NuciText.Censorship.English.UnitTests.csproj
├── ARCHITECTURE.md                 # Root architecture documentation
├── CONTRIBUTING.md                 # Contribution guidelines
├── LICENSE                         # GPL-3.0-or-later
├── NuciText.Censorship.English.slnx  # Solution file
├── PRIVACY.md                      # Privacy policy
├── README.md                       # Project overview
├── SECURITY.md                     # Security policy
└── docs/                           # Generated documentation
```

## Module Organisation

### NuciText.Censorship.English (Library)

| Path | Type | Description |
|------|------|-------------|
| `EnglishTextCensor.cs` | Source | Sealed class implementing `ITextCensor` |
| `NuciText.Censorship.English.csproj` | Project | .NET 10.0, net10.0 target, embedded resources, NuciText.Censorship dependency |
| `Data/bad.txt` | Data | Offensive English words, one per line |
| `Data/hosts.txt` | Data | Known unwanted domain hostnames, one per line |
| `Data/tlds.txt` | Data | Top-level domains, one per line |

### NuciText.Censorship.English.UnitTests (Tests)

| Path | Type | Description |
|------|------|-------------|
| `EnglishTextCensorTests.cs` | Source | NUnit 4.x test cases for censorship behaviour |
| `NuciText.Censorship.English.UnitTests.csproj` | Project | .NET 10.0, net10.0 target, NUnit + test adapter |

## Build Artifacts

| Path | Description |
|------|-------------|
| `bin/Debug/net10.0/` | Build output (library and tests) |
| `obj/` | Intermediate build artifacts |

## CI/CD

| Path | Description |
|------|-------------|
| `.github/workflows/dotnet.yml` | GitHub Actions workflow: restore, build, test on Ubuntu |

## Documentation

| Path | Description |
|------|-------------|
| `docs/` | Generated documentation (this directory) |
| `docs/INDEX.md` | Master index and navigation |

## Related Documentation

- [repository-overview.md](./repository-overview.md) — Purpose, scope, entry points
- [architecture.md](./architecture.md) — High-level architecture