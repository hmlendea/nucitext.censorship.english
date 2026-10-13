# Invariants

## System-Wide Invariants

| # | Invariant | Description |
|---|-----------|-------------|
| 1 | **Statelessness** | No mutable instance state; all processing is a pure function of input text and embedded word lists |
| 2 | **Thread safety** | Static regex instances are immutable after initialization; `Regex.Replace` is thread-safe |
| 3 | **No side effects** | Input text is never logged, stored, or transmitted |
| 4 | **Word-boundary awareness** | Bad words are matched only at word boundaries to avoid false positives |
| 5 | **Case insensitivity** | All matching is case-insensitive |
| 6 | **Regex safety** | All word list entries are escaped via `Regex.Escape` to prevent ReDoS |
| 7 | **No external I/O** | No network calls, file system access, or persistent storage at runtime |
| 8 | **Deterministic output** | Same input always produces same output |
| 9 | **Idempotent replacement** | `****` replacement does not match any word list entry |
| 10 | **Immutable word lists** | Word lists are embedded resources; cannot be modified at runtime |

## Contract Invariants

| Contract | Owner | Invariant | Verification |
|----------|-------|-----------|--------------|
| `ITextCensor.Censor(string)` | NuciText.Censorship | Returns censored text; never mutates input; thread-safe | Unit tests |
| `EnglishTextCensor` public API | This library | Sealed class; single public method `Censor`; no breaking changes without major version | Unit tests; `GenerateDocumentationFile` |
| Embedded word list format | This library | UTF-8, one entry per line, whitespace lines ignored | Build verification |
| NuGet package identity | This library | Package ID `NuciText.Censorship.English`; GPL-3.0-or-later license | `dotnet pack` verification |

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Compatibility contracts section
- [components/application-services.md](./components/application-services.md) — Implementation detail
- [error-handling.md](./error-handling.md) — Error handling