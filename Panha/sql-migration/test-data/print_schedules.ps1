# Replays frmDisburshment.toExcel (F11) for each test loan: same two SQL statements,
# same Sample\Schedule.xls template, same cell writes. Each filled sheet is copied into
# one output workbook so all cases can be reviewed together.
param(
    [string]$Template = "E:\loan-panha\Panha\KTV\bin\Debug\Sample\Schedule.xls",
    [string]$Out = "E:\loan-panha\Panha\Schedule-print-from-app.xlsx",
    [string]$Br = "001",
    [int[]]$LoanIds = (900001..900011),
    [string]$OutDir = "E:\loan-panha\Panha\schedule-prints"
)
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory $OutDir | Out-Null }
$ErrorActionPreference = 'Stop'
$cnn = New-Object System.Data.SqlClient.SqlConnection("Server=lpc:.\SQLEXPRESS;Database=Panha;Integrated Security=True;TrustServerCertificate=True")
$cnn.Open()
function SetVal($cell, $v) { [void][System.__ComObject].InvokeMember("Value2", [System.Reflection.BindingFlags]::SetProperty, $null, $cell, @($v)) }
function Fill($sql) { $da = New-Object System.Data.SqlClient.SqlDataAdapter($sql, $cnn); $dt = New-Object System.Data.DataTable; [void]$da.Fill($dt); return ,$dt }

$xl = New-Object -ComObject Excel.Application
$xl.Visible = $false; $xl.DisplayAlerts = $false
$files = @()
foreach ($LD_ID in $LoanIds) {
    # --- header query, verbatim from toExcel ---
    $hdr = Fill("select a.LD_ID,a.CM_ID,b.CM_KhName,c.VL_ID+','+c.CN_ID+','+c.DT_ID+','+c.PV_ID 'CM_Address',a.LD_Unit,LD_Type,convert( varchar(12),LD_Dis_Date,101)LD_Dis_Date,convert( varchar(12),LD_First_Date,101)LD_First_Date,LD_Term,LD_Dis_Amt,LD_IntRate,case when a.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end CU_ID,b.LD_Cycle from BK_Loan a inner join BK_Customer b on a.CM_ID1=b.ID and LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_ID ='$LD_ID' and LD_BrId ='$Br' ")
    if ($hdr.Rows.Count -eq 0) { Write-Warning "loan $LD_ID not found"; continue }
    $h = $hdr.Rows[0]
    $v = @(); for ($k = 0; $k -le 12; $k++) { $v += [string]$h.Item($k) }
    # --- schedule query, verbatim from toExcel ---
    $sql = "select convert( varchar(12),SH_Date,101) SH_Date,case  when DATENAME(WEEKDAY,SH_Date)=N'Monday'then 1 when DATENAME(WEEKDAY,SH_Date)=N'Tuesday' then 2 when DATENAME(WEEKDAY,SH_Date)=N'Wednesday' then 3 when DATENAME(WEEKDAY,SH_Date)=N'Thursday' then 4 when DATENAME(WEEKDAY,SH_Date)=N'Friday' then 5 else 0 end'Day''Day',SH_Prn_Amt+SH_Int_Amt+isnull(SH_Service,0),SH_Prn_Amt,SH_Int_Amt,isnull(SH_Service,0),SH_Balance from BK_LoanSchedule where LD_ID='$LD_ID' and SH_BrId='$Br'"
    $ds = Fill($sql)
    $ct = Fill("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='$LD_ID' and SH_BrId='$Br'")
    $count = [int]$ct.Rows[0].Item(0)

    $book = $xl.Workbooks.Open($Template, $false, $true)
    $ws = $book.Worksheets.Item("Sheet1")
    [void]$ws.Range("A8:A" + ($count + 4)).EntireRow.Insert()
    $ws.Range("C2").Value2 = [string]$LD_ID
    $ws.Range("C3").Value2 = $v[1]
    $ws.Range("B4").Value2 = $v[2]
    $ws.Range("B5").Value2 = $v[3]
    $ws.Range("E2").Value2 = $v[4]
    $ws.Range("E3").Value2 = $v[5]
    $ws.Range("E4").Value2 = $v[6]
    $ws.Range("E5").Value2 = $v[7]
    $ws.Range("G2").Value2 = $v[8]
    $ws.Range("G3").Value2 = $v[9]
    $ws.Range("G4").Value2 = $v[10]
    $ws.Range("G5").Value2 = $v[11]
    $ws.Range("H2").Value2 = "ជំហ៊ានទី: " + $v[12]
    $dayNames = @{ 1 = "ច័ន្ទ"; 2 = "អង្គារ"; 3 = "ពុធ"; 4 = "ព្រហស្បតិ៍"; 5 = "សុក្រ" }
    for ($i = 0; $i -lt $ds.Rows.Count; $i++) {
        for ($j = 0; $j -lt $ds.Columns.Count; $j++) {
            SetVal $ws.Cells.Item($i + 7, $j + 2) ([string]$ds.Rows[$i].Item($j))   # app writes .ToString()
            SetVal $ws.Cells.Item($i + 7, 1) ($i + 1)
        }
        $d = [int]$ws.Cells.Item($i + 7, 3).Value2
        if ($dayNames.ContainsKey($d)) { SetVal $ws.Cells.Item($i + 7, 3) $dayNames[$d] }
    }
    # save the filled template as its own xlsx (what the user would get with Save As after F11)
    $one = Join-Path $OutDir ("Schedule-" + $LD_ID + ".xlsx")
    if (Test-Path $one) { Remove-Item $one -Force }
    $book.SaveAs($one, 51)
    $book.Close($false)
    $files += $one
    Write-Host ("loan {0}: {1} rows -> {2}" -f $LD_ID, $ds.Rows.Count, $one)
}
# combine: all files are xlsx now, so sheet copy works within one instance
$outBook = $xl.Workbooks.Add()
foreach ($f in $files) {
    $b = $xl.Workbooks.Open($f)
    [void]$b.Worksheets.Item("Sheet1").Copy([Type]::Missing, $outBook.Worksheets.Item($outBook.Worksheets.Count))
    $outBook.Worksheets.Item($outBook.Worksheets.Count).Name = "Loan " + [System.IO.Path]::GetFileNameWithoutExtension($f).Replace("Schedule-", "")
    $b.Close($false)
}
while ($outBook.Worksheets.Count -gt $files.Count) { $outBook.Worksheets.Item(1).Delete() }
if (Test-Path $Out) { Remove-Item $Out -Force }
$outBook.SaveAs($Out, 51)
$outBook.Close($false)
$xl.Quit()
[void][System.Runtime.InteropServices.Marshal]::ReleaseComObject($xl)
$cnn.Close()
Write-Host "saved $Out"
