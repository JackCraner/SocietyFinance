Public Class Manage_Pending_Form
    Dim exp As RepaymentExpenditure
    Public Sub startForm(ByRef exp As RepaymentExpenditure)
        Me.exp = exp
        TextBox3.Text = exp.IDCode
        updateForm()
    End Sub
    Public Sub updateForm()
        Label1.Text = exp.name
        Label4.Text = exp.amount
        Label7.Text = exp.Get_Recoup()
        TextBox1.Text = ""
        TextBox2.Text = ""
        DataGridView1.Rows.Clear()
        For Each payment As Transaction In exp.list_of_payments
            DataGridView1.Rows.Add(New String() {payment.name, payment.amount, payment.dateMade})
        Next
        Base_Form.updateALL()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Base_Form.Add_Payment(exp, TextBox1.Text, TextBox2.Text)
        Base_Form.updateALL()
        updateForm()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If (exp.expenditureType = ExpenditureTypes.Pending) And exp.amount = 0 Then
            Dim new_amount As Double = 0
            Try
                new_amount = InputBox("Enter Final Amount to Pay", "Set Amount")
                exp.amount = new_amount
            Catch ex As Exception
                Exit Sub
            End Try

        End If
        Base_Form.End_Expendition(exp)
        Me.Visible = False
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Manage_Payment_Form.Visible = True
        Manage_Payment_Form.StartForm(exp.list_of_payments(DataGridView1.CurrentRow.Index), exp)
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter)) Then
            If (TextBox3.Text.Length = 3) Then
                exp.IDCode = TextBox3.Text
                Base_Form.updateALL()
                updateForm()
            Else
                MsgBox("Code Wrong Format")
            End If

        End If


    End Sub
End Class