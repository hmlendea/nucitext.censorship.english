# Design Decisions

## Decision Log

### 1. Static Regex Compilation

**Status:** Accepted

**Context:** The library needs to match bad words and URLs against input text efficiently. Regex compilation is expensive.

**Decision:** Compile regex patterns once as static readonly fields (`BadWordPattern`, `UrlPattern`) using `RegexOptions.Compiled | RegexOptions.IgnoreCase`.

**Consequences:**
- First call incurs compilation cost (~ms); subsequent calls are fast
- Patterns are thread-safe after initialization
- Memory held for process lifetime

### 2. Embedded Resources for Word Lists

**Status:** Accepted

**Context:** Word lists must be available at runtime without external file dependencies.

**Decision:** Embed `bad.txt`, `hosts.txt`, `tlds.txt` as assembly manifest resources, loaded via `Assembly.GetManifestResourceStream`.

**Consequences:**
- No file system I/O at runtime
- Word list updates require library rebuild and version bump
- Resource name follows convention `NuciText.Censorship.English.Data.{fileName}`

### 3. Word-Boundary Matching

**Status:** Accepted

**Context:** Bad words like "arse" should not match substrings of larger words like "arsenal".

**Decision:** Use `\b` word boundaries in regex patterns: `\b({alternation})\b` for bad words, `\b({host})\.({tld})\b` for URLs.

**Consequences:**
- Avoids false positives from partial word matches
- `\b` is Unicode-aware in .NET; behaviour depends on .NET version's Unicode category data

### 4. Regex.Escape on All Word List Entries

**Status:** Accepted

**Context:** Word list entries may contain regex metacharacters (e.g., `.`, `-`).

**Decision:** Apply `Regex.Escape` to every entry before building alternation patterns.

**Consequences:**
- Prevents ReDoS from malicious word list content
- Word lists are trusted (embedded resources), but escaping is a defence-in-depth measure

### 5. Sealed Class

**Status:** Accepted

**Context:** The library provides a single, well-defined implementation of `ITextCensor`.

**Decision:** `EnglishTextCensor` is declared `sealed`.

**Consequences:**
- Prevents inheritance-based extension
- Allows JIT optimizations
- Extension via composition/wrapping instead

### 6. No Runtime Configuration

**Status:** Accepted

**Context:** The library should be simple to consume with minimal setup.

**Decision:** All behaviour determined by embedded word lists; no environment variables, config files, or command-line arguments.

**Consequences:**
- Zero configuration for host applications
- Word list updates require library version bump
- No extensibility for custom words without forking

### 7. Synchronous API

**Status:** Accepted

**Context:** The `ITextCensor` interface defines `string Censor(string)` — synchronous.

**Decision:** Implement synchronously; no async/await.

**Consequences:**
- Matches interface contract
- CPU-bound operation; async would add overhead without benefit
- Host application can wrap in `Task.Run` if needed for offloading

### 8. GPL-3.0-or-later License

**Status:** Accepted

**Context:** The library is distributed as a NuGet package.

**Decision:** License under GPL-3.0-or-later.

**Consequences:**
- Viral copyleft; host applications must comply if distributed
- Source code must be made available for modified versions

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Design constraints section
- [components/application-services.md](./components/application-services.md) — Implementation detail
- [configuration.md](./configuration.md) — Configuration model