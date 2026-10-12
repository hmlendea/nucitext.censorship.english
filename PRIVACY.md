# Privacy and Personal Data

This document describes how NuciText.Censorship.English handles personal data. This package is a .NET library that censors English text by replacing bad words and known unwanted domain names with asterisks. It performs all processing locally in memory and does not transmit data to project maintainers or external services.

**Information reviewed:** 2026-10-12

## 📑 Table of Contents

- [What This Document Covers](#-what-this-document-covers)
- [Self-Hosted Deployments](#-self-hosted-deployments)
- [Data We Handle](#-data-we-handle)
- [Processing and Use](#-processing-and-use)
- [Storage, Retention, and Deletion](#-storage-retention-and-deletion)
- [External Processing and Integrations](#-external-processing-and-integrations)
- [Data Protection and Security](#-data-protection-and-security)
- [Document Changes](#-document-changes)
- [Contact](#-contact)

## 🔎 What This Document Covers

This document describes how NuciText.Censorship.English at https://github.com/hmlendea/nucitext.censorship.english handles personal data. It covers the application behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 🏠 Self-Hosted Deployments

This document covers the project maintainers and self-hosted instance operators. The instance operator controls their instance's configuration, local storage, logs, backups, access controls, retention, and request handling unless the project directly controls those functions.

NuciText.Censorship.English is a .NET library consumed by host applications. The library itself does not send data from a self-hosted instance to project maintainers or external services. It does not include telemetry, update checks, crash reports, email, authentication, reverse-proxy, object-storage, or monitoring integrations. Operators can disable any optional data flow by not enabling it in their host application.

## 📥 Data We Handle

### Data Provided to the Application

- No personal data is requested by this library. Host applications pass text to the `Censor` method; any personal data in that text is provided by the host application's users or connected systems.

### Data Generated or Collected by the Application

- No personal data is generated or collected automatically by this library. The library does not write logs, persist data, or emit telemetry.

### Data Received from Integrations

- No personal data is received from integrations or third parties. The library has no built-in external data transfer.

## 🧭 Processing and Use

The application processes the data described above for these verified functions:
- Censoring English text by replacing bad words with asterisks — Data Provided to the Application
- Censoring English text by replacing known unwanted domain names with asterisks — Data Provided to the Application

## 🗄️ Storage, Retention, and Deletion

The library does not store, retain, or delete data. It loads embedded word lists (`bad.txt`, `hosts.txt`, `tlds.txt`) from the assembly at runtime and performs all processing in memory. No local databases, files, logs, caches, or backups are created by this library. For self-hosted deployments, the host application and instance operator control storage, deletion, and backups.

## 🔗 External Processing and Integrations

The application has no built-in external data transfer. It does not integrate with or transmit data to any external services, recipients, or third parties.

| Service or integration | Purpose | Data involved | Configuration or documentation |
|-----------------------|---------|---------------|--------------------------------|

## 🛡️ Data Protection and Security

The library performs all processing in memory and does not persist data. It does not expose secrets, credentials, or sensitive implementation details. For self-hosted deployments, the host application and instance operator are responsible for updates, secrets, access controls, backups, network exposure, and log protection. No absolute security is promised.

## 🔄 Document Changes

Update this document when application data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/nucitext.censorship.english/blob/main/PRIVACY.md.

## 📬 Contact

For questions about application data handling, contact the project maintainers. For a self-hosted instance, contact the instance operator, unless the project explicitly handles the request. Include the package version and host application details if relevant; do not send passwords, access tokens, or other secrets.
