# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repository is

A monorepo of ~10 **independent forks of the same application**: a Khmer-language microfinance /
loan-management desktop app (VB.NET WinForms, .NET Framework 4.8, legacy non-SDK `.vbproj`, SQL Server
backend). Each top-level folder is one client/deployment. They share **no code** — a fix in one variant does
not reach any other; porting means copying the changed files into the sibling folder. Always confirm with
the user which variant to work in, and do not touch another variant without being asked.

Two variants carry their own detailed `CLAUDE.md`. **Read the relevant one before doing any real work** —
they cover the build command, startup/auth flow, data-access idiom, domain tables, stored procedures and
gotchas, and nearly all of it applies to every other variant too:

- [Panha/CLAUDE.md](Panha/CLAUDE.md) — the only variant with a test project (`KTV.Tests`, MSTest via
  `dotnet test`), a testable `DbOps` module, and the branch import/export staging-DB work (`sql-migration/`).
- [Rity/CLAUDE.md](Rity/CLAUDE.md) — the variant with the dynamic service-fee work
  (`BK_ServiceRule`, `ufn_GetLoanService`, `KTV/migrations/`) and local source stand-ins for missing
  third-party assemblies.

## Variant map

| Folder | Solution / project | Notes |
|---|---|---|
| `Panha/` | `Panha.sln` → `KTV/Morokot.vbproj` + `KTV.Tests/` | Live DB `Panha`. `Panha/Panha/` is a stale older copy, not built — exclude it from searches. |
| `Rity/` | `Rithy.sln` → `KTV/Morokot.vbproj` | Service-fee migrations under `KTV/migrations/`. |
| `Chet/` | `Chet.sln` → `KTV/Morokot.vbproj` | |
| `ChetPnob/` | `chetpnob.sln` → `KTV/Morokot.vbproj` | |
| `Reaksmey/` | `Morokot.sln` → `KTV/Morokot.vbproj` | |
| `ReasmeyNew/` | `ReakSmey/Reaksmey.sln` → `ReakSmey/KTV/Morokot.vbproj` | One level deeper. |
| `SoPheap/` | `Sopheap.sln` → `KTV/Morokot.vbproj` | |
| `M004 - New1/` | `M004 - New/004.sln` → `M004 - New/KTV/Morokot.vbproj` | One level deeper. |
| `M002 - New - Copy1/` | contains both `M002 - New - Copy/002.sln` and a second `M004 - New/004.sln` | Also has a `.rar` archive. |
| `ProJectOld/` | `BackUpProject/BSokha - BackUp/KTV/` (`Morokot.vbproj`, `BSokha.vbproj`) | Archived backup; not actively developed. |
| `Stock/` | (empty) | |

In every variant the app project is `KTV/Morokot.vbproj` (root namespace `morokot`, assembly
`Loan Management`, startup form `frmsignin`), forms mostly live in `KTV/Admin/`, and the shared helpers are
`KTV/Admin/moremode.vb` and `KTV/Admin/ktvmode.vb`.

## Building (common to all variants)

Run from inside the variant folder, with the app closed and VS not debugging (otherwise MSB3027 file-lock
on `bin\Debug\Loan Management.exe`):

```bash
"C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" KTV/Morokot.vbproj -p:Configuration=Debug
```

Output: `KTV/bin/Debug/Loan Management.exe`. There is no linter and no package manager; every reference is
a file/GAC/COM reference. Two references (`Encrypted.dll`, `Kernel.dll`) point outside the repo and only
resolve because copies are committed under `KTV/bin/Debug/` — never delete those. SpeechLib MSB3305
warnings are normal.

Only `Panha` has tests:

```bash
dotnet test Panha/KTV.Tests/KTV.Tests.vbproj
```

## Repo-wide conventions

- `bin/Debug/` DLLs and `Resources/` images that are already tracked stay tracked (the build depends on
  them); `.gitignore` only hides *new* files there. Use `git add -f` to add a new dependency. `.bak` SQL
  backups and `bin/Debug/BackUp/` are never committed.
- Connection files (`<exe dir>\Connections\*.txt`) hold `server-database-user-password` in plain text and
  at least one is committed — don't echo their contents.
- Database objects (~40 `sp_rpt*` procs, `sp_repay1`) hold most business logic and live only in the
  client's SQL Server, not in the repo. Apply SQL manually via SSMS/`sqlcmd`; there is no migration runner.
  The MCP SQL tool cannot reach the local instance; use `sqlcmd` with `Server=lpc:.\SQLEXPRESS`.
- `Option Strict Off`; forms are used via VB default instances and read each other's controls directly;
  SQL is string-concatenated inline and run over the single global `g_cnn`. Match surrounding style.
- Project files list every source file explicitly — adding a form means adding `.vb`, `.Designer.vb`,
  `.resx` and the matching `<Compile>` / `<EmbeddedResource>` entries to `Morokot.vbproj`.
