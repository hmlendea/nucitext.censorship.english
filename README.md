[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.censorship.english)](https://github.com/hmlendea/nucitext.censorship.english/releases/latest)
[![Build Status](https://github.com/hmlendea/nucitext.censorship.english/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship.english/actions/workflows/dotnet.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# NuciText.Censorship.English

An English text censorship filter for [NuciText.Censorship](https://github.com/hmlendea/nucitext.censorship). Replaces bad words and known unwanted domain names with asterisks.

## ✨ Features

- Replaces offensive English words with `****`
- Replaces known unwanted or malicious domain names with `****`
- Case-insensitive matching
- Word-boundary aware, avoiding false positives from partial word matches

## 🚀 Usage

```csharp
using NuciText.Censorship.English;

ITextCensor censor = new EnglishTextCensor();
string result = censor.Censor("You arse!"); // Returns "You ****!"
```

## 📦 Installation

[![Get it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Censorship.English)

### .NET CLI

```bash
dotnet add package NuciText.Censorship.English
```

### Package Manager Console

```powershell
Install-Package NuciText.Censorship.English
```

## 🛠️ Development

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

All NuGet dependencies are restored automatically by `dotnet restore`.

### Build

```bash
dotnet build NuciText.Censorship.English
```

### Test

```bash
dotnet test NuciText.Censorship.English.slnx
```

### Release

```bash
dotnet pack NuciText.Censorship.English -c Release
```

### Dependencies

| Package | Purpose |
|---------|---------|
| [NuciText.Censorship](https://nuget.org/packages/NuciText.Censorship) | Provides the `ITextCensor` interface implemented by this package |

## 🤝 Contributing

Contributions are welcome. Please:
- Keep the changes cross-platform
- Keep the existing public contract intact unless a breaking change is intentional
- Keep the pull requests focused and consistent with the existing code style
- Update the documentation when behaviour changes
- Add unit tests for any new or changed functionality

## 💝 Support

Found a bug or have a suggestion? [Open an issue](https://github.com/hmlendea/nucitext.censorship.english/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or giving a star on GitHub!

## 🔗 Related Projects

- [NuciText.Censorship](https://github.com/hmlendea/nucitext.censorship): The base censorship abstractions and `ITextCensor` interface that this package implements

## 📄 Licence

Licensed under the [GNU General Public Licence v3](https://gnu.org/licenses/gpl-3.0) or later.
See [LICENSE](./LICENSE) for details.
