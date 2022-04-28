Public Class Manage_Pending_Form
    Dim exp As Expense
    Dim isActive As Boolean
    Public Sub startForm(ByRef exp As Expense, ByVal isActive As Boolean)
        Me.exp = exp
        Me.isActive = isActive
        TextBox3.Text = exp.IDCode
        updateForm()

    End Sub
    Public Sub updateForm()
        Label1.Text = exp.name
        Label4.Text = exp.projected_cost
        Label7.Text = exp.Get_Recoup()
        TextBox1.Text = exp.getProjectedPayback
        If (exp.hasTopic()) Then
            TextBox4.Text = exp.topic.name
        Else

            TextBox4.Text = "1"
        End If

        DataGridView1.Rows.Clear()
        For Each payment As Transaction In exp.get_transactions
            DataGridView1.Rows.Add(New String() {payment.name, payment.getABSAmount, payment.dateMade, payment.getLabel.ToString})
        Next

        If (exp.isPaid Or exp.list_of_payments.Count > 0) Then

            Button2.Text = "End Expense"
        Else
            Button2.Text = "Cancel Expense"
        End If
        If Not (isActive) Then

            Button2.Text = "Return Expense"
        End If

        If (exp.isPaid) Then
            Label3.Text = exp.getPaidFlag.name
            Label11.Text = exp.getPaidFlag.amount
            Label12.Text = exp.getPaidFlag.dateMade
        End If
        Base_Form.updateALL()
        ColourCode()
    End Sub
    Public Sub ColourCode()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells(3).Value = "Income" Then
                row.DefaultCellStyle.BackColor = Color.Green
            ElseIf (row.Cells(3).Value = "Donation") Then
                row.DefaultCellStyle.BackColor = Color.DarkGreen
            ElseIf (row.Cells(3).Value = "Loan") Then
                row.DefaultCellStyle.BackColor = Color.Yellow
            ElseIf (row.Cells(3).Value = "Membership") Then
                row.DefaultCellStyle.BackColor = Color.LightBlue
            ElseIf (row.Cells(3).Value = "Outgoing") Then
                row.DefaultCellStyle.BackColor = Color.Red
            ElseIf (row.Cells(3).Value = "Refund") Then
                row.DefaultCellStyle.BackColor = Color.Purple
            Else
                row.DefaultCellStyle.BackColor = Color.Red
            End If
        Next

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        exp.SetPayBack(Double.Parse(TextBox1.Text))

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (isActive) Then
            If (exp.isPaid) Then
                Base_Form.account_Pending.Remove_Expense(exp)
                Base_Form.account_History.Add_Expense(exp)
            Else
                If (exp.list_of_payments.Count > 0) Then
                    Dim answer = MsgBox("Are you sure? Not yet paid", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
                    If (answer = MsgBoxResult.Yes) Then
                        Base_Form.account_Pending.Remove_Expense(exp)
                        Base_Form.account_History.Add_Expense(exp)
                    End If
                Else
                    Base_Form.account_Pending.Remove_Expense(exp)
                End If

            End If
        Else
            Base_Form.account_Pending.Return_Expense(exp)
            Base_Form.account_History.Remove_Expense(exp)
            Form6.Update_Form()
        End If

        Me.Visible = False
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Manage_Payment_Form.Visible = True
        Manage_Payment_Form.StartForm(exp.get_transactions(DataGridView1.CurrentRow.Index))
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter)) Then
            If (TextBox3.Text.Length = 3) Then
                If (Base_Form.account_Pending.Check_UID(TextBox3.Text)) Then
                    exp.IDCode = TextBox3.Text
                    Base_Form.updateALL()
                    updateForm()
                Else
                    TextBox3.Text = exp.IDCode
                    MsgBox("Code already Exists")
                End If
            Else
                    MsgBox("Code Wrong Format")
            End If

        End If


    End Sub

    Private Sub Manage_Pending_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter)) Then
            Base_Form.account_Pending.Set_Topic(TextBox4.Text)
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class