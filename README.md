[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.censorship.english)](https://github.com/hmlendea/nucitext.censorship.english/releases/latest)
[![Build Status](https://github.com/hmlendea/nucitext.censorship.english/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship.english/actions/workflows/dotnet.yml)
[![License](https://img.shields.io/github/license/hmlendea/nucitext.censorship.english)](https://github.com/hmlendea/nucitext.censorship.english/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/NuciText.Censorship.English)](https://nuget.org/packages/NuciText.Censorship.English)

# NuciText.Censorship.English

An English text censorship filter for [NuciText.Censorship](https://github.com/hmlendea/nucitext.censorship). Replaces bad words and known unwanted domain names with asterisks.

## 📑 Table of Contents

- [Capabilities](#-capabilities)
- [Usage](#-usage)
- [Installation](#-installation)
- [Development](#️-development)
- [Dependencies](#-dependencies)
- [GitHub Actions](#️-github-actions)
- [Architecture](#️-architecture)
- [Documentation](#-documentation)
- [Troubleshooting](#-troubleshooting)
- [Contributing](#-contributing)
- [Related Projects](#-related-projects)
- [Project Engagement](#-project-engagement)
- [Security](#-security)
- [License](#-license)

## ✨ Capabilities

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

[![Obtain it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Censorship.English)

### Package Manager Installation

```bash
dotnet add package NuciText.Censorship.English
```

Or, via the `Package Manager Console`:
```powershell
Install-Package NuciText.Censorship.English
```

## 🛠️ Development

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10)

All NuGet dependencies are restored automatically by `dotnet restore`.

### Setup

```bash
# Clone the repository
git clone https://github.com/hmlendea/nucitext.censorship.english.git
cd nucitext.censorship.english

# Restore dependencies
dotnet restore
```

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

## 📦 Dependencies

| Package | Version | Scope | Purpose |
|---------|---------|-------|---------|
| [NuciText.Censorship](https://nuget.org/packages/NuciText.Censorship) | 1.0.0 | Runtime | Provides the `ITextCensor` interface implemented by this package |

## ⚙️ GitHub Actions

| Workflow | Purpose | What it does |
|----------|---------|--------------|
| [ dotnet.yml ](https://github.com/hmlendea/nucitext.censorship.english/blob/master/.github/workflows/dotnet.yml) | Continuous integration | Restores dependencies, builds the solution, and runs tests on push and pull requests to master |

## 🏗️ Architecture

NuciText.Censorship.English is a stateless .NET library that implements the `ITextCensor` interface. It loads embedded word lists (`bad.txt`, `hosts.txt`, `tlds.txt`) at static initialization, compiles regex patterns once, and performs case-insensitive, word-boundary-aware censorship on input text.

See the [architecture documentation](./ARCHITECTURE.md) for the system context, principal components, runtime flows, ownership boundaries, dependencies, constraints, and extension points.

## 📚 Documentation

| Resource | Description |
|----------|-------------|
| [Architecture](./ARCHITECTURE.md) | System context, components, runtime flows, and design decisions |
| [Security](./SECURITY.md) | Security policy, vulnerability reporting, and disclosure |
| [Privacy](./PRIVACY.md) | Data handling and privacy practices |
| [Contributing](./CONTRIBUTING.md) | Contribution guidelines and processes |
| [Generated Documentation](./docs/) | API reference, flows, components, and cross-cutting concerns |

## 🩺 Troubleshooting

See [Troubleshooting Guide](./docs/troubleshooting.md) for diagnostic procedures and resolutions to recurring problems.

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Preserve the existing public contract unless a breaking change is intentional
- Submit focused pull requests that conform to the existing code style
- Maintain your branch synchronised with `master`
- Revise the documentation when functionality changes
- Properly test all modifications, including edge cases and error conditions
- Add tests for additional or modified functionality
- Raise a new [issue](https://github.com/hmlendea/nucitext.censorship.english/issues) for problems or suggestions

See the [contributing guidelines](./CONTRIBUTING.md) for details on how to report issues, suggest enhancements, and submit changes.

## 🔗 Related Projects

- [NuciText.Censorship](https://github.com/hmlendea/nucitext.censorship): The base censorship abstractions and `ITextCensor` interface that this package implements

## 💝 Project Engagement

Discovered a problem or have a suggestion? [Open an issue](https://github.com/hmlendea/nucitext.censorship.english/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or starring ⭐️ it on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/funding)

## 🔒 Security

NuciText.Censorship.English has no external dependencies beyond NuciText.Censorship, processes no user data, and performs no network I/O.

For information on reporting security vulnerabilities, see [SECURITY.md](./SECURITY.md).

## 📄 License

This project is being distributed under the `GNU General Public License v3.0 or later`.
See [LICENSE](./LICENSE) for further information.
