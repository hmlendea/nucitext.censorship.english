# Application Services

## Core Implementation: EnglishTextCensor

**Source:** `NuciText.Censorship.English/EnglishTextCensor.cs`

### Responsibilities

- Implement `ITextCensor.Censor(string)`
- Load and compile regex patterns from embedded word lists
- Perform case-insensitive, word-boundary-aware replacement

### Class Structure

```csharp
public sealed class EnglishTextCensor : ITextCensor
{
    private static string CensoredWordReplacement => "****";

    private static readonly Regex BadWordPattern = BuildBadWordPattern();
    private static readonly Regex UrlPattern = BuildUrlPattern();

    public string Censor(string text)
    {
        string result = BadWordPattern.Replace(text, CensoredWordReplacement);
        result = UrlPattern.Replace(result, CensoredWordReplacement);
        return result;
    }

    private static Regex BuildBadWordPattern() { ... }
    private static Regex BuildUrlPattern() { ... }
    private static IEnumerable<string> LoadWordList(string fileName) { ... }
}
```

### Static Initialization

On first access to `BadWordPattern` or `UrlPattern`:
1. `LoadWordList` reads embedded resources (`bad.txt`, `hosts.txt`, `tlds.txt`)
2. `BuildBadWordPattern` creates regex: `\b(word1|word2|...)\b` with `IgnoreCase | Compiled`
3. `BuildUrlPattern` creates regex: `\b(host1|host2|...)\.(tld1|tld2|...)\b` with `IgnoreCase | Compiled`

### Censor Method Flow

```csharp
public string Censor(string text)
{
    // 1. Replace bad words
    string result = BadWordPattern.Replace(text, "****");
    // 2. Replace URLs
    result = UrlPattern.Replace(result, "****");
    // 3. Return censored text
    return result;
}
```

### LoadWordList Implementation

```csharp
private static IEnumerable<string> LoadWordList(string fileName)
{
    Assembly assembly = Assembly.GetExecutingAssembly();
    string resourceName = $"NuciText.Censorship.English.Data.{fileName}";

    using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
    using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

    string content = reader.ReadToEnd();

    return content
        .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
        .Where(line => !string.IsNullOrWhiteSpace(line));
}
```

### Regex Pattern Construction

**Bad Word Pattern:**
```csharp
string alternation = string.Join("|", badWords.Select(Regex.Escape));
return new Regex($@"\b({alternation})\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
```

**URL Pattern:**
```csharp
string hostAlternation = string.Join("|", hosts.Select(Regex.Escape));
string tldAlternation = string.Join("|", tlds.Select(Regex.Escape));
return new Regex($@"\b({hostAlternation})\.({tldAlternation})\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
```

### Embedded Resources

| Resource | Path | Description |
|----------|------|-------------|
| `bad.txt` | `NuciText.Censorship.English.Data.bad.txt` | Offensive English words (~200 entries) |
| `hosts.txt` | `NuciText.Censorship.English.Data.hosts.txt` | Unwanted domain hostnames (~300 entries) |
| `tlds.txt` | `NuciText.Censorship.English.Data.tlds.txt` | Top-level domains (~200 entries) |

### Thread Safety

- Static regex fields are initialized once by .NET runtime (thread-safe)
- `Regex.Replace` is thread-safe for read-only patterns
- No mutable instance state

### Performance Characteristics

| Aspect | Detail |
|--------|--------|
| First call | ~1-5ms (resource loading + regex compilation) |
| Subsequent calls | O(n) in input length; compiled regex avoids recompilation |
| Memory | Regex patterns held for process lifetime (~KB) |
| CPU | Regex matching is CPU-bound; no async overhead |

### Error Paths

| Condition | Behaviour |
|-----------|-----------|
| `text` is null | `ArgumentNullException` from `Regex.Replace` |
| Resource not found | `NullReferenceException` from `!` operator (should not occur) |
| Regex timeout | Not possible (no timeout set, simple alternations) |

### Related Documentation

- [flows/censorship-execution.md](./flows/censorship-execution.md) — End-to-end flow
- [data-model.md](./data-model.md) — Word list data model
- [error-handling.md](./error-handling.md) — Error taxonomy
- [invariants.md](./invariants.md) — System invariants