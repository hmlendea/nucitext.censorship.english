# Security

This document complements the root [SECURITY.md](../SECURITY.md) by providing implementation-specific security details.

## Security Model

The library operates as a stateless, in-process text processor with no external dependencies at runtime.

### Trust Boundaries

| Boundary | Description |
|----------|-------------|
| **Host Application → Library** | Host passes untrusted input text to `Censor` method |
| **Library → Host Application** | Library returns censored text; no data exfiltration |
| **Library → Embedded Resources** | Library reads trusted, compile-time embedded word lists |

### Threat Model

| Threat | Mitigation |
|--------|------------|
| **ReDoS via malicious input** | Input is matched against pre-compiled regex of escaped literals; no user-controlled regex |
| **ReDoS via malicious word list** | Word lists are embedded resources (trusted); `Regex.Escape` applied as defence-in-depth |
| **Data exfiltration** | No network calls, file I/O, or logging; input never leaves process |
| **Information disclosure** | No secrets, credentials, or sensitive data in library |
| **Denial of service via large input** | Regex matching is O(n); host responsible for input size limits |

## Vulnerability Reporting

See root [SECURITY.md](../SECURITY.md) for reporting process.

## Scope

### In Scope

- Code execution via crafted input
- Denial of service via resource exhaustion
- Data exposure or information disclosure

### Out of Scope

- Issues in third-party dependencies (report to dependency maintainer)
- Social engineering or phishing attacks
- Physical attacks or local privilege escalation
- Vulnerabilities in host applications consuming this library

## Implementation Security Controls

| Control | Implementation |
|---------|----------------|
| **Input validation** | None (by design); regex matching handles all input |
| **Output encoding** | Not applicable (returns plain text) |
| **Authentication** | None (stateless library) |
| **Authorization** | None (stateless library) |
| **Encryption** | None (no sensitive data) |
| **Audit logging** | None (host responsibility) |
| **Rate limiting** | None (host responsibility) |

## Secure Development Practices

- `Regex.Escape` applied to all word list entries
- No `eval`, `dynamic`, or reflection-based execution
- No deserialization of untrusted data
- No file system or network access
- Sealed class prevents inheritance-based attacks
- No mutable state prevents state confusion attacks

## Related Documentation

- [Root SECURITY.md](../SECURITY.md) — Security policy, vulnerability reporting
- [Root PRIVACY.md](../PRIVACY.md) — Data handling
- [error-handling.md](./error-handling.md) — Error handling
- [invariants.md](./invariants.md) — System invariants