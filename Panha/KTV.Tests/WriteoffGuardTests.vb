Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Unit tests for the write-off disbursement block. No database is touched here.
<TestClass()>
Public Class WriteoffGuardTests

    Private Shared Function Rec(ByVal id As String, ByVal d As Date, ByVal amt As Double, ByVal cu As Integer, Optional ByVal cm As String = Nothing) As WriteoffGuard.WriteoffRecord
        Return New WriteoffGuard.WriteoffRecord With {.LoanId = id, .WriteoffDate = d, .Amount = amt, .Currency = cu, .CustomerId = cm}
    End Function

    <TestMethod()>
    Public Sub HistorySql_joins_writeoff_to_the_customers_loans_in_the_branch()
        Dim sql = WriteoffGuard.BuildHistorySql("299", "001")
        StringAssert.Contains(sql, "from Writeoff w inner join BK_Loan l on l.LD_ID = w.LD_ID and l.LD_BrId = w.BR_ID")
        StringAssert.Contains(sql, "l.CM_ID = '299'")
        StringAssert.Contains(sql, "l.LD_BrId = '001'")
        StringAssert.Contains(sql, "select distinct")
    End Sub

    <TestMethod()>
    Public Sub HistorySql_also_matches_another_code_with_the_same_name_and_place()
        Dim sql = WriteoffGuard.BuildHistorySql("299", "001")
        StringAssert.Contains(sql, "or exists (")
        StringAssert.Contains(sql, "n.CM_ID = '299'")
        StringAssert.Contains(sql, "replace(c.CM_KhName, ' ', '') = replace(n.CM_KhName, ' ', '')")
        StringAssert.Contains(sql, "lo.VL_ID = nl.VL_ID and lo.CN_ID = nl.CN_ID and lo.DT_ID = nl.DT_ID and lo.PV_ID = nl.PV_ID")
    End Sub

    <TestMethod()>
    Public Sub HistorySql_escapes_quotes_in_the_customer_code()
        Dim sql = WriteoffGuard.BuildHistorySql("1' or 1=1 --", "001")
        StringAssert.Contains(sql, "l.CM_ID = '1'' or 1=1 --'")
    End Sub

    <TestMethod()>
    Public Sub PersonSql_matches_by_name_and_the_place_of_the_new_location_row()
        Dim sql = WriteoffGuard.BuildHistoryByPersonSql("សុខ សុភា", "43", "001")
        StringAssert.Contains(sql, "inner join BK_Location nl on nl.LO_ID = '43' and nl.LO_BrID = '001'")
        StringAssert.Contains(sql, "replace(c.CM_KhName, ' ', '') = replace(N'សុខ សុភា', ' ', '')")
        StringAssert.Contains(sql, "lo.PV_ID = nl.PV_ID")
        Assert.IsFalse(sql.Contains("CM_ID = '"), "person lookup must not depend on a customer code")
    End Sub

    <TestMethod()>
    Public Sub PersonSql_escapes_quotes_in_the_name()
        Dim sql = WriteoffGuard.BuildHistoryByPersonSql("O'Neil", "1", "001")
        StringAssert.Contains(sql, "N'O''Neil'")
    End Sub

    <TestMethod()>
    Public Sub Message_is_empty_when_there_is_no_history()
        Assert.AreEqual("", WriteoffGuard.BuildMessage(New List(Of WriteoffGuard.WriteoffRecord)))
        Assert.AreEqual("", WriteoffGuard.BuildMessage(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Message_names_loan_id_amount_and_date_in_riel()
        Dim msg = WriteoffGuard.BuildMessage(New List(Of WriteoffGuard.WriteoffRecord) From {Rec("1011", #10/22/2020#, 1643200, 1)})
        StringAssert.Contains(msg, "ឥណទានលេខ 1011")
        StringAssert.Contains(msg, "1,643,200 រៀល")
        StringAssert.Contains(msg, "22/10/2020")
        StringAssert.Contains(msg, "Write-off")
    End Sub

    <TestMethod()>
    Public Sub Message_names_the_customer_code_the_loan_was_under()
        Dim msg = WriteoffGuard.BuildMessage(New List(Of WriteoffGuard.WriteoffRecord) From {Rec("1011", #10/22/2020#, 1966600, 1, "299")})
        StringAssert.Contains(msg, "ឥណទានលេខ 1011 (កូដអតិថិជន 299)")
    End Sub

    <TestMethod()>
    Public Sub DuplicateMessage_names_the_person_and_lists_the_loans()
        Dim msg = WriteoffGuard.BuildDuplicateMessage("សុខ សុភា", New List(Of WriteoffGuard.WriteoffRecord) From {Rec("1011", #10/22/2020#, 1966600, 1, "299")})
        StringAssert.Contains(msg, "មិនអាចបង្កើតអតិថិជនថ្មីឈ្មោះ សុខ សុភា")
        StringAssert.Contains(msg, "ឥណទានលេខ 1011")
        Assert.AreEqual("", WriteoffGuard.BuildDuplicateMessage("x", Nothing))
    End Sub

    <TestMethod()>
    Public Sub Message_formats_dollar_amounts_with_cents()
        Dim msg = WriteoffGuard.BuildMessage(New List(Of WriteoffGuard.WriteoffRecord) From {Rec("5", #1/5/2024#, 350.5, 2)})
        StringAssert.Contains(msg, "350.50 ដុល្លារ")
    End Sub

    <TestMethod()>
    Public Sub Message_lists_every_written_off_loan()
        Dim msg = WriteoffGuard.BuildMessage(New List(Of WriteoffGuard.WriteoffRecord) From {
            Rec("10", #12/20/2024#, 214400, 1), Rec("7", #3/1/2021#, 100, 2)})
        StringAssert.Contains(msg, "ឥណទានលេខ 10")
        StringAssert.Contains(msg, "ឥណទានលេខ 7")
    End Sub

End Class
