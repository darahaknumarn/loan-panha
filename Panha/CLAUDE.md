# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A Khmer-language microfinance / loan-management desktop app: VB.NET WinForms, .NET Framework 4.8,
legacy (non-SDK-style) `.vbproj` from the VS2012 era, backed by SQL Server.

- Solution `Panha.sln` → `KTV/Morokot.vbproj` (the app) + `KTV.Tests/KTV.Tests.vbproj` (MSTest)
- Root namespace `morokot`, assembly name `Loan Management`, startup form `frmsignin`
  (the running process shows up as **NiTA Solution**)
- Live database for this client is `Panha`; the shared/dev one is `loan`

## Repository layout (important)

The git root is `F:\loan-panha`, which holds ~10 near-identical **forks of the same app**, one per
client/deployment (`Chet`, `ChetPnob`, `M002 - New - Copy1`, `M004 - New1`, `Panha`, `Reaksmey`,
`ReasmeyNew`, `Rity`, `SoPheap`, `Stock`, `ProJectOld`). This working directory is the **Panha**
variant. They share no code — a fix here does **not** reach the others; porting means copying changed
files into the sibling folder. Check with the user before touching another variant. `Rity/CLAUDE.md`
documents the sibling that has the dynamic service-fee work.

`Panha/Panha/` is a stale full copy of an older version of this same app (`Panha.vbproj`). It is not in
the solution and is not built. Don't edit it, and don't let `grep`/`find` results from it mislead you —
exclude `./Panha/` when searching.

Build outputs (`bin/`, `obj/`) and `.suo` files are committed, so builds dirty the working tree.

## Build, run, test

```bash
"C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" KTV/Morokot.vbproj -p:Configuration=Debug
```

**Close the running app (and stop VS debugging) first** — otherwise compilation succeeds but the copy of
`obj\Debug\Loan Management.exe` → `bin\Debug\` fails with MSB3027 (file locked). SpeechLib COM-interop
MSB3305 warnings are normal and harmless.

Run from `KTV/bin/Debug/Loan Management.exe`. Runtime also needs SQL Server, Microsoft ReportViewer 10,
Office Interop (Excel + Outlook) and SpeechLib/VBIDE COM references.

Tests are SDK-style and run on the .NET CLI:

```bash
dotnet test KTV.Tests/KTV.Tests.vbproj
```

```bash
dotnet test KTV.Tests/KTV.Tests.vbproj --filter "ClassName~DbOpsTests"
```

`--filter "Name~Restore_refuses"` selects a single test. There is no linter and no package manager for
the main project — all its references are file/GAC/COM references.

Two of those have `HintPath`s pointing **outside the repository** (`Encrypted.dll` →
`..\..\..\HRMS\...`, `Kernel.dll` → `..\..\..\Projects\...`). Those paths don't exist on this machine;
the build works because copies are committed in `KTV/bin/Debug/`. Don't delete them.

### Test layout

- `KTV.Tests/DbOpsTests.vb` — 21 pure unit tests over `DbOps`, no database touched. This is the only
  code in the repo with real test coverage.
- `KTV.Tests/BackupRestoreIntegrationTests.vb` — end-to-end against a real SQL Server, restoring the
  client's sample `.bak` into a scratch database `TempData_Test` that is dropped afterwards. Overridable
  via `KTV_TEST_SQL` / `KTV_TEST_BAK` env vars; calls `Assert.Inconclusive` and skips when the server or
  sample file is absent, so the unit tests still run on a bare machine.
- The test project **links `..\KTV\Admin\DbOps.vb` as source** rather than referencing the EXE, to keep
  WinForms and the COM interops out of the test host. Any new testable module must be added the same way
  to `KTV.Tests/KTV.Tests.vbproj` (`EnableDefaultCompileItems` is off — files are listed explicitly).

## Startup, connection and authorization flow

1. `frmsignin` lists `*.txt` files under `<exe dir>\Connections\` in a combo box. Each file is one line,
   `server-database-user-password` split on `-`, written by `Frm_Connection`. (Passwords sit in plain
   text there and one such file is committed — don't echo its contents into output.)
2. `ChkCnn()` (`KTV/Admin/moremode.vb`) stores `server` / `DB` / `dbUser` / `dbPwd` and opens the
   **global** `g_cnn As SqlConnection`. Every query in the app reuses that one open connection.
3. Login checks `sys_User`, sets the global `uid`, then shows `frmMain`.
4. `frmMain` is the MDI parent. It reads `sys_UserPrivilege` (`PrivilegeID = 1`) and enables menu items
   whose `ToolStripItem.Tag` matches a returned `MenuID`. Per-action checks use
   `CheckPermission(MenuId, acct, privilegelvl)` (2=New, 3=Update, 4=Delete, 5=Export).
5. `frmMain.lblCode.Text` holds the current **branch/company ID** and is threaded into nearly every
   query as `LD_BrId` / `SH_BrId` / `LR_BrID`.

## Data access idiom

There is no repository/DAL layer. SQL is string-concatenated inline in form event handlers and run
through module-level helpers, all sharing `g_cnn`:

- `KTV/Admin/moremode.vb` — `ChkCnn`, `IsExisted`, `ExecuteDatatable(sql, cnn)`, `CheckPermission`,
  Excel/DataGridView export helpers, and most shared globals (`g_cnn`, `uid`, `DB`, `server`). Also the
  connection-string builders: `CnnString(catalog)`, `MasterCnnString()` (BACKUP/RESTORE must run against
  `master`), `ExportStagingDB()` / `ImportStagingDB()`.
- `KTV/Admin/ktvmode.vb` — `getData(sql)` (scalar), `getDataUni(...)` (parameterized scalar),
  `addIn(sql)` (write), `getImage(sql)`, `AddToListView`, plus `Check_date` / `Check_date1` (advance a
  payment date past weekends and `BK_Holiday` rows).
- `KTV/Admin/DbOps.vb` — the one module written to be testable: pure SQL-building for the
  Backup / Restore / Import / Export screens, free of UI and global state. `SqlEscape`, `QuoteDbName`,
  `GuardStagingTarget`, `BuildBackupSql`, `BuildRestoreSql`, `BuildMoveClauses`, `BuildExportSql`,
  `ReadBackupFileList`, `InstanceDataDir`. Keep new backup/restore logic here, not in the forms — the
  forms (`frm_Backup`, `frm_Restore`, `frmImport`, `frmExport`) only supply values and execute.

Errors are typically caught and shown with `MessageBox.Show(Err.Description, "IT Solution")` and then
swallowed.

## Branch data transfer (import/export)

Branches export their day's work into a staging database and head office imports it. Both sides derive
the staging name as `"Temp" & DB` (`Panha` → `TempPanha`, `loan` → `TempLoan`). `GuardStagingTarget`
exists so a RESTORE can never be aimed at the live database, and `BuildRestoreSql` redirects every file
in the backup into the local instance data directory.

`sql-migration/` holds the fix that made the stored procedures agree with that naming. The procs had been
half-renamed off a generic `Data` / `TempData` pair and pointed at databases that do not exist.

- `generate.pl` rewrites the `Data`/`TempData` qualifiers into `<LIVE>`/`<STAGING>` for a given client and
  turns `CREATE PROCEDURE` into `ALTER PROCEDURE`; it hard-fails if any qualifier is left over.
- `apply/` is generated output, `APPLY-Panha.sql` / `APPLY-loan.sql` are the concatenated runnable
  scripts (applied 2026-09-11), `rollback/` holds the original proc bodies.
- Apply manually via SSMS or `sqlcmd`; there is no migration runner.

## Domain model

Core tables (column prefixes matter when reading queries):

- `BK_Loan` (`LD_*`) — loan header; `LD_Status`, `LD_BrId` = branch
- `BK_LoanSchedule` (`SH_*`) — generated installment schedule; `SH_Date`, `SH_Int_Amt`, `SH_Service`,
  `SH_Balance_Org`
- `BK_LoanRepay` (`LR_*`) — actual repayments against a schedule row
- `BK_Customer` / `BK_CustomerOther` (`CM_*`), `BK_Employee`, `BK_Location`, `BK_Position`, `BK_Company`,
  `BK_Holiday`, `BK_Exchange` (KHR/USD rate)
- `sys_User`, `sys_UserPrivilege`; older `tbl*` tables back the payroll/asset/stationery screens

Key screens: `frmDisburshment.vb` (disburse a loan, generate its `BK_LoanSchedule` rows via `Check_date`),
`frmRepayment.vb` (collections; every path calls `exec sp_repay1`), `frmWiteOff.vb`, `frmReport.vb` +
`frmResultReport.vb` (report parameter picker and result grid/export).

## Stored procedures and SQL

Most reporting logic lives in the database, not the repo: ~40 `sp_rpt*` procedures plus `sp_repay` /
`sp_repay1`, invoked as `exec sp_x '...'` strings. `sp_repay1(@LD_ID, @BrID, @SH_Date, @LR_ID)` is the
central repayment/allocation proc and was the subject of the most recent behavioural fix (over-payment
beyond the scheduled amount). Ad-hoc analysis scripts (`Profit.sql`, `endingBalance.sql`, `listLoan.sql`,
`Int summary.sql`) sit at the variant root.

The MCP SQL tool cannot reach this server; query it with `sqlcmd` over the `lpc:` protocol
(`Server=lpc:.\SQLEXPRESS`).

## Excel reporting

Reports are produced by driving Excel through Interop against template workbooks loaded from
`frmMain.strPath & "\sample\"` and `"\simple Excel\"` (exe directory). Those templates are **not** in the
repo — they ship via the `Setup/Setup.vdproj` installer, so report code cannot be exercised from a bare
clone. `moremode.ExportDatagridViewToExcel1` is the lighter path: it writes an HTML table with an `.xls`
extension.

## Conventions and gotchas

- `Option Strict Off`, `Option Explicit On`, `Option Infer On`. Late binding and implicit conversions are
  pervasive; matching surrounding style is usually right.
- Forms are used via VB **default instances** (`frmMain.Show()`, `frmsignin.txtpass.Text`), never `New`.
  State is passed by reading controls on other forms directly.
- DataGridViews are bound with a blank add-row; code that walks `.Rows` must skip it or hit `DBNull`
  (this was the `frmChangeLocation` crash — bad grid handling, not bad data).
- `Morokot.vbproj` lists every file explicitly — adding a form means adding `.vb`, `.Designer.vb`, `.resx`
  and matching `<Compile>` / `<EmbeddedResource>` entries (with `<DependentUpon>`).
- Most forms live in `KTV/Admin/`; a handful (`frmFirst`, `frmEmployee`, `frmResultReport`) at `KTV/` root,
  and app/splash config under `KTV/My Project/`.
- `moremode.vb` does `Imports Encrypted`, and `Microsoft.VisualBasic.PowerPacks.Vs` / ReportViewer 10 are
  GAC references. This variant has no source stand-ins for any of them (the `Rity` fork does) — it builds
  only because the assemblies are committed under `KTV/bin/Debug/`.
- UI text is Khmer; message-box titles vary ("IT Solution", "NiTA POS Solution", "POS Solution").
- Amounts are handled in both KHR and USD; `BK_Exchange` holds the rate.
