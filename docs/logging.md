# Logging

## Overview

The library emits **no logs, metrics, or traces**. It is a stateless, in-memory text processor with no observability concerns.

## Logging Framework

None. The library does not reference any logging framework (Serilog, NLog, log4net, etc.).

## Log Levels

Not applicable. No logging is performed.

## Structured Fields

Not applicable. No log entries are produced.

## Correlation

Not applicable. No request tracing or correlation IDs.

## Sinks/Destinations

Not applicable. No log output destinations.

## Redaction/Masking

Not applicable. No data is logged.

## Retention

Not applicable. No log retention policies.

## Sampling

Not applicable. No log sampling.

## Performance Considerations

- No logging overhead
- Host applications may add their own observability around `Censor` calls

## Error Logging

Not applicable. Exceptions propagate to the caller; host application is responsible for logging.

## Audit Logging

Not applicable. No security-relevant events are logged by the library.

## Configuration

Not applicable. No logging configuration.

## Host Application Guidance

Host applications that require observability should wrap `Censor` calls:

```csharp
// Example: Host application adds its own logging
try
{
    logger.LogInformation("Censoring text of length {Length}", input.Length);
    string result = censor.Censor(input);
    logger.LogInformation("Censorship complete, result length {Length}", result.Length);
}
catch (Exception ex)
{
    logger.LogError(ex, "Censorship failed");
    throw;
}
```

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Observability section
- [components/application-services.md](./components/application-services.md) — Implementation detail