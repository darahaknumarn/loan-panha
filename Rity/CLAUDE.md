# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A Khmer-language microfinance / loan-management desktop app: VB.NET WinForms, .NET Framework 4.8,
legacy (non-SDK-style) `.vbproj` from the VS2012 era, backed by SQL Server.

- Solution: `Rithy.sln` -> single real project `KTV/Morokot.vbproj`
- Root namespace `morokot`, assembly name `Loan Management`, startup form `frmsignin`
- Default database name is `loan` (see `KTV/migrations/sp_repay1.sql`)

## Repository layout (important)

The git root is `F:\loan-panha`, which contains ~10 near-identical **forks of the same app**, one per
client/deployment: `Chet`, `ChetPnob`, `M002 - New - Copy1`, `M004 - New1`, `Panha`, `Reaksmey`,
`ReasmeyNew`, `Rity`, `SoPheap`, `Stock`, `ProJectOld`. This working directory is the **Rity** variant
(`Rity/Rithy/Rithy`). They share no code — a fix made here does **not** reach the others; porting means
copying the changed files into the sibling folder. Check with the user before touching another variant.

Build outputs (`bin/`, `obj/`) and `.suo` files are committed, so builds dirty the working tree.

## Build and run

There is no test suite, no linter, and no package manager (all references are file/GAC/COM references).

```bash
"C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" Rithy.sln //p:Configuration=Debug
```

Run the built app from `KTV/bin/Debug/Loan Management.exe`.

Two references have `HintPath`s pointing **outside the repository** and are absent on this machine, so a
clean build will fail until they are supplied or removed:

- `Encrypted.dll` -> `..\..\..\HRMS\HCM Soft\myHRSys\bin\Release\Encrypted.dll` (also `Imports Encrypted` in `KTV/Admin/moremode.vb`)
- `Kernel.dll` -> `..\..\..\Projects\Application\Loan Management System\Kernel\bin\Debug\Kernel.dll`

`KTV/ClsEncrypted.vb` and the `Microsoft.VisualBasic.PowerPacks` namespace stub at the bottom of
`KTV/frmFirst.vb` exist as local stand-ins for missing third-party assemblies. Don't delete them.

Runtime also needs: SQL Server, Microsoft ReportViewer 10, Office Interop (Excel + Outlook) and
SpeechLib/VBIDE COM references.

## Startup, connection and authorization flow

