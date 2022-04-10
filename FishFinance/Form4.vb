Public Class Manage_Payment_Form
    Dim payment As Transaction
    Dim expense As RepaymentExpenditure
    Public Sub StartForm(ByRef payment As Transaction, ByRef expense As RepaymentExpenditure)
        Me.payment = payment
        Me.expense = expense
        TextBox1.Text = payment.amount
        TextBox2.Text = payment.name
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        payment.name = TextBox2.Text
        payment.amount = Double.Parse(TextBox1.Text)
        Manage_Pending_Form.updateForm()
        Me.Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        expense.list_of_payments.Remove(payment)
        Base_Form.Remove_Payment(payment)
        Manage_Pending_Form.updateForm()
        Me.Visible = False
    End Sub
End Class