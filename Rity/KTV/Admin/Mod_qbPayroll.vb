Imports System.Data.SqlClient
Module Mod_qbPayroll
    Friend ConnectionString As String = ""
    Friend PhotoPath As New ArrayList
    Friend path As String = ""
    Friend sheetnames As String = ""
    'Friend Adatp As New SqlDataAdapter
    'Friend DS As New DataSet("qbPayrollDataSet")
    Friend cls As New Cls_qbPayroll
    Friend txtUpdate_EmpID As New TextBox
    Friend txtUpdate_SubID As New TextBox
    Friend txtInsur_ID As New TextBox
    Friend txtHN As New TextBox
    Friend _ALUse As New TextBox
    Friend _SLUse As New TextBox
    Friend _ALBalance As New TextBox
    Friend _SLBalance As New TextBox
    Friend _TSpecialL As New TextBox
    Friend _TUnpaidL As New TextBox
    Friend _TFreeHomeL As New TextBox
    Friend _TAnualL As New TextBox
    Friend _TSickL As New TextBox
    Friend _TXTCFAL As New TextBox
    Friend _TXTCFSL As New TextBox
    Friend _Workday As New TextBox
    Friend _LeaveDay As New TextBox
    Friend _SenderText As String = ""
    Friend GridViewTranPlanID As New DataGridView
    Friend TXT_UGID As New TextBox
    Friend TXT_UTID As New TextBox
End Module
