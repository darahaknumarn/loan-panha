# pheap (SoPheap client) onto the Panha codebase

Goal: run the single Panha build (`Panha/KTV/Morokot.vbproj`) against the SoPheap
client's database `pheap`, so the `SoPheap/` source fork can be retired.

**Status: applied on 2026-10-03 to `Dara-Desktop\SQLEXPRESS`** (`pheap`, `Temppheap`, and
`sp_repay1` on `Panha`). Export and a rolled-back head-office import were run end to end
against real data afterwards and both succeeded.

Compared beforehand:

- **Tables**: `pheap` and `Panha` have identical table schemas (70 tables, no column
  differences). The `Writeoff` table and the `PayOff` / `Ref` columns on `BK_Loan` that
  the Panha code uses are already present in `pheap`.
- **Procedures**: 13 modules differed. `APPLY-pheap.sql` changes 12 of them (below) and
  `APPLY-Panha-sp_repay1.sql` brings the 13th to `Panha`.

## Files and run order

1. `CREATE-Temppheap.sql` (sysadmin, on master) - creates the staging database the Panha
   build's Export / Import screens target for this client, plus `dbo.SYS_IMPORT_EXPORT`.
   `CREATE-SYS_IMPORT_EXPORT.sql` does only the table part for any other staging database
   (`-v STAGING=TempPanha`).
2. `APPLY-pheap.sql` - alters the 12 procedures. `ROLLBACK-pheap.sql` restores the
   bodies `pheap` had before (copies in `rollback/pheap.*.sql`).
3. `APPLY-Panha-sp_repay1.sql` - replaces `Panha.dbo.sp_repay1` with pheap's body
   (rollback body in `rollback/Panha.sp_repay1.sql`).
4. Add a `Connections\<name>.txt` for `pheap` and sign in with the Panha build.

**Always run these with `sqlcmd ... -f 65001`** (or from SSMS). The files are UTF-8 and
contain Khmer string literals (`N'រៀល'`, `N'បង់ផ្ដាច់'`); without `-f 65001` sqlcmd reads
them as ANSI and the procedures are stored with garbled literals, which silently breaks
the currency and payoff comparisons.

```
sqlcmd -S <server> -E -C -f 65001 -i CREATE-Temppheap.sql
sqlcmd -S <server> -E -C -f 65001 -i APPLY-pheap.sql
sqlcmd -S <server> -E -C -f 65001 -i APPLY-Panha-sp_repay1.sql
```

## What APPLY-pheap.sql changes and why

