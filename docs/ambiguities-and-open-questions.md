# Ambiguities and Open Questions

## Resolved

### 1. Static Initialization Timing

**Question:** When are regex patterns compiled?

**Answer:** On first access to `BadWordPattern` or `UrlPattern` static fields, which occurs on the first `Censor` call. .NET runtime handles thread-safe lazy initialization.

### 2. Word Boundary Behaviour

**Question:** How does `\b` handle Unicode?

**Answer:** .NET's `\b` is Unicode-aware. Word boundaries are determined by Unicode category data. Behaviour depends on .NET version's Unicode category data.

### 3. Null Input Handling

**Question:** What happens when `text` is null?

**Answer:** `Regex.Replace` throws `ArgumentNullException`. No explicit null check in `Censor` method.

## Open Questions

### 1. Regex Performance with Large Word Lists

**Status:** Unanswered

**Question:** How does regex compilation and matching scale with significantly larger word lists (thousands of entries)?

**Current state:** Word lists are ~200-300 entries each. No performance testing with larger lists.

**Impact:** Could affect startup time and memory usage if lists grow significantly.

### 2. Unicode Word Boundary Edge Cases

**Status:** Unanswered

**Question:** How does `\b` behave with mixed scripts (e.g., English + CJK characters)?

**Current state:** Not tested. .NET's Unicode-aware `\b` may behave unexpectedly with mixed scripts.

**Impact:** Potential false positives or negatives in multilingual contexts.

### 3. Regex Engine Choice

**Status:** Unanswered

**Question:** Would `Regex` with `NonBacktracking` option (if available in .NET 10) improve performance?

**Current state:** Uses standard `Regex` with `Compiled` option.

**Impact:** Potential performance improvement for large inputs.

## Known Gaps

### 1. Null Input Testing

**Gap:** No unit test for null input.

**Reason:** Relies on .NET framework behaviour (`ArgumentNullException` from `Regex.Replace`).

**Recommendation:** Add explicit test if null handling policy changes.

### 2. Performance Testing

**Gap:** No performance or load tests.

**Reason:** Library is stateless, CPU-bound; host application responsible for profiling.

**Recommendation:** Add benchmark tests if performance becomes a concern.

### 3. Fuzz Testing

**Gap:** No fuzz testing of regex patterns.

**Reason:** Patterns are simple alternations of escaped literals; ReDoS not possible.

**Recommendation:** Not needed with current design.

## TODOs

None currently.

## Related Documentation

- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Design constraints section
- [testing.md](./testing.md) — Test coverage gaps
- [components/application-services.md](./components/application-services.md) — Implementation detail