# Troubleshooting

## Common Issues

### 1. Word Not Being Censored

**Symptom:** Expected bad word passes through unchanged.

**Causes:**
- Word not in `bad.txt`
- Word is part of a larger word (word boundary protection)
- Case mismatch (but matching is case-insensitive)

**Solutions:**
1. Verify word exists in `NuciText.Censorship.English/Data/bad.txt`
2. Check if input contains the word at word boundaries (not as substring)
3. Rebuild library after modifying word lists: `dotnet build`

### 2. URL Not Being Censored

**Symptom:** Expected URL passes through unchanged.

**Causes:**
- Hostname not in `hosts.txt`
- TLD not in `tlds.txt`
- URL format doesn't match `host.tld` pattern at word boundaries

**Solutions:**
1. Verify hostname in `NuciText.Censorship.English/Data/hosts.txt`
2. Verify TLD in `NuciText.Censorship.English/Data/tlds.txt`
3. Check input format: must be `hostname.tld` at word boundaries
4. Rebuild library after modifying word lists

### 3. False Positives (Clean Text Censored)

**Symptom:** Clean text incorrectly censored.

**Causes:**
- Word list entry matches unintended text
- Word boundary behaviour with Unicode

**Solutions:**
1. Review word lists for overly broad entries
2. Test with specific input to identify matching entry
3. Consider removing or refining problematic word list entries

### 4. NullReferenceException

**Symptom:** `NullReferenceException` when calling `Censor`.

**Causes:**
- Embedded resource not found (assembly corruption)
- Resource name mismatch

**Solutions:**
1. Verify `bad.txt`, `hosts.txt`, `tlds.txt` are included as `EmbeddedResource` in `.csproj`
2. Rebuild: `dotnet build`
3. Check assembly with ILSpy or similar to verify resources embedded

### 5. ArgumentNullException

**Symptom:** `ArgumentNullException` when calling `Censor`.

**Cause:** Input text is `null`.

**Solution:** Validate input before calling `Censor`:
```csharp
string result = censor.Censor(input ?? string.Empty);
```

### 6. Performance Issues

**Symptom:** Slow censorship on first call or large inputs.

**Causes:**
- First call incurs static initialization (~1-5ms)
- Very large input strings

**Solutions:**
1. Warm up on startup: `censor.Censor("test");`
2. Profile host application for large input handling
3. Consider input size limits in host application

## Build Issues

### 1. NuGet Package Not Found

**Error:** `NU1101: Unable to find package NuciText.Censorship`

**Solution:**
- Verify NuGet source configuration
- Check package version exists on NuGet.org
- Run `dotnet restore`

### 2. Target Framework Mismatch

**Error:** Project targets `net10.0` but SDK not installed.

**Solution:**
- Install .NET 10 SDK
- Or modify `<TargetFramework>` in `.csproj`

### 3. Tests Not Running

**Error:** No tests found or test adapter issues.

**Solution:**
- Verify `NUnit3TestAdapter` package reference
- Run `dotnet test --verbosity normal` for details
- Check test project targets same framework

## Debugging Tips

### 1. Inspect Compiled Regex

Add temporary debug output:
```csharp
// In EnglishTextCensor.cs (temporary)
Console.WriteLine($"BadWordPattern: {BadWordPattern}");
Console.WriteLine($"UrlPattern: {UrlPattern}");
```

### 2. Test Word List Loading

```csharp
// Temporary test
var badWords = LoadWordList("bad.txt");
Console.WriteLine($"Loaded {badWords.Count()} bad words");
```

### 3. Verify Resource Embedding

```bash
# Check embedded resources in built assembly
dotnet build
# Use ILSpy or dotnet-ildasm to inspect resources
```

## Related Documentation

- [FAQs](./faqs.md) — Common questions
- [change-guide.md](./change-guide.md) — Modifying word lists
- [build-and-deployment.md](./build-and-deployment.md) — Build process
- [components/application-services.md](./components/application-services.md) — Implementation detail