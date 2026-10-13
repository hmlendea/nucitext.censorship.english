# Quick Start

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Installation

```bash
dotnet add package NuciText.Censorship.English
```

## Basic Usage

```csharp
using NuciText.Censorship.English;

ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor("You arse!");
// result: "You ****!"
```

## Dependency Injection (ASP.NET Core)

```csharp
// Program.cs
builder.Services.AddSingleton<ITextCensor, EnglishTextCensor>();

// Controller or Service
public class CommentController : ControllerBase
{
    private readonly ITextCensor _censor;

    public CommentController(ITextCensor censor)
    {
        _censor = censor;
    }

    [HttpPost]
    public IActionResult PostComment([FromBody] string comment)
    {
        string sanitized = _censor.Censor(comment);
        return Ok(sanitized);
    }
}
```

## Run Tests

```bash
dotnet test NuciText.Censorship.English.slnx
```

## Build

```bash
dotnet build NuciText.Censorship.English
```

## Pack (Release)

```bash
dotnet pack NuciText.Censorship.English -c Release
```

## Next Steps

- [Behaviour: Censor Text](./behaviour/censor-text.md) — Understand censorship rules
- [API Reference: EnglishTextCensor](./api-reference/englishtextcensor.md) — Detailed API
- [FAQs](./faqs.md) — Common questions
- [Troubleshooting](./troubleshooting.md) — Common issues

## Related Documentation

- [README.md](../README.md) — Project overview
- [repository-overview.md](./repository-overview.md) — Repository purpose and scope