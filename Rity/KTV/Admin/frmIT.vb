Public Class frmIT
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Open.Click
        OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើសទីតាំងទិន្នន័យជាមុនសិន មុននឹងធ្វើការបញ្ចូល។", "ជ្រើសរើសទីតាំង")
            Else
                Dim BrID As String = ""
                '-------------- Restore database
                addIn("restore database BarCode1 from Disk='" & Me.TextBox1.Text & "' with replace")
                '-------------- Delete data in table
                BrID = getData("use BARCODE1 select ID from tblcompany1 ")
                Dim name As String = getData("use BARCODE1 select Name from tblcompany1 ")
                '--------------------MessageBox.Show(BrID.ToString)
                addIn("delete from tblOum where BrID='" & BrID & "' delete from tblPosition where BrID='" & BrID & "' delete from BK_Customer where CM_BrId='" & BrID & "'")
                addIn("delete from BK_Location where LO_BrId='" & BrID & "' delete from BK_Loan where LD_BrId='" & BrID & "' delete from tblAsset	where BrID='" & BrID & "'")
                addIn("delete from tblAssetAdd where BrID='" & BrID & "' delete from tblAssetAddDetail where BrID='" & BrID & "' delete from tblSang where BrID='" & BrID & "'")
                addIn("delete from tblCustom where BrID='" & BrID & "' delete from tblStaff where BrID='" & BrID & "' delete from tblResource where BrID='" & BrID & "'")
                addIn("delete from tblAssetDetail where BrID='" & BrID & "' delete from tblCU_Pro where BrID='" & BrID & "' delete from tblTraceAssetDetail where BrID='" & BrID & "'")
                addIn("delete from tblTraceResource where BrID='" & BrID & "' delete from tblTracesang where BrID='" & BrID & "'")
                addIn("delete from tblCollateral where BrID='" & BrID & "' delete from tblCollateral where BrID='" & BrID & "'")
                '---------------Insert into database
                addIn("INSERT INTO BarCode.dbo.tblAsset SELECT * FROM BARCODE1.dbo.tblAsset;")
                addIn("INSERT INTO BarCode.dbo.tblcompany1 SELECT * FROM BARCODE1.dbo.tblcompany1;")
                addIn("INSERT INTO BarCode.dbo.tblAssetAdd SELECT * FROM BARCODE1.dbo.tblAssetAdd;")
                addIn("INSERT INTO BarCode.dbo.tblAssetAddDetail SELECT * FROM BARCODE1.dbo.tblAssetAddDetail;")
                addIn("INSERT INTO BarCode.dbo.tblSang SELECT * FROM BARCODE1.dbo.tblSang;")
                addIn("INSERT INTO BarCode.dbo.tblCustom SELECT * FROM BARCODE1.dbo.tblCustom;")
                addIn("INSERT INTO BarCode.dbo.tblStaff SELECT * FROM BARCODE1.dbo.tblStaff;")
                addIn("INSERT INTO BarCode.dbo.tblResource SELECT * FROM BARCODE1.dbo.tblResource;")
                addIn("INSERT INTO BarCode.dbo.tblAssetDetail SELECT * FROM BARCODE1.dbo.tblAssetDetail;")
                addIn("INSERT INTO BarCode.dbo.BK_Loan SELECT * FROM BARCODE1.dbo.BK_Loan;")
                addIn("INSERT INTO BarCode.dbo.BK_Location SELECT * FROM BARCODE1.dbo.BK_Location;")
                addIn("INSERT INTO BarCode.dbo.BK_Customer SELECT * FROM BARCODE1.dbo.BK_Customer;")
                addIn("INSERT INTO BarCode.dbo.tblPosition SELECT * FROM BARCODE1.dbo.tblPosition;")
                addIn("INSERT INTO BarCode.dbo.tblOum SELECT * FROM BARCODE1.dbo.tblOum;")
                addIn("INSERT INTO BarCode.dbo.tblCU_Pro SELECT * FROM BARCODE1.dbo.tblCU_Pro;")
                addIn("INSERT INTO BarCode.dbo.tblTraceAssetDetail SELECT * FROM BARCODE1.dbo.tblTraceAssetDetail;")
                addIn("INSERT INTO BarCode.dbo.tblTraceResource SELECT * FROM BARCODE1.dbo.tblTraceResource;")
                addIn("INSERT INTO BarCode.dbo.tblTracesang SELECT * FROM BARCODE1.dbo.tblTracesang;")
                addIn("INSERT INTO BarCode.dbo.tblCollateral SELECT * FROM BARCODE1.dbo.tblCollateral;")


                'MessageBox.Show("Import data completed brand " & BrID.ToString)
                result = frmMessageError.ShowBoxError("ទិន្នន័យ" & name & " បានបញ្ចូលរួចរាល់។", "ការបញ្ចូលទិន្នន័យ")
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class