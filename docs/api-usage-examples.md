# API Usage Examples

## Basic Usage

```csharp
using NuciText.Censorship.English;

ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor("You arse!");
// result: "You ****!"
```

## With Dependency Injection

```csharp
// Program.cs or Startup.cs
services.AddSingleton<ITextCensor, EnglishTextCensor>();

// Consumer
public class CommentService
{
    private readonly ITextCensor _censor;

    public CommentService(ITextCensor censor)
    {
        _censor = censor;
    }

    public string Sanitize(string comment)
    {
        return _censor.Censor(comment);
    }
}
```

## Processing Multiple Texts

```csharp
ITextCensor censor = new EnglishTextCensor();

string[] comments = {
    "This is clean text.",
    "You arse!",
    "Visit abercrombie.ac for more.",
    "arse at acmemail.com"
};

foreach (var comment in comments)
{
    Console.WriteLine(censor.Censor(comment));
}

// Output:
// This is clean text.
// You ****!
// Visit **** for more.
// **** at ****
```

## Handling Null Input

```csharp
ITextCensor censor = new EnglishTextCensor();

string? input = GetUserInput(); // May return null

string result = input != null ? censor.Censor(input) : string.Empty;
// Or with null-conditional:
string result = censor.Censor(input ?? string.Empty);
```

## Batch Processing with Parallelism

```csharp
ITextCensor censor = new EnglishTextCensor();
string[] inputs = GetLargeInputArray();

var results = inputs.AsParallel()
    .Select(censor.Censor)
    .ToArray();
```

## Custom Wrapper for Additional Logic

```csharp
public class EnhancedCensor : ITextCensor
{
    private readonly ITextCensor _inner;

    public EnhancedCensor(ITextCensor inner)
    {
        _inner = inner;
    }

    public string Censor(string text)
    {
        // Pre-processing
        string normalized = text.Trim();

        // Core censorship
        string censored = _inner.Censor(normalized);

        // Post-processing
        return censored;
    }
}

// Usage
ITextCensor censor = new EnhancedCensor(new EnglishTextCensor());
```

## Related Documentation

- [api-reference/englishtextcensor.md](./api-reference/englishtextcensor.md) — API reference
- [behaviour/censor-text.md](./behaviour/censor-text.md) — Behaviour details
- [components/presentation.md](./components/presentation.md) — Public API surface