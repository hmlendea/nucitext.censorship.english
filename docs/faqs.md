# Frequently Asked Questions

## General

### What does this library do?

NuciText.Censorship.English censors English text by replacing offensive words and known unwanted domain names with asterisks (`****`).

### How do I use it?

```csharp
using NuciText.Censorship.English;

ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor("You arse!"); // Returns "You ****!"
```

### What NuGet package do I need?

```bash
dotnet add package NuciText.Censorship.English
```

### What .NET version is required?

.NET 10.0 or later.

## Behaviour

### Is matching case-sensitive?

No, all matching is case-insensitive.

### Does it match partial words?

No, it uses word boundaries (`\b`) to avoid false positives. "arse" matches but "arsenal" does not.

### What gets replaced?

1. Bad words from `bad.txt` (offensive English words)
2. URLs matching host.tld patterns from `hosts.txt` + `tlds.txt`

### What is the replacement token?

All matches are replaced with `****` (four asterisks).

### Can I customize the word lists?

Not at runtime. Word lists are embedded resources. To customize, fork the repository, modify `Data/bad.txt`, `Data/hosts.txt`, or `Data/tlds.txt`, rebuild, and publish as a separate package.

### Does it support other languages?

No, English only. Other languages require separate packages implementing `ITextCensor`.

## Technical

### Is it thread-safe?

Yes. Static regex instances are immutable after initialization; `Regex.Replace` is thread-safe.

### Does it have any dependencies?

- NuciText.Censorship (provides `ITextCensor` interface)
- .NET 10.0+ BCL (System.Text.RegularExpressions, System.Reflection, System.IO)

### Does it log anything?

No. The library emits no logs, metrics, or traces.

### Does it make network calls?

No. All processing is in-memory with embedded resources.

### Is it async?

No. The `Censor` method is synchronous. Host applications can wrap in `Task.Run` if needed.

### What happens with null input?

`ArgumentNullException` is thrown (from `Regex.Replace`).

## Licensing

### What license?

GPL-3.0-or-later. Viral copyleft; host applications must comply if distributed.

## Troubleshooting

### My word isn't being censored

1. Check if the word is in `bad.txt` (case-insensitive)
2. Verify it's not part of a larger word (word boundaries apply)
3. Rebuild the library after modifying word lists

### URLs aren't being censored

1. Check if the hostname is in `hosts.txt`
2. Check if the TLD is in `tlds.txt`
3. Verify the format matches `host.tld` at word boundaries

### Performance is slow

1. First call incurs ~1-5ms for static initialization
2. Subsequent calls are O(n) in input length
3. Profile your host application if needed

## Related Documentation

- [quick-start.md](./quick-start.md) — Getting started guide
- [behaviour/censor-text.md](./behaviour/censor-text.md) — Behaviour details
- [troubleshooting.md](./troubleshooting.md) — Common issues