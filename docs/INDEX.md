# NuciText.Censorship.English Documentation

## Repository Summary

NuciText.Censorship.English is a .NET library (NuGet package) that implements the `ITextCensor` interface from NuciText.Censorship. Its principal responsibility is to censor English text by replacing offensive words and known unwanted domain names with asterisks (`****`). The library is consumed by host applications that need text filtering capabilities.

## Root Document Map

- [ARCHITECTURE.md](../ARCHITECTURE.md) — High-level architecture, components, data flows, and design decisions
- [SECURITY.md](../SECURITY.md) — Security policy, vulnerability reporting, and disclosure
- [PRIVACY.md](../PRIVACY.md) — Data handling and privacy practices
- [LICENSE](../LICENSE) — GNU General Public License v3.0 or later
- [CONTRIBUTING.md](../CONTRIBUTING.md) — Contribution guidelines and processes
- [README.md](../README.md) — Project overview, usage, installation, development

## Documentation Catalogue

### Architecture & Design

| File | Description |
|------|-------------|
| [architecture.md](./architecture.md) | High-level architecture complementing root ARCHITECTURE.md |
| [design-decisions.md](./design-decisions.md) | Key architectural/design choices and rationale |
| [repository-overview.md](./repository-overview.md) | Repository purpose, scope, entry points |
| [repository-structure.md](./repository-structure.md) | Source tree layout, module organisation |

### Components

| File | Description |
|------|-------------|
| [components/presentation.md](./components/presentation.md) | Public API surface and contracts |
| [components/application-services.md](./components/application-services.md) | Core censorship implementation |

### Flows

| File | Description |
|------|-------------|
| [flows/censorship-execution.md](./flows/censorship-execution.md) | End-to-end censorship flow from input to output |

### API Reference

| File | Description |
|------|-------------|
| [api-reference/INDEX.md](./api-reference/INDEX.md) | API reference index with endpoint summary table |
| [api-reference/englishtextcensor.md](./api-reference/englishtextcensor.md) | EnglishTextCensor class and Censor method documentation |

### Behaviour

| File | Description |
|------|-------------|
| [behaviour/censor-text.md](./behaviour/censor-text.md) | User-facing censorship behaviour |

### Cross-Cutting Concerns

| File | Description |
|------|-------------|
| [configuration.md](./configuration.md) | Configuration schema, sources, precedence |
| [dependencies.md](./dependencies.md) | External and internal dependencies |
| [error-handling.md](./error-handling.md) | Error taxonomy, handling patterns, recovery |
| [invariants.md](./invariants.md) | System-wide invariants and contracts |
| [logging.md](./logging.md) | Logging framework, levels, structured fields, correlation, sinks |
| [security.md](./security.md) | Security model, threats, mitigations (complements root SECURITY.md) |
| [testing.md](./testing.md) | Test strategy, organisation, coverage |

### Operations

| File | Description |
|------|-------------|
| [build-and-deployment.md](./build-and-deployment.md) | Build pipeline, deployment, environments |
| [change-guide.md](./change-guide.md) | How to modify common areas safely |
| [documentation-maintenance.md](./documentation-maintenance.md) | How to keep docs current, ownership |
| [troubleshooting.md](./troubleshooting.md) | Common issues and solutions |

### Reference

| File | Description |
|------|-------------|
| [api-usage-examples.md](./api-usage-examples.md) | Example requests/responses for API usage |
| [ambiguities-and-open-questions.md](./ambiguities-and-open-questions.md) | Unresolved items, TODOs, known gaps |
| [data-model.md](./data-model.md) | Domain entities, relationships, persistence |
| [faqs.md](./faqs.md) | Frequently asked questions |
| [integrations.md](./integrations.md) | External system integrations |
| [quick-start.md](./quick-start.md) | Getting started guide for newcomers |
| [state-and-persistence.md](./state-and-persistence.md) | State management, stores, caches, migrations |

## Navigation Aids

- **Start here** (newcomers): [quick-start.md](./quick-start.md) → [repository-overview.md](./repository-overview.md) → [behaviour/censor-text.md](./behaviour/censor-text.md)
- **Deep dive** (component owners): [components/application-services.md](./components/application-services.md) → [flows/censorship-execution.md](./flows/censorship-execution.md) → [architecture.md](./architecture.md)
- **Flows** (debuggers): [flows/censorship-execution.md](./flows/censorship-execution.md) → [api-reference/englishtextcensor.md](./api-reference/englishtextcensor.md)

## Maintenance Metadata

- **Last reviewed:** 2026-10-13
- **Owner:** Horațiu Mlendea
- **Coverage status:** Complete for current implementation