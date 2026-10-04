Imports System.Collections.Generic
Imports System.Text

'Blocks a new disbursement to a customer who already had a loan written off.
'
'A write-off is a row in the Writeoff table keyed by loan, not by customer, and
'it never changes BK_Loan (LD_Status stays as it was, IsWriteoff is never set).
'So the history has to be found by joining Writeoff to the customer's loans in
'the same branch. The requirement is a hard block: the user is told which loan
'was written off and for how much, and the customer code is cleared.
'
'This module only builds the SQL and the message so KTV.Tests can cover it;
'frmDisburshment runs the query and shows the dialog.
Module WriteoffGuard

    Public Const Title As String = "អតិថិជនមានប្រវត្តិលុបបំណុល"

    'One written-off loan of the customer.
    Public Class WriteoffRecord
        Public Property CustomerId As String  'BK_Loan.CM_ID: the code the written-off loan was under
        Public Property LoanId As String
        Public Property WriteoffDate As Date
        Public Property Amount As Double      'Writeoff.LD_OS: outstanding balance written off
        Public Property Currency As Integer   'BK_Loan.CU_ID: 1 = riel, anything else = dollar
    End Class

    Private Const SelectClause As String =
        "select distinct l.CM_ID, w.LD_ID, w.WOF_Date, w.LD_OS, l.CU_ID " &
        "from Writeoff w " &
        "inner join BK_Loan l on l.LD_ID = w.LD_ID and l.LD_BrId = w.BR_ID " &
        "inner join BK_Customer c on c.CM_ID = l.CM_ID and c.CM_BrId = l.LD_BrId " &
        "inner join BK_Location lo on lo.LO_ID = c.LO_ID and lo.LO_BrID = c.CM_BrId "

    Private Const OrderClause As String = " order by w.WOF_Date desc, w.LD_ID desc"

    'Name comparison ignores spaces: Khmer names are often keyed with or without them.
    Private Function SameName(ByVal otherNameExpr As String) As String
        Return "replace(c.CM_KhName, ' ', '') = replace(" & otherNameExpr & ", ' ', '')"
    End Function

    'Same village, commune, district and province as location alias nl.
    Private Function SamePlace(ByVal nl As String) As String
        Return "lo.VL_ID = " & nl & ".VL_ID and lo.CN_ID = " & nl & ".CN_ID and lo.DT_ID = " & nl & ".DT_ID and lo.PV_ID = " & nl & ".PV_ID"
    End Function

    'Every write-off of the customer (CM_ID) in the branch, newest first, plus the
    'write-offs of any other customer code in the branch carrying the same name
    'and the same village/commune/district/province. The second part catches a
    'person re-registered under a new code to get around the block.
    'DISTINCT because Writeoff carries duplicate rows for a few loans.
    Public Function BuildHistorySql(ByVal cmId As String, ByVal brId As String) As String
        Dim cm As String = "'" & SqlEscape(cmId) & "'"
        Dim br As String = "'" & SqlEscape(brId) & "'"
        Return SelectClause &
               "where l.LD_BrId = " & br & " and (l.CM_ID = " & cm & " or exists (" &
               "select 1 from BK_Customer n inner join BK_Location nl on nl.LO_ID = n.LO_ID and nl.LO_BrID = n.CM_BrId " &
               "where n.CM_ID = " & cm & " and n.CM_BrId = " & br & " and n.Status = 'Active' and " &
               SameName("n.CM_KhName") & " and " & SamePlace("nl") & "))" &
               OrderClause
    End Function

    'Every write-off in the branch belonging to a customer with this name living at
    'the place described by location row loId. Used when a new customer is saved,
    'before the code exists, so the person cannot be registered a second time.
    Public Function BuildHistoryByPersonSql(ByVal khName As String, ByVal loId As String, ByVal brId As String) As String
        Dim br As String = "'" & SqlEscape(brId) & "'"
        Return SelectClause &
               "inner join BK_Location nl on nl.LO_ID = '" & SqlEscape(loId) & "' and nl.LO_BrID = " & br & " " &
               "where l.LD_BrId = " & br & " and " &
               SameName("N'" & SqlEscape(khName) & "'") & " and " & SamePlace("nl") &
               OrderClause
    End Function

    Public Function FormatAmount(ByVal amount As Double, ByVal currency As Integer) As String
        If currency = 1 Then
            Return Format(amount, "#,##0") & " រៀល"
        End If
        Return Format(amount, "#,##0.00") & " ដុល្លារ"
    End Function

    'Khmer message naming each written-off loan with its amount and date.
    'Returns "" when there is no history, which means the disbursement may go on.
    Public Function BuildMessage(ByVal records As IList(Of WriteoffRecord)) As String
        If records Is Nothing OrElse records.Count = 0 Then Return ""
        Dim sb As New StringBuilder()
        sb.Append("អតិថិជននេះមិនអាចខ្ចីថ្មីបានទេ ព្រោះធ្លាប់មានឥណទានត្រូវបានលុបបំណុល (Write-off)៖")
        For Each r In records
            sb.AppendLine()
            sb.Append("- ឥណទានលេខ " & r.LoanId)
            If Not String.IsNullOrEmpty(r.CustomerId) Then sb.Append(" (កូដអតិថិជន " & r.CustomerId & ")")
            sb.Append(" ចំនួនទឹកប្រាក់ " & FormatAmount(r.Amount, r.Currency) &
                      " លុបបំណុលថ្ងៃទី " & r.WriteoffDate.ToString("dd/MM/yyyy"))
        Next
        sb.AppendLine()
        sb.Append("សូមកុំផ្តល់ឥណទានថ្មីដល់អតិថិជននេះ។")
        Return sb.ToString()
    End Function

    'Message for the customer form: the person being registered already exists
    'under another code with a write-off, so the new record is refused.
    Public Function BuildDuplicateMessage(ByVal khName As String, ByVal records As IList(Of WriteoffRecord)) As String
        If records Is Nothing OrElse records.Count = 0 Then Return ""
        Return "មិនអាចបង្កើតអតិថិជនថ្មីឈ្មោះ " & khName & " បានទេ ព្រោះមានអតិថិជនឈ្មោះ និងអាសយដ្ឋានដូចគ្នារួចហើយ។" &
               Environment.NewLine & BuildMessage(records)
    End Function

End Module