| Procedure | pheap had | Now | Reason |
|---|---|---|---|
| SP_DELETE, SP_DELETE1, SP_DELETE2, SP_MAIN_EXPORT, SP_SHRINK_TEMPDATA | generic `Data` / `TempData` names | `pheap` / `Temppheap` | same fix as APPLY-Panha.sql, generated with `generate.pl` |
| SP_IMPORT | `Data` / `TempData`; stale column lists; `SELECT *` into identity tables | `pheap` / `Temppheap`; every INSERT names its columns from the live schema | would not even compile against the real tables (see below) |
| SP_EXPORT | `Morokot` / `TempMorokot` (another client's databases); `SELECT *` into identity tables | Panha body with `pheap` / `Temppheap`; explicit columns + `SET IDENTITY_INSERT`; fills `SYS_IMPORT_EXPORT` | export pointed at the wrong databases and could not compile |
| sp_rptGetLoanPaid | 5 parameters | 6 parameters (write-off filter) | `frmResultReport.vb` passes 6 |
| sp_rptEndBalSumByEndDay | branch filter `=` only, exchange not filtered by branch, USD total used the KHRTOUSD column | Panha body | supports `All`, fixes the USD total |
| sp_GetLoanToWriteoff | principal paid summed from the schedule | summed from actual repayments (`Prn`), left joins | correct outstanding for write-off |
| sp_GetListLoanWriteoff | inner joins, no `CM_ID1` match | left joins + `CM_ID1` | matches Panha write-off screen |
| spGetLoanRepayDetailAudit | one row per repayment | grouped per schedule date | avoids duplicate schedule rows in the audit grid |

Whitespace-only differences (sp_CountAsset, sp_rptEndBalSumByEndDay1, both
BK_SavingRepay triggers) are left alone.

### The import/export procedures were broken for every client

Applying the generated SP_IMPORT / SP_EXPORT to a database where the staging database
really exists exposed problems that the `Data` / `TempData` versions had hidden (SQL Server
defers name resolution when the referenced database is missing, so they "compiled"):

- `INSERT ... SELECT *` into tables with an identity column (`BK_LoanRepay`,
  `OwnerTransaction`, every `TRACE_*` table, ...) is a compile error.
- SP_IMPORT's hand-written column lists were out of date: `TRACE_Customer` named nine
  columns that do not exist, and `BK_LoanRepay` / `TRACE_Loan` / `ExpenseOperation` omitted
  columns that do (`Prn`, `Int`, `LR_Service`, `Mark`, `PayOff`, `Ref`, `InNo`, ...), so an
  import would have silently dropped that data at head office.
- SP_DELETE1 reads the export date and branch from `<staging>.dbo.SYS_IMPORT_EXPORT`, a
  table that existed in no database on the server, and the two `UPDATE`s in SP_EXPORT that
  fill it were commented out. Every import therefore failed at that step.
- The schema has two tables that differ only by case, `TRACE_OtherIncome` and
  `Trace_OtherIncome`, with different columns. Both are exported.

`fix-insert-lists.py` regenerates every `INSERT ... SELECT` in a generated body from the
live column metadata (`sys.columns` dumped to a TSV). Export keeps identity values via
`SET IDENTITY_INSERT`; import drops identity columns so head office assigns new ids, except
`OwnerTransaction`, whose `OPID` is the key SP_DELETE1 / SP_DELETE2 join on.

**`Panha` / `TempPanha` need the same three fixes** (explicit column lists, the
`SYS_IMPORT_EXPORT` table, and the two `UPDATE`s in SP_EXPORT) before a Panha branch export
or head-office import can work on this server. `APPLY-Panha.sql` as applied on 2026-09-11
still has the old bodies.

## Decisions still open

1. **sp_rptProfit** - pheap uses `left join` on the expense temp tables (shows the row
   even with no expenses), Panha uses `inner join`. pheap's is safer. Also a label:
   row 12 is "Insurance Fee Income" in Panha, "Saving Fee Income" in pheap.
2. **Terminology** - `sp_rptLoanDisbursment` aliases `LD_InRate` as InsuranceRate (Panha)
   vs SavingRate (pheap). Column headers in the app say ថ្លៃធានា (insurance). If SoPheap
   calls this "saving", this should become a setting, not a code fork.
3. **sp_rptTotalAsset** - pheap orders by `Convert(Int, ASID)`; fails if an ASID is not
   numeric. Panha orders by the string. Left as Panha.
4. **Eight report procedures the Panha code calls exist in neither database**:
   sp_rptWF, sp_rptAssetPaidOff, sp_rptLoanDisbursmentByCO, sp_rptLoanDisbursmentByBrand,
   sp_rptSumIncomeByCO, sp_rptSumIncomeByBrand, sp_rptWriteOffSummaryByCO,
   sp_rptWriteOffSummaryByBranch. Those menu items error today on Panha as well. They
   need to be written (or recovered from the production Panha server) or the menus hidden.
5. **The local `Panha` database has not had APPLY-Panha.sql applied** - its SP_DELETE /
   SP_IMPORT / SP_MAIN_EXPORT still reference `Data` / `TempData`.

Resolved: **sp_repay1** - pheap's body (over-payment fix from commit 4f85f33) is now on
both databases.