1. `frmsignin` lists `*.txt` files under `<exe dir>\Connections\` in a combo box. Each file is one line:
   `server-database-user-password` (split on `-`), written by `Frm_Connection`.
2. `ChkCnn()` (`KTV/Admin/moremode.vb`) builds the connection string and opens the **global**
   `g_cnn As SqlConnection`. Every query in the app reuses this single open connection.
3. Login checks `sys_User` by username/password, sets the global `uid`, then shows `frmMain`.
4. `frmMain` is the MDI parent. It reads `sys_UserPrivilege` (`PrivilegeID = 1`) and enables menu items
   whose `ToolStripItem.Tag` matches a returned `MenuID`. Per-action checks use
   `CheckPermission(MenuId, acct, privilegelvl)` (2=New, 3=Update, 4=Delete, 5=Export).
5. `frmMain.lblCode.Text` holds the current **branch/company ID** and is threaded into nearly every
   query as `LD_BrId` / `SH_BrId` / `LR_BrID`.

## Data access idiom

There is no repository/DAL layer. SQL is string-concatenated inline in form event handlers and executed
through module-level helpers, all sharing `g_cnn`:

- `KTV/Admin/ktvmode.vb` — `getData(sql)` (scalar), `getDataUni(...)` (parameterized scalar),
  `addIn(sql)` (write), `AddToGridOther(...)`, `getImage(sql)`, plus `Check_date` / `Check_date1`
  (advance a payment date past weekends and `BK_Holiday` rows)
- `KTV/Admin/moremode.vb` — `ChkCnn`, `IsExisted(sql)`, `ExecuteDatatable(sql, cnn)`, `CheckPermission`,
  Excel/DataGridView export helpers, and most shared globals (`g_cnn`, `uid`, `DB`, `server`)

Errors are typically caught and shown with `MessageBox.Show(Err.Description, "IT Solution")` and then
swallowed. `AppLogger.LogException(ex, context)` writes to `%APPDATA%\Morokot\Logs\yyyy-MM-dd.log`; it is
currently only wired into `frm_Backup`.

## Domain model

Core tables (column prefixes matter when reading queries):

- `BK_Loan` (`LD_*`) — loan header; `LD_Status` = Active/…, `LD_BrId` = branch
- `BK_LoanSchedule` (`SH_*`) — generated installment schedule; `SH_Date`, `SH_Int_Amt`, `SH_Service`, `SH_Balance_Org`
- `BK_LoanRepay` (`LR_*`) — actual repayments against a schedule row
- `BK_Customer` / `BK_CustomerOther` (`CM_*`), `BK_Employee`, `BK_Location`, `BK_Company`, `BK_Holiday`,
  `BK_Exchange` (KHR/USD), `BK_ServiceRule` (dynamic service-fee rules)
- `sys_User`, `sys_UserPrivilege`; older `tbl*` tables back the payroll/asset/stationery screens

Key screens: `frmDisburshment.vb` (disburse a loan and generate its `BK_LoanSchedule` rows via
`Check_date`), `frmRepayment.vb` (collections; calls `sp_repay1`), `frmWiteOff.vb`, `frmReport.vb` +
`frmResultReport.vb` (report parameter picker and result grid/export).

## Stored procedures and SQL

Most business logic for reporting lives in the database, not the repo: ~40 `sp_rpt*` procedures plus
`sp_repay`/`sp_repay1`, invoked as `exec sp_x '...'` strings. `sp_repay1(@LD_ID, @BrID, @SH_Date, @LR_ID)`
is the central repayment/allocation proc and was the subject of the most recent behavioural fix
(over-payment beyond the scheduled amount).

`KTV/migrations/` is the only SQL kept in-tree:

- `sql/0001_add_service_rule.sql` — creates + seeds `BK_ServiceRule`
- `sql/0002_create_calc_function.sql` — `dbo.ufn_GetLoanService(@LoanAmt, @LoanUnit, @BranchID)`
- `sql/0003_update_sp_repay1.sql` — a **template**, not runnable as-is; it documents how to graft the
  dynamic service fee into the real proc
- `sp_repay1.sql` — a UTF-16 dump of the live `sp_repay1`, already integrated with `ufn_GetLoanService`

Ad-hoc analysis scripts (`Profit.sql`, `endingBalance.sql`, `listLoan.sql`, `Int summary.sql`) sit at the
variant root. Apply migrations manually via SSMS/sqlcmd; there is no migration runner.

## Excel reporting

Reports are produced by driving Excel through Interop against template workbooks loaded from
`frmMain.strPath & "\sample\"` and `"\simple Excel\"` (exe directory). Those templates are **not** in the
repo — they ship via the `Setup/Setup.vdproj` installer, so report code cannot be exercised from a bare
clone. `moremode.ExportDatagridViewToExcel1` is the lighter-weight path: it writes an HTML table with an
`.xls` extension.

## Conventions and gotchas

- `Option Strict Off`, `Option Explicit On`, `Option Infer On`. Late binding and implicit conversions are
  pervasive; matching surrounding style is usually right.
- Forms are used via VB **default instances** (`frmMain.Show()`, `frmsignin.txtpass.Text`), never `New`.
  State is passed by reading controls on other forms directly.
- The project file lists every file explicitly — adding a form means adding `.vb`, `.Designer.vb`, `.resx`
  and matching `<Compile>` / `<EmbeddedResource>` (with `<DependentUpon>`) entries to `KTV/Morokot.vbproj`.
- Most forms live in `KTV/Admin/`, a handful (`frmFirst`, `frmEmployee`, `frmResultReport`) at `KTV/` root,
  and login/splash under `KTV/My Project/`.
- UI text is Khmer; message-box titles vary ("IT Solution", "NiTA POS Solution", "POS Solution").
- Amounts are handled in both KHR and USD; `BK_Exchange` holds the rate.
