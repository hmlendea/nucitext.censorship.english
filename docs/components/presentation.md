# Presentation Layer

## Public API Surface

The library exposes a single public type implementing a single interface from NuciText.Censorship.

### ITextCensor (External Contract)

**Source:** NuciText.Censorship package

```csharp
namespace NuciText.Censorship
{
    public interface ITextCensor
    {
        string Censor(string text);
    }
}
```

| Member | Description |
|--------|-------------|
| `Censor(string text)` | Returns censored text with bad words and URLs replaced by `****` |

### EnglishTextCensor (Implementation)

**Source:** `NuciText.Censorship.English/EnglishTextCensor.cs`

```csharp
namespace NuciText.Censorship.English
{
    public sealed class EnglishTextCensor : ITextCensor
    {
        public string Censor(string text);
    }
}
```

| Member | Visibility | Description |
|--------|------------|-------------|
| `Censor(string text)` | Public | Implements `ITextCensor.Censor`; replaces bad words and URLs with `****` |
| `EnglishTextCensor()` | Public (implicit) | Default constructor; triggers static initialization on first use |

## Contract Guarantees

| Guarantee | Description |
|-----------|-------------|
| **Thread safety** | Static regex instances are immutable after initialization; `Regex.Replace` is thread-safe |
| **No mutation** | Input string is never modified; new string returned |
| **Null handling** | `ArgumentNullException` if input is null (from `Regex.Replace`) |
| **Idempotency** | Calling `Censor` multiple times on same input produces same output |
| **No side effects** | No I/O, logging, state mutation, or external calls |

## Usage Example

```csharp
using NuciText.Censorship.English;

ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor("You arse!"); // Returns "You ****!"
```

## Related Documentation

- [api-reference/englishtextcensor.md](./api-reference/englishtextcensor.md) — Detailed API reference
- [behaviour/censor-text.md](./behaviour/censor-text.md) — User-facing behaviour
- [flows/censorship-execution.md](./flows/censorship-execution.md) — Execution flow