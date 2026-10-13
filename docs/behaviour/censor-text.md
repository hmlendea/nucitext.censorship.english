# Censor Text Behaviour

## Overview

The `EnglishTextCensor.Censor(string)` method censors English text by replacing:

1. **Bad words** — offensive English words from `bad.txt`
2. **Known unwanted domain names** — host.tld patterns from `hosts.txt` + `tlds.txt`

Both replacements use `****` (four asterisks).

## Matching Rules

| Rule | Detail |
|------|--------|
| **Case insensitive** | `ARSE`, `arse`, `ArSe` all match |
| **Word boundary aware** | `arse` matches but `arsenal` does not |
| **Global replacement** | All occurrences in input are replaced |
| **Sequential** | Bad words replaced first, then URLs |

## Examples

### Bad Word Replacement

| Input | Output |
|-------|--------|
| `"You arse!"` | `"You ****!"` |
| `"You anal person."` | `"You **** person."` |
| `"ARSE"` | `"****"` |
| `"Arse"` | `"****"` |
| `"aRsE"` | `"****"` |
| `"arse and anal"` | `"**** and ****"` |

### URL Replacement

| Input | Output |
|-------|--------|
| `"Visit abercrombie.ac now."` | `"Visit **** now."` |
| `"Go to acmemail.com for details."` | `"Go to **** for details."` |

### Combined Replacement

| Input | Output |
|-------|--------|
| `"arse at abercrombie.ac"` | `"**** at ****"` |

### Clean Text Pass-Through

| Input | Output |
|-------|--------|
| `"Hello, World!"` | `"Hello, World!"` |
| `"Solaire of Astora said hello."` | `"Solaire of Astora said hello."` |
| `"Vasile Ciupitu is a good person."` | `"Vasile Ciupitu is a good person."` |
| `""` | `""` |

### Substring Protection

| Input | Output |
|-------|--------|
| `"arsenal"` | `"arsenal"` (unchanged) |
| `"analysis"` | `"analysis"` (unchanged) |
| `"classical"` | `"classical"` (unchanged) |

## Replacement Token

All replacements use the constant `****` (four asterisks), defined as:

```csharp
private static string CensoredWordReplacement => "****";
```

## Related Documentation

- [api-reference/englishtextcensor.md](./api-reference/englishtextcensor.md) — API reference
- [flows/censorship-execution.md](./flows/censorship-execution.md) — Execution flow
- [components/application-services.md](./components/application-services.md) — Implementation detail