# EnglishTextCensor

**Namespace:** `NuciText.Censorship.English`

**Assembly:** `NuciText.Censorship.English`

**Implements:** `ITextCensor` (from NuciText.Censorship)

**Type:** `public sealed class`

## Class Summary

`EnglishTextCensor` is the sole public type in the library. It implements the `ITextCensor` interface to censor English text by replacing offensive words and known unwanted domain names with asterisks (`****`).

## Members

### Public Members

| Member | Signature | Description |
|--------|-----------|-------------|
| `Censor` | `public string Censor(string text)` | Replaces bad words and URLs in `text` with `****` |

### Private Static Members

| Member | Signature | Description |
|--------|-----------|-------------|
| `CensoredWordReplacement` | `private static string CensoredWordReplacement { get; }` | Returns `"****"` |
| `BadWordPattern` | `private static readonly Regex BadWordPattern` | Compiled regex for bad word matching |
| `UrlPattern` | `private static readonly Regex UrlPattern` | Compiled regex for URL matching |
| `BuildBadWordPattern` | `private static Regex BuildBadWordPattern()` | Builds bad word regex from `bad.txt` |
| `BuildUrlPattern` | `private static Regex BuildUrlPattern()` | Builds URL regex from `hosts.txt` + `tlds.txt` |
| `LoadWordList` | `private static IEnumerable<string> LoadWordList(string fileName)` | Loads embedded word list resource |

## Censor Method

### Signature

```csharp
public string Censor(string text)
```

### Description

Censors English text by replacing bad words and known unwanted domain names with asterisks (`****`).

### Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `text` | `string` | The input text to censor |

### Returns

| Type | Description |
|------|-------------|
| `string` | The censored text with bad words and URLs replaced by `****` |

### Exceptions

| Exception | Condition |
|-----------|-----------|
| `ArgumentNullException` | `text` is `null` |

### Behaviour

1. Applies `BadWordPattern.Replace(text, "****")` — replaces bad words at word boundaries (case-insensitive)
2. Applies `UrlPattern.Replace(result, "****")` — replaces host.tld patterns at word boundaries (case-insensitive)
3. Returns the censored string

### Examples

| Input | Output |
|-------|--------|
| `"You arse!"` | `"You ****!"` |
| `"You anal person."` | `"You **** person.""` |
| `"ARSE"` | `"****"` |
| `"arse and anal"` | `"**** and ****"` |
| `"Hello, World!"` | `"Hello, World!"` |
| `""` | `""` |
| `"Visit abercrombie.ac now."` | `"Visit **** now.""` |
| `"arse at abercrombie.ac"` | `"**** at ****"` |
| `"arsenal"` | `"arsenal"` (unchanged — word boundary) |

### Regex Patterns

**Bad Word Pattern:**
```
\b(word1|word2|...)\b
```
- Options: `IgnoreCase | Compiled`
- Entries from `bad.txt`, each `Regex.Escape`d

**URL Pattern:**
```
\b(host1|host2|...)\.(tld1|tld2|...)\b
```
- Options: `IgnoreCase | Compiled`
- Hosts from `hosts.txt`, TLDs from `tlds.txt`, each `Regex.Escape`d

## Static Initialization

Static fields are initialized on first access (lazy via .NET runtime):

1. `LoadWordList("bad.txt")` → reads embedded resource `NuciText.Censorship.English.Data.bad.txt`
2. `LoadWordList("hosts.txt")` → reads embedded resource `NuciText.Censorship.English.Data.hosts.txt`
3. `LoadWordList("tlds.txt")` → reads embedded resource `NuciText.Censorship.English.Data.tlds.txt`
4. `BuildBadWordPattern()` → compiles regex from bad words
5. `BuildUrlPattern()` → compiles regex from hosts + TLDs

## Thread Safety

- Static regex fields are initialized once by .NET runtime (thread-safe)
- `Regex.Replace` is thread-safe for read-only patterns
- No mutable instance state
- Safe for concurrent use across threads

## Related Documentation

- [components/application-services.md](./components/application-services.md) — Implementation detail
- [flows/censorship-execution.md](./flows/censorship-execution.md) — Execution flow
- [behaviour/censor-text.md](./behaviour/censor-text.md) — User-facing behaviour
- [data-model.md](./data-model.md) — Word list data model