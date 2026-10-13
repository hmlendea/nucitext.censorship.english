# Error Handling

## Error Taxonomy

| Error Type | Source | Condition | Behaviour |
|------------|--------|-----------|-----------|
| `ArgumentNullException` | `Regex.Replace` | `text` parameter is `null` | Thrown to caller |
| `NullReferenceException` | `LoadWordList` | Embedded resource not found | Thrown (should not occur in normal operation) |
| `RegexMatchTimeoutException` | `Regex.Replace` | Regex timeout | Not possible (no timeout set) |

## Error Handling Patterns

### No Explicit Error Handling

The `Censor` method has no try/catch blocks. It relies on .NET framework behaviour:

```csharp
public string Censor(string text)
{
    string result = BadWordPattern.Replace(text, CensoredWordReplacement);
    result = UrlPattern.Replace(result, CensoredWordReplacement);
    return result;
}
```

### Host Application Responsibility

Host applications are responsible for try/catch if they need to handle errors:

```csharp
try
{
    string result = censor.Censor(inputText);
}
catch (ArgumentNullException)
{
    // Handle null input
}
```

## Failure Scenarios

| Scenario | Cause | Result |
|----------|-------|--------|
| Null input | Host passes `null` | `ArgumentNullException` |
| Missing resource | Assembly corruption | `NullReferenceException` from `!` operator |
| Regex catastrophic backtracking | Malicious word list | Not possible (`Regex.Escape` applied to all entries) |
| Regex timeout | Long input | Not possible (no timeout set) |

## Recovery

- **Null input:** Host should validate input before calling `Censor`
- **Missing resource:** Indicates assembly corruption; requires library reinstall
- **Regex issues:** Not possible with current design (escaped literals, no timeout)

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Error handling section
- [components/application-services.md](./components/application-services.md) — Error paths
- [invariants.md](./invariants.md) — System invariants