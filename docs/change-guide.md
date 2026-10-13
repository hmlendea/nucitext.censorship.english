# Change Guide

## How to Modify Common Areas Safely

### 1. Adding/Removing Bad Words

**File:** `NuciText.Censorship.English/Data/bad.txt`

**Steps:**
1. Add or remove entries (one per line)
2. Rebuild: `dotnet build NuciText.Censorship.English`
3. Run tests: `dotnet test NuciText.Censorship.English.slnx`
4. Bump version in `.csproj` if behaviour changes
5. Pack: `dotnet pack -c Release`

**Impact:**
- Changes to `bad.txt` affect `BadWordPattern` regex
- No API changes required
- Tests may need updating if new words affect existing test cases

### 2. Adding/Removing Hostnames

**File:** `NuciText.Censorship.English/Data/hosts.txt`

**Steps:**
1. Add or remove entries (one per line)
2. Rebuild and test (same as above)
3. Bump version and pack

**Impact:**
- Changes to `hosts.txt` affect `UrlPattern` regex
- No API changes required

### 3. Adding/Removing TLDs

**File:** `NuciText.Censorship.English/Data/tlds.txt`

**Steps:**
1. Add or remove entries (one per line)
2. Rebuild and test (same as above)
3. Bump version and pack

**Impact:**
- Changes to `tlds.txt` affect `UrlPattern` regex
- No API changes required

### 4. Changing Replacement Token

**File:** `NuciText.Censorship.English/EnglishTextCensor.cs`

**Location:** `CensoredWordReplacement` property

**Steps:**
1. Modify the return value
2. Update tests that assert on `****`
3. Rebuild and test
4. Bump version (minor or major depending on impact)

**Impact:**
- Affects all censored output
- Breaking change if consumers depend on `****` format

### 5. Changing Regex Options

**File:** `NuciText.Censorship.English/EnglishTextCensor.cs`

**Location:** `BuildBadWordPattern()` and `BuildUrlPattern()`

**Steps:**
1. Modify `RegexOptions` flags
2. Rebuild and test
3. Bump version

**Impact:**
- May affect matching behaviour (case sensitivity, word boundaries)
- Review all test cases

### 6. Adding New Censorship Logic

**File:** `NuciText.Censorship.English/EnglishTextCensor.cs`

**Steps:**
1. Add new private method or modify `Censor`
2. Add tests for new behaviour
3. Rebuild and test
4. Bump version

**Impact:**
- May affect performance characteristics
- Review invariants and error handling

## Version Bump Policy

| Change Type | Version Bump |
|-------------|--------------|
| Bug fix in word list | Patch |
| New word added | Patch |
| Word removed | Minor |
| Replacement token changed | Major |
| Regex options changed | Minor/Major |
| New censorship logic | Minor |
| API change | Major |

## Pre-Change Checklist

- [ ] Run existing tests: `dotnet test`
- [ ] Verify no regressions in censorship behaviour
- [ ] Update tests if behaviour changes
- [ ] Update documentation if behaviour changes
- [ ] Bump version in `.csproj`
- [ ] Verify build: `dotnet build`
- [ ] Verify pack: `dotnet pack -c Release`

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Extension points
- [design-decisions.md](./design-decisions.md) — Design rationale
- [testing.md](./testing.md) — Test strategy
- [build-and-deployment.md](./build-and-deployment.md) — Build and deployment