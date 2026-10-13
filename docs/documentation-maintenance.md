# Documentation Maintenance

## Ownership

| Document | Owner | Review Cadence |
|----------|-------|----------------|
| Root ARCHITECTURE.md | Horațiu Mlendea | Per release |
| Root SECURITY.md | Horațiu Mlendea | Per release |
| Root PRIVACY.md | Horațiu Mlendea | Per release |
| Root README.md | Horațiu Mlendea | Per release |
| docs/INDEX.md | Horațiu Mlendea | Per release |
| docs/architecture.md | Horațiu Mlendea | Per release |
| docs/components/*.md | Horațiu Mlendea | Per release |
| docs/flows/*.md | Horațiu Mlendea | Per release |
| docs/api-reference/*.md | Horațiu Mlendea | Per release |
| docs/behaviour/*.md | Horațiu Mlendea | Per release |

## Update Triggers

Update documentation when:

- [ ] Public API changes (interface, class, method signatures)
- [ ] Behaviour changes (censorship rules, matching logic)
- [ ] New dependencies added or removed
- [ ] Configuration model changes
- [ ] Error handling changes
- [ ] Security model changes
- [ ] Performance characteristics change
- [ ] New test cases added that reveal new behaviour
- [ ] Word lists significantly modified

## Update Process

1. **Identify affected documents** using the cross-reference tables in each file
2. **Make changes** to source files and documentation in same commit
3. **Verify links** are valid (relative paths from `docs/` root)
4. **Run tests** to ensure behaviour matches documentation
5. **Update last reviewed date** in `docs/INDEX.md`

## Link Verification

All cross-references use relative paths from `docs/` root:

```markdown
[Architecture](./architecture.md)
[Components/Application Services](./components/application-services.md)
[Flows/Censorship Execution](./flows/censorship-execution.md)
```

## Documentation Standards

| Standard | Rule |
|----------|------|
| File naming | kebab-case (`state-and-persistence.md`) |
| Directory naming | kebab-case, plural (`components/`, `flows/`) |
| Headings | Sentence case (`## Repository purpose`) |
| Code symbols | Backticks with full namespace (``Namespace.Class.Method``) |
| Cross-references | Relative links from `docs/` root |

## Root Document Integration

- Reference root `ARCHITECTURE.md`, `SECURITY.md`, `ROADMAP.md`, `PRIVACY.md`, `LICENSE` from `INDEX.md` and relevant `docs/` files
- Do not duplicate root document content under `docs/`
- Complement, don't replicate: `docs/architecture.md` elaborates on root `ARCHITECTURE.md`; `docs/security.md` elaborates on root `SECURITY.md`

## Related Documentation

- [INDEX.md](./INDEX.md) — Master index with maintenance metadata
- [Root ARCHITECTURE.md](../ARCHITECTURE.md) — Root architecture documentation
- [Root README.md](../README.md) — Project overview