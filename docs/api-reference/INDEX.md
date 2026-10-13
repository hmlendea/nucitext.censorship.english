# API Reference Index

## Endpoint Summary

| Method | Path | Controller | Action | Auth |
|--------|------|------------|--------|------|
| N/A | N/A | `EnglishTextCensor` | `Censor(string)` | None |

## Controllers

- [EnglishTextCensor](./englishtextcensor.md) — Core censorship implementation

## Global Authentication

None. The library is a stateless in-process component with no authentication requirements.

## Versioning Strategy

- Semantic versioning via NuGet package version
- Breaking changes require major version bump
- Interface stability guaranteed by NuciText.Censorship package versioning

## Rate Limiting / Throttling

None. The library has no built-in rate limiting. Host applications may implement their own.

## Related Documentation

- [flows/censorship-execution.md](../flows/censorship-execution.md) — Execution flow
- [components/application-services.md](../components/application-services.md) — Implementation detail
- [data-model.md](../data-model.md) — Word list data model
- [dependencies.md](../dependencies.md) — External dependencies