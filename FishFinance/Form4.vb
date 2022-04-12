Public Class Manage_Payment_Form
    Dim payment As Transaction
    Dim expense As Expense
    Public Sub StartForm(ByRef payment As Transaction, ByRef expense As Expense)
        Me.payment = payment
        Me.expense = expense
        UpdateForm()
    End Sub
    Public Sub UpdateForm()
        TextBox1.Text = payment.getABSAmount
        TextBox2.Text = payment.name
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        payment.name = TextBox2.Text
        payment.setAmount(Double.Parse(TextBox1.Text))
        Manage_Pending_Form.updateForm()
        Me.Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        expense.Remove_Payment(payment)
        Me.Visible = False
    End Sub

    Private Sub Manage_Payment_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class