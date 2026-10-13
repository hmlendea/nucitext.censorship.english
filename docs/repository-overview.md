# Repository Overview

## Purpose

NuciText.Censorship.English is a .NET library (NuGet package) that implements the `ITextCensor` interface from NuciText.Censorship. Its principal responsibility is to censor English text by replacing offensive words and known unwanted domain names with asterisks (`****`). The library is consumed by host applications that need text filtering capabilities.

## Scope

### What It Owns

- Implementation of `ITextCensor.Censor(string)` via the `EnglishTextCensor` class
- Embedded word lists (`bad.txt`, `hosts.txt`, `tlds.txt`) compiled into the assembly
- Compiled regex patterns for bad word and URL matching
- Unit tests verifying censorship behaviour

### What It Deliberately Does Not Own

- The `ITextCensor` interface contract (owned by NuciText.Censorship)
- Host application logic, configuration, logging, or telemetry
- Runtime word list updates (lists are compile-time embedded resources)
- Network calls, persistent storage, or external I/O
- Multi-language support (English only; other languages require separate packages)

## Entry Points

| Entry Point | Location | Description |
|-------------|----------|-------------|
| `EnglishTextCensor.Censor(string)` | `NuciText.Censorship.English/EnglishTextCensor.cs` | Public method implementing `ITextCensor.Censor` |
| `EnglishTextCensor` constructor | `NuciText.Censorship.English/EnglishTextCensor.cs` | Default constructor; triggers static initialization |

## Runtime Topology

The library operates as a stateless, in-process component within a host application's process boundary.

```mermaid
flowchart LR
    HostApp[Host Application] -->|Calls Censor(text)| Lib[NuciText.Censorship.English]
    Lib -->|Returns censored text| HostApp
    Lib -.->|Loads embedded resources| Resources[Embedded Word Lists]
```

## Major Integrations

| Integration | Direction | Contract |
|-------------|-----------|----------|
| NuciText.Censorship (NuGet) | Inbound (interface) | `ITextCensor.Censor(string)` |
| Host Application | Outbound (consumer) | Instantiates `EnglishTextCensor` or resolves via DI |
| Embedded Resources | Internal | `Assembly.GetManifestResourceStream` |

## System-Wide Invariants

1. **Statelessness:** No mutable instance state; all processing is a pure function of input text and embedded word lists
2. **Thread safety:** Static regex instances are immutable after initialization; `Regex.Replace` is thread-safe
3. **No side effects:** Input text is never logged, stored, or transmitted
4. **Word-boundary awareness:** Bad words are matched only at word boundaries to avoid false positives
5. **Case insensitivity:** All matching is case-insensitive
6. **Regex safety:** All word list entries are escaped via `Regex.Escape` to prevent ReDoS

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Full architectural documentation
- [README.md](../README.md) — Usage, installation, development
- [components/application-services.md](./components/application-services.md) — Core implementation detail
- [flows/censorship-execution.md](./flows/censorship-execution.md) — Execution flow