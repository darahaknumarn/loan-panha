Public Class FrmCustomer
    Sub showcustom()
        AddToGrid(DataGridView1, 11, "select a.CM_ID,a.CM_Name ,b.VL_ID,c.FamilyBook,c.plong_reng,c.BongKanDai,c.LiveBook,c.Plong_tun,c.Files,c.CarCard,c.MotoCard from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID left join tblCU_Pro c on a.CM_BrId=c.BrID and a.CM_ID=c.CU_ID where a.CM_BrId='" & frmMain.lblCode.Text & "' order by CM_Date_Create desc")
    End Sub
    Sub AddCustom()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = Me.txtCustomerID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtCustomerName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtCustomerAddress.Text
            End With
            sql = "insert tblcustom(customid,customname,customadd) values (@d0,@d1,@d2)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub UpdateCustom()
        Dim sql, co1, co2 As String
        Dim a As String = getData("Select CU_ID from tblCU_Pro where CU_ID ='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
        If a = "" Then
            Try
                Dim com As New SqlClient.SqlCommand
                Dim con As New SqlClient.SqlConnection
                con.ConnectionString = connectionString1
                con.Open()
                com.Connection = con
                With com.Parameters
                    .Add("@d0", SqlDbType.NVarChar).Value = txtCustomerID.Text
                    .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text

                    '------------------------------------------------------- Family
                    If chFamily.Checked Then
                        If txtFamilyBook.Text = "" Or txtFamilyBook.Text = 0 Then
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅគ្រួសារមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "សៀវភៅគ្រួសារគ្មានចំនួន")
                            Return
                        Else
                            If txtFamilyBook.Text > 2 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅគ្រួសារមិនអាចលើសពី 2 បានទេ។", "សៀវភៅគ្រួសារលើសចំនួន")
                                Return
                            Else
                                .Add("@d2", SqlDbType.Int).Value = Me.txtFamilyBook.Text
                            End If

                        End If
                    Else
                        .Add("@d2", SqlDbType.Int).Value = 0
                    End If

                    '--------------------------------------------------------- Live
                    If chLive.Checked Then
                        If txtLiveBook.Text = "" Or txtLiveBook.Text = 0 Then
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅស្នាក់នៅមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "សៀវភៅស្នាក់នៅគ្មានចំនួន")
                            Return
                        Else
                            If Val(txtLiveBook.Text) > 2 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅស្នាក់នៅមិនអាចលើសពី 2 បានទេ។", "សៀវភៅស្នាក់នៅលើសចំនួន")
                                Return
                            Else
                                .Add("@d3", SqlDbType.Int).Value = Me.txtLiveBook.Text
                            End If
                        End If
                    Else
                        .Add("@d3", SqlDbType.Int).Value = 0
                    End If
                    '--------------------------------------------------------- Moto
                    If chMoto.Checked Then
                        .Add("@d4", SqlDbType.Int).Value = 1
                    Else
                        .Add("@d4", SqlDbType.Int).Value = 0
                    End If
                    '-------------------------------------------------------- Car
                    If chCar.Checked Then
                        .Add("@d5", SqlDbType.Int).Value = 1
                    Else
                        .Add("@d5", SqlDbType.Int).Value = 0
                    End If
                    '-------------------------------------------------------- Pun
                    If chPun.Checked Then
                        .Add("@d6", SqlDbType.Int).Value = 1
                    Else
                        .Add("@d6", SqlDbType.Int).Value = 0
                    End If
                    '-------------------------------------------------------Plong Reng
                    If chReng.Checked Then
                        If txtReng.Text = "" Or txtReng.Text = 0 Then
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់រឹងមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "ប្លង់រឹងខុសចំនួន")
                            Return
                        Else
                            If Val(txtReng.Text) > 3 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់រឹងមិនអាចលើសពី 3 បានទេ", "ប្លង់រឹងលើសចំនួន")
                                Return
                            Else
                                .Add("@d7", SqlDbType.Int).Value = Me.txtReng.Text
                            End If

                        End If
                    Else
                        .Add("@d7", SqlDbType.Int).Value = 0
                    End If
                    '--------------------------------------------------------Plong Tun
                    If chTun.Checked Then
                        If txtTun.Text = "" Or txtTun.Text = 0 Then
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់ទន់មិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "ប្លង់ទន់ខុសចំនួន")
                            Return
                        Else
                            If Val(txtTun.Text) > 3 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់ទន់មិនអាចលើសពី 3 បានទេ", "ប្លង់ទន់លើសចំនួន")
                                Return
                            Else
                                .Add("@d8", SqlDbType.Int).Value = Me.txtTun.Text
                            End If

                        End If
                    Else
                        .Add("@d8", SqlDbType.Int).Value = 0
                    End If

                    '--------------------------------------------------------- File
                    If chFile.Checked Then
                        .Add("@d9", SqlDbType.Int).Value = 1
                    Else
                        .Add("@d9", SqlDbType.Int).Value = 0
                    End If
                    ''--------------------------------------------------------- Description
                    If chReng.Checked Or chTun.Checked Or chCar.Checked Or chMoto.Checked Or chPun.Checked Then
                        .Add("@d10", SqlDbType.NVarChar).Value = Me.txtDes.Text
                    Else
                        .Add("@d10", SqlDbType.NVarChar).Value = 0
                    End If
                End With
                sql = "insert into tblCU_Pro(CU_ID,BrID,FamilyBook,LiveBook,MotoCard,CarCard,BongKanDai,plong_reng,Plong_tun,Files,Des) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)"
                com.CommandText = sql
                com.ExecuteNonQuery()
                com.Parameters.Clear()
                com.Dispose()
                con.Close()
                con.Dispose()
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានធ្វើការកែប្រែរួចរាល់។", "កែប្រែទិន្នន័យ")
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            End Try
        Else
            Try
                Dim com As New SqlClient.SqlCommand
                Dim con As New SqlClient.SqlConnection
                con.ConnectionString = connectionString1
                con.Open()
                com.Connection = con
                With com.Parameters
                    .Add("@d0", SqlDbType.NVarChar).Value = txtCustomerID.Text
                    .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                    '------------------ Family
                    co2 = getData(" select case when collateralid=8 then COUNT(collateralid) else 0 end FamilyBook from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=8 group by collateralid")
                    '----------------------------
                    If co2 = "" Or co2 = "0" Then
                        If chFamily.Checked Then
                            If txtFamilyBook.Text > 2 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅស្នាក់នៅមិនអាចលើសពី 2 បានទេ។", "សៀវភៅស្នាក់នៅលើសចំនួន")
                                Return
                            ElseIf txtFamilyBook.Text = "" Or txtFamilyBook.Text = 0 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅគ្រួសារមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "សៀវភៅគ្រួសារគ្មានចំនួន")
                                Return
                            Else
                                .Add("@d2", SqlDbType.Int).Value = txtFamilyBook.Text
                            End If
                        Else
                            .Add("@d2", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Or co2 = "2" Then
                        If chFamily.Checked Then
                            If co2 < txtFamilyBook.Text Or co2 = txtFamilyBook.Text Then
                                .Add("@d2", SqlDbType.Int).Value = txtFamilyBook.Text
                            Else
                                resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'សៀវភៅគ្រួសារ' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                                chFamily.Checked = True
                                Return
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'សៀវភៅគ្រួសារ' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chFamily.Checked = True
                            Return
                        End If
                    End If
                    '--------------- Live
                    co2 = getData(" select case when collateralid=7 then COUNT(collateralid) else 0 end LiveBook from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=7 group by collateralid")
                    If co2 = "" Or co2 = "0" Then
                        If chLive.Checked Then
                            If txtLiveBook.Text > 2 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅស្នាក់នៅមិនអាចលើសពី 2 បានទេ។", "សៀវភៅស្នាក់នៅលើសចំនួន")
                                Return
                            ElseIf txtLiveBook.Text = "" Or txtLiveBook.Text = 0 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់សៀវភៅស្នាក់នៅមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "សៀវភៅគ្រួសារគ្មានចំនួន")
                                Return
                            Else
                                .Add("@d3", SqlDbType.Int).Value = txtLiveBook.Text
                            End If
                        Else
                            .Add("@d3", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Or co2 = "2" Then
                        If chLive.Checked Then
                            If co2 < txtLiveBook.Text Or co2 = txtLiveBook.Text Then
                                .Add("@d3", SqlDbType.Int).Value = txtLiveBook.Text
                            Else
                                resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'សៀវភៅស្នាក់នៅ' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                                chLive.Checked = True
                                Return
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'សៀវភៅស្នាក់នៅ' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chLive.Checked = True
                            Return
                        End If
                    End If
                    '---------------------------------------- Moto
                    co2 = getData(" select case when collateralid=5 then 1 else 0 end MotoCard from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=5 ")
                    If co2 = "" Or co2 = "0" Then
                        If chMoto.Checked Then
                            .Add("@d4", SqlDbType.Int).Value = 1
                        Else
                            .Add("@d4", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Then
                        Me.txtDes.ReadOnly = False
                        If chMoto.Checked Then
                            co1 = 1
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យធានា 'ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chMoto.Checked = True
                            Return
                        End If
                    End If
                    '---------------------------------------- Car
                    co2 = getData(" select case when collateralid=6 then 1 else 0 end CarCard from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=6 ")
                    If co2 = "" Or co2 = "0" Then
                        If chCar.Checked Then
                            .Add("@d5", SqlDbType.Int).Value = 1
                        Else
                            .Add("@d5", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Then
                        Me.txtDes.ReadOnly = False
                        If chCar.Checked Then
                            co1 = 1
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យធានា 'ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chCar.Checked = True
                            Return
                        End If
                    End If
                    '---------------------------------------- Pun
                    co2 = getData(" select case when collateralid=11 then 1 else 0 end BongKanDai from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=11 ")
                    If co2 = "" Or co2 = "0" Then
                        If chPun.Checked Then
                            .Add("@d6", SqlDbType.Int).Value = 1
                        Else
                            .Add("@d6", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Then
                        Me.txtDes.ReadOnly = False
                        If chPun.Checked Then
                            co1 = 1
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យធានា 'បង្កាន់ដៃពន្ធនាំចូល' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chPun.Checked = True
                            Return
                        End If
                    End If
                    '----------------------------------------Plong_Reng
                    co2 = getData(" select case when collateralid=9 then COUNT(collateralid) else 0 end Plong_Reng from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=9 group by collateralid")
                    If co2 = "" Or co2 = "0" Then
                        If chReng.Checked Then
                            If txtReng.Text > 3 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់រឹងមិនអាចលើសពី 3 បានទេ។", "ប្លង់រឹងលើសចំនួន")
                                Return
                            ElseIf txtReng.Text = "" Or txtReng.Text = 0 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់រឹងមិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "ប្លង់រឹងមានគ្មានចំនួន")
                                Return
                            Else
                                Me.txtDes.ReadOnly = False
                                .Add("@d7", SqlDbType.Int).Value = txtReng.Text
                            End If
                        Else
                            .Add("@d7", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Or co2 = "2" Or co2 = "3" Then
                        If chReng.Checked Then
                            If co2 < txtReng.Text Then
                                Me.txtDes.ReadOnly = False
                                .Add("@d7", SqlDbType.Int).Value = txtReng.Text
                            Else
                                resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'ប្លង់រឹង' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                                chReng.Checked = True
                                Return
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'ប្លង់រឹង' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chReng.Checked = True
                            Return
                        End If
                    End If
                    '----------------------------------------Plong_Tun
                    co2 = getData(" select case when collateralid=10 then COUNT(collateralid) else 0 end Plong_Tun from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0 and collateralid=10 group by collateralid")
                    If co2 = "" Or co2 = "0" Then
                        If chTun.Checked Then
                            If txtTun.Text > 3 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់ទន់មិនអាចលើសពី 3 បានទេ។", "ប្លង់ទន់លើសចំនួន")
                                Return
                            ElseIf txtTun.Text = "" Or txtTun.Text = 0 Then
                                resultError = frmMessageError.ShowBoxError("ទ្រព្យតម្តល់ប្លង់ទន់មិនអាចគ្មានចំនួន រឺ ស្មើនឹង 0​ បាន​ទេ។", "ប្លង់ទន់មានគ្មានចំនួន")
                                Return
                            Else
                                .Add("@d8", SqlDbType.Int).Value = txtTun.Text
                            End If
                        Else
                            .Add("@d8", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Or co2 = "2" Or co2 = "3" Then
                        If chTun.Checked Then
                            If co2 < txtTun.Text Then
                                Me.txtDes.ReadOnly = False
                                .Add("@d8", SqlDbType.Int).Value = txtTun.Text
                            Else
                                resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'ប្លង់ទន់' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                                chTun.Checked = True
                                Return
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យតម្តល់ 'ប្លង់ទន់' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chTun.Checked = True
                            Return
                        End If
                    End If
                    '---------------------------------------- File
                    co2 = getData("select case when collateralid=12 then 1 else 0 end Files from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")
                    If co2 = "" Or co2 = "0" Then
                        If chFile.Checked Then
                            .Add("@d9", SqlDbType.Int).Value = 1
                        Else
                            .Add("@d9", SqlDbType.Int).Value = 0
                        End If
                    ElseIf co2 = "1" Then
                        If chFile.Checked Then
                            co1 = 1
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យធានា 'Files' នៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            chFile.Checked = True
                        End If
                    End If
                    ''---------------------------------------------Description
                    co2 = getData(" select Des from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")
                    If co2 = "" Or co2 = "0" Then
                        If chReng.Checked Or chTun.Checked Or chCar.Checked Or chMoto.Checked Or chPun.Checked Then
                            .Add("@d10", SqlDbType.NVarChar).Value = Me.txtDes.Text
                        Else
                            .Add("@d10", SqlDbType.NVarChar).Value = 0
                        End If
                    ElseIf co2 = "1" Then
                        If chReng.Checked Or chTun.Checked Or chCar.Checked Or chMoto.Checked Or chPun.Checked Then
                            co1 = 1
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែបានទេ ព្រោះទ្រព្យធានានៅមិនទាន់សងនៅឡើយ", "មិនអាចកែប្រែ")
                            'chFile.Checked = True
                        End If
                    End If
                End With
                sql = "update tblCU_Pro set BrID=@d1, FamilyBook=@d2, LiveBook=@d3, MotoCard=@d4, CarCard=@d5, BongKanDai=@d6, plong_reng=@d7, Plong_tun=@d8, Files=@d9, Des=@d10 where CU_ID=@d0 and BrID=@d1"
                com.CommandText = sql
                com.ExecuteNonQuery()
                com.Parameters.Clear()
                com.Dispose()
                con.Close()
                con.Dispose()
                resultError = frmMessageError.ShowBoxError("បានរក្សាទុក", "IT Solution")
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            End Try
        End If
    End Sub
    Private Sub BtnNew_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            BtnNew.Text = "រក្សាទុក"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            txtCustomerID.ReadOnly = False
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            UpdateCustom()
            BtnNew.Text = "រក្សាទុក"
            BtnNew.Enabled = False
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            showcustom()
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "រក្សាទុក"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If txtCustomerID.Text = "" Or txtCustomerName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកទិន្នន័យបានទេព្រោះពត៌មានមិនគ្រប់គ្រាន់", "ទិន្នន័យមិនគ្រប់គ្រាន់")
                Return
            Else
                AddCustom()
                showcustom()
            End If
        Else
            BtnNew.Text = "រក្សាទុក"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "រក្សាទុក"
            BtnNew.Enabled = False
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        txtFamilyBook.ReadOnly = False
        txtLiveBook.ReadOnly = False
        txtDes.ReadOnly = False
        If Me.txtCustomerID.Text = "" Then
            resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើសអតិថិជនជាមុនសិនមុននឹងធ្វើការកែប្រែ", "មិនអាចកែប្រែបានទេ")
            Return
        End If
        If BtnEdit.Text = "កែប្រែ" Then
            BtnEdit.Enabled = False
            BtnNew.Text = "រក្សាទុក"
            BtnNew.Enabled = True
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            txtCustomerID.ReadOnly = True
            'txtDes.ReadOnly = True
        End If
    End Sub
    Sub showall()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 11
        DataGridView1.Columns(0).Name = "កូដអតិថិជន"
        DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
        DataGridView1.Columns(3).Name = "សៀវភៅគ្រួសារ Copy"
        DataGridView1.Columns(4).Name = "ប្លង់រឹង"
        DataGridView1.Columns(5).Name = "បង្កាន់ដៃពន្ធនាំចូល"
        DataGridView1.Columns(6).Name = "សៀវភៅស្នាក់នៅ Copy"
        DataGridView1.Columns(7).Name = "ប្លង់ទន់"
        DataGridView1.Columns(8).Name = "File"
        DataGridView1.Columns(9).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)"
        DataGridView1.Columns(10).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)"
        showcustom()
        txtCustomerID.ReadOnly = True
        txtCustomerName.ReadOnly = True
        txtDes.ReadOnly = True
        chFamily.Checked = False
        chLive.Checked = False
        chMoto.Checked = False
        chCar.Checked = False
        chPun.Checked = False
        chFile.Checked = False
        chTun.Checked = False
        chReng.Checked = False
    End Sub
    Private Sub FrmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            BtnDelete.Enabled = False
            BtnEdit.Enabled = False
            Button3.Enabled = False
            BtnNew.Enabled = False
            txtCustomerID.ReadOnly = True
            Me.txtDes.ReadOnly = True
        End If
        showall()
        ' ----------------------------------- Update tblCu_Pro
        BtnNew.Enabled = False
        txtFamilyBook.ReadOnly = True
        txtLiveBook.ReadOnly = True
        BtnNew.Text = "រក្សាទុក"
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.DataGridView1.SelectedRows.Count > 0 Then
            Me.txtCustomerID.Text = Me.DataGridView1.SelectedRows(0).Cells(0).Value
            Me.txtCustomerName.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
            Me.txtCustomerAddress.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
        End If
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទុកទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
        If result = "1" Then
            If txtCustomerID.Text = "" Or txtCustomerName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("តើអ្នកចង់លុនទិន្នន័យនេះមែនទេ?", "លុបទិន្នន័យ")
            Else
                Dim a As String = getData("Select customid from tblResource where customid='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")
                If a = "" Then
                    addIn("delete from BK_Customer where CM_ID='" & txtCustomerID.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                    resultError = frmMessageError.ShowBoxError("លុបបានសំរេច", "លុបទិន្នន័យ")
                Else
                    resultError = frmMessageError.ShowBoxError("មិនអាចលុបអតិថិជននេះបានទេព្រោះមិនទាន់សងទ្រព្យធានា", "មិនអាចលុបបាន")
                End If
                showall()
            End If
        End If
    End Sub
    Private Sub BtnImportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImportExcel.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmCustomerReport.MdiParent = frmMain
        frmCustomerReport.WindowState = FormWindowState.Maximized
        frmCustomerReport.Show()
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        AddToGrid(DataGridView1, 11, "select Convert(int,a.CM_ID) CM_ID,a.CM_Name ,b.VL_ID,FamilyBook,c.plong_reng,c.BongKanDai,c.LiveBook,c.Plong_tun,c.Files,c.CarCard,c.MotoCard from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID left join tblCU_Pro c on a.CM_BrId=c.BrID and a.CM_ID=c.CU_ID where a.CM_ID='" & TextBox1.Text & "' and a.CM_BrId='" & frmMain.lblCode.Text & "' order by 1")
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ToExcel(DataGridView1)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        showall()
    End Sub
    Private Sub DataGridView1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If Me.DataGridView1.SelectedRows.Count < 1 Then
            Exit Sub
        End If

        Me.txtCustomerID.Text = Me.DataGridView1.SelectedRows(0).Cells(0).Value
        Me.txtCustomerName.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
        Me.txtCustomerAddress.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
        '--------------------------3 Family
        If Me.DataGridView1.SelectedRows(0).Cells(3).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(3).Value = "0" Then
            Me.chFamily.Checked = False
            txtFamilyBook.Text = ""
            txtFamilyBook.ReadOnly = True
        Else
            Me.chFamily.Checked = True
            txtFamilyBook.Text = DataGridView1.SelectedRows(0).Cells(3).Value

        End If
        '--------------------------4 Plong Reng
        If Me.DataGridView1.SelectedRows(0).Cells(4).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(4).Value = "0" Then
            Me.chReng.Checked = False
            txtReng.Text = ""
            txtReng.ReadOnly = True
        Else
            Me.chReng.Checked = True
            txtReng.Text = DataGridView1.SelectedRows(0).Cells(4).Value
        End If
        '---------------------------5 Pun Nom Jol
        If Me.DataGridView1.SelectedRows(0).Cells(5).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(5).Value = "0" Then
            Me.chPun.Checked = False
        Else
            Me.chPun.Checked = True
        End If
        '---------------------------6 LiveBook
        If Me.DataGridView1.SelectedRows(0).Cells(6).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(6).Value = "0" Then
            Me.chLive.Checked = False
            txtLiveBook.Text = ""
            txtLiveBook.ReadOnly = True
        Else
            Me.chLive.Checked = True
            txtLiveBook.Text = DataGridView1.SelectedRows(0).Cells(6).Value
        End If
        '---------------------------7 Plong Tun
        If Me.DataGridView1.SelectedRows(0).Cells(7).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(7).Value = "0" Then
            Me.chTun.Checked = False
            txtTun.Text = ""
            txtTun.ReadOnly = True
        Else
            Me.chTun.Checked = True
            txtTun.Text = DataGridView1.SelectedRows(0).Cells(7).Value
        End If
        '---------------------------8 File
        If Me.DataGridView1.SelectedRows(0).Cells(8).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(8).Value = "0" Then
            Me.chFile.Checked = False
        Else
            Me.chFile.Checked = True
        End If
        '---------------------------9 Car
        If Me.DataGridView1.SelectedRows(0).Cells(9).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(9).Value = "0" Then
            Me.chCar.Checked = False
        Else
            Me.chCar.Checked = True
        End If
        '---------------------------10 Moto
        If Me.DataGridView1.SelectedRows(0).Cells(10).Value = "" Or Me.DataGridView1.SelectedRows(0).Cells(10).Value = "0" Then
            Me.chMoto.Checked = False
        Else
            Me.chMoto.Checked = True
        End If
        '----------------------------11 Description
        Me.txtDes.Text = frmMain.lblCode.Text
        Me.txtDes.Text = getData("select top(1) Des from tblCU_Pro where CU_ID='" & txtCustomerID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
    End Sub
    Sub chFalse()
        chFamily.Checked = False
        chLive.Checked = False
        chFile.Checked = False
        chReng.Checked = False
        chTun.Checked = False
        chPun.Checked = False
        chMoto.Checked = False
        chCar.Checked = False
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim result As String = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្ន័យមែនទេ?", "រក្សាទុកទិន្ន័យ")
        If result = "1" Then
            resultError = frmMessageError.ShowBoxError("ត្រូវបានជ្រើសរើស", "ជ្រើសរើស")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmUPdateCustomer.MdiParent = frmMain
        frmUPdateCustomer.WindowState = FormWindowState.Maximized
        frmUPdateCustomer.Show()
    End Sub

    Private Sub chFamily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chFamily.CheckedChanged
        If chFamily.Checked = True Then
            txtFamilyBook.ReadOnly = False
            txtDes.ReadOnly = False
            txtFamilyBook.Focus()
        Else
            txtFamilyBook.ReadOnly = True
            txtDes.ReadOnly = True
            txtFamilyBook.Text = ""
        End If
    End Sub

    Private Sub chLive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chLive.CheckedChanged
        If chLive.Checked = True Then
            txtLiveBook.ReadOnly = False
            txtDes.ReadOnly = False
            txtLiveBook.Focus()
        Else
            txtLiveBook.ReadOnly = True
            txtDes.ReadOnly = True
            txtLiveBook.Text = ""
        End If

    End Sub
    Private Sub chReng_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chReng.CheckedChanged
        If chReng.Checked = True Then
            txtReng.ReadOnly = False
            txtDes.ReadOnly = False
            txtReng.Focus()
        Else
            txtReng.ReadOnly = True
            txtDes.ReadOnly = True
            txtReng.Text = ""
        End If
    End Sub

    Private Sub chTun_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chTun.CheckedChanged
        If chTun.Checked = True Then
            txtTun.ReadOnly = False
            txtDes.ReadOnly = False
            txtTun.Focus()
        Else
            txtTun.ReadOnly = True
            txtDes.ReadOnly = True
            txtTun.Text = ""
        End If
    End Sub
    Private Sub txtFamilyBook_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFamilyBook.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtLiveBook_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLiveBook.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtReng_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReng.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtTun_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTun.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class