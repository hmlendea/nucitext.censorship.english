# Configuration

## Overview

The library has **no runtime configuration**. All behaviour is determined by embedded word lists compiled into the assembly.

## Configuration Sources

| Source | Supported | Description |
|--------|-----------|-------------|
| Environment variables | ❌ | Not consumed |
| Configuration files (JSON, XML, etc.) | ❌ | Not consumed |
| Command-line arguments | ❌ | Not consumed |
| Dependency injection | ❌ | Not required; stateless |
| Embedded resources | ✅ | Only source of word lists |

## Behaviour Determinants

| Behaviour | Determined By |
|-----------|---------------|
| Bad words censored | `Data/bad.txt` (embedded) |
| URLs censored | `Data/hosts.txt` + `Data/tlds.txt` (embedded) |
| Replacement token | Hardcoded `****` |
| Case sensitivity | Hardcoded `IgnoreCase` |
| Word boundary matching | Hardcoded `\b` in regex |
| Regex compilation | Hardcoded `Compiled` option |

## Modifying Behaviour

To change censorship behaviour:

1. **Modify word lists:** Edit `Data/bad.txt`, `Data/hosts.txt`, or `Data/tlds.txt`
2. **Rebuild:** `dotnet build NuciText.Censorship.English`
3. **Version bump:** Update `<Version>` in `.csproj`
4. **Publish:** `dotnet pack -c Release`

## Host Application Integration

Host applications configure censorship by:

1. Adding NuGet package reference
2. Instantiating `EnglishTextCensor` or registering with DI container
3. Calling `Censor(text)` — no configuration needed

```csharp
// No configuration required
ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor(inputText);
```

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Configuration section
- [design-decisions.md](./design-decisions.md) — Decision: No Runtime Configuration
- [data-model.md](./data-model.md) — Embedded word lists