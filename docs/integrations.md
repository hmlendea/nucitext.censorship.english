# Integrations

## Overview

The library has **no external integrations**. It is a self-contained, in-process text processor.

## Integration Inventory

| Integration | Type | Direction | Data |
|-------------|------|-----------|------|
| NuciText.Censorship (NuGet) | Package dependency | Inbound (interface) | `ITextCensor` interface |
| Host Application | Consumer | Outbound | Input text → censored text |
| Embedded Resources | Internal | Internal | Word lists |

## External System Integrations

None. The library does not integrate with:

- Databases
- Message queues
- HTTP clients
- External APIs
- Cloud services
- Monitoring systems
- Logging services
- Telemetry systems

## Host Application Integration

Host applications consume the library by:

1. Adding NuGet package reference:
   ```xml
   <PackageReference Include="NuciText.Censorship.English" Version="1.0.0" />
   ```

2. Instantiating or resolving via DI:
   ```csharp
   ITextCensor censor = new EnglishTextCensor();
   // or via DI container
   ```

3. Calling `Censor`:
   ```csharp
   string result = censor.Censor(inputText);
   ```

## Related Documentation

- [dependencies.md](./dependencies.md) — External dependencies
- [components/presentation.md](./components/presentation.md) — Public API
- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Interfaces and integrations section