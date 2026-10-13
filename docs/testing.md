# Testing

## Test Strategy

| Level | Framework | Scope | Location |
|-------|-----------|-------|----------|
| Unit | NUnit 4.x | Public `ITextCensor` contract | `NuciText.Censorship.English.UnitTests/EnglishTextCensorTests.cs` |

## Test Organisation

```
NuciText.Censorship.English.UnitTests/
└── EnglishTextCensorTests.cs
    ├── GivenTextWithBadWord_WhenCensoring_ThenBadWordIsReplaced
    ├── GivenTextWithUppercaseBadWord_WhenCensoring_ThenBadWordIsReplaced
    ├── GivenTextWithMultipleBadWords_WhenCensoring_ThenAllBadWordsAreReplaced
    ├── GivenCleanText_WhenCensoring_ThenTextIsReturned
    ├── GivenTextWithUrl_WhenCensoring_ThenUrlIsReplaced
    ├── GivenBadWordAsPartOfLargerWord_WhenCensoring_ThenLargerWordIsUnchanged
    └── GivenTextWithBadWordAndUrl_WhenCensoring_ThenBothAreCensored
```

## Test Cases

| Test | Input | Expected Output | Verifies |
|------|-------|-----------------|----------|
| Bad word replacement | `"You arse!"` | `"You ****!"` | Basic bad word censorship |
| Case insensitivity | `"ARSE"`, `"Arse"`, `"aRsE"` | `"****"` | Case-insensitive matching |
| Multiple bad words | `"arse and anal"` | `"**** and ****"` | Global replacement |
| Clean text pass-through | `"Hello, World!"` | `"Hello, World!"` | No false positives |
| URL replacement | `"Visit abercrombie.ac now."` | `"Visit **** now."` | Host.tld pattern matching |
| Substring protection | `"arsenal"`, `"analysis"`, `"classical"` | Unchanged | Word boundary matching |
| Combined censorship | `"arse at abercrombie.ac"` | `"**** at ****"` | Sequential replacement |

## Running Tests

```bash
# Run all tests
dotnet test NuciText.Censorship.English.slnx

# Run with verbosity
dotnet test --verbosity normal

# Run specific test project
dotnet test NuciText.Censorship.English.UnitTests/
```

## Coverage

| Area | Covered | Notes |
|------|---------|-------|
| Bad word censorship | ✅ | Multiple test cases |
| Case insensitivity | ✅ | Parameterized tests |
| Multiple matches | ✅ | Single test case |
| URL censorship | ✅ | Parameterized tests |
| Clean text | ✅ | Multiple test cases |
| Word boundaries | ✅ | Substring protection tests |
| Combined censorship | ✅ | Single test case |
| Empty string | ✅ | Included in clean text tests |
| Null input | ❌ | Not tested (throws `ArgumentNullException`) |
| Performance | ❌ | No load/performance tests |
| Fuzz testing | ❌ | Not applicable (escaped literals) |

## Material Coverage Gaps

1. **Null input handling** — Not explicitly tested; relies on .NET framework behaviour
2. **Integration tests** — Not applicable (library has no host dependencies)
3. **Performance/load tests** — Not included; stateless, CPU-bound
4. **Fuzz testing** — Not needed; patterns are simple alternations of escaped literals

## Test Framework Details

| Component | Version |
|-----------|---------|
| NUnit | 4.5.1 |
| NUnit3TestAdapter | 6.2.0 |
| Microsoft.NET.Test.Sdk | 18.3.0 |

## CI Integration

Tests run automatically on push/PR to `master` via `.github/workflows/dotnet.yml`:

```yaml
- name: Test
  run: dotnet test --no-build --verbosity normal
```

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Testing and verification section
- [components/application-services.md](./components/application-services.md) — Implementation under test
- [behaviour/censor-text.md](./behaviour/censor-text.md) — Behaviour verified by tests