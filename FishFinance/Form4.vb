Public Class Manage_Payment_Form
    Dim payment As Transaction
    Public Sub StartForm(ByRef payment As Transaction)
        Me.payment = payment
        UpdateForm()
    End Sub
    Public Sub UpdateForm()
        TextBox1.Text = payment.getABSAmount
        TextBox2.Text = payment.name
        TextBox3.Text = payment.reference
        Label4.Text = payment.transID
        DataGridView1.Rows.Clear()
        ComboBox1.Items.Clear()

        For Each type As TransactionHandle In System.Enum.GetValues(GetType(TransactionHandle))
            If (payment.getLabel > 0) Then
                If type > 0 Then
                    ComboBox1.Items.Add(type.ToString)
                End If
            Else
                If type < 0 Then
                    ComboBox1.Items.Add(type.ToString)
                End If
            End If



        Next
        ComboBox1.Text = payment.getLabel.ToString
        For Each exp As Expense In Base_Form.account_Pending.Get_Expenses()
            If Not (exp.Equals(Base_Form.getExpenseGlobal(payment.expLink))) Then
                DataGridView1.Rows.Add((New String() {exp.name, exp.deadline, exp.IDCode}))
            End If



        Next
        If (Not IsNothing(payment.expLink)) Then
            Label3.Text = Base_Form.getExpenseGlobal(payment.expLink).name
            Button4.Visible = True
        Else
            Label3.Text = "No Expense Link"
            Button4.Visible = False
        End If
        DataGridView1.ClearSelection()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        payment.name = TextBox2.Text
        payment.setAmount(Double.Parse(TextBox1.Text))
        Manage_Pending_Form.updateForm()
        Me.Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Base_Form.getExpenseGlobal(payment.expLink).Remove_Payment(payment)
        Manage_Pending_Form.updateForm()
        Me.Visible = False
    End Sub

    Private Sub Manage_Payment_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim highlightedExp As Expense = Base_Form.account_Pending.Get_Expense(DataGridView1.SelectedCells(2).Value.ToString())
        Base_Form.getExpenseGlobal(payment.expLink).Move_Payment(payment, highlightedExp)
        Base_Form.updateALL()
        Manage_Pending_Form.updateForm()
        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Manage_Pending_Form.Visible = True
        If (IsNothing(Base_Form.account_Pending.Get_Expense(payment.expLink))) Then
            Manage_Pending_Form.startForm(Base_Form.getExpenseGlobal(payment.expLink), False)
        Else
            Manage_Pending_Form.startForm(Base_Form.getExpenseGlobal(payment.expLink), True)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not (ComboBox1.Text = payment.getLabel.ToString) Then
            Dim answer = MsgBox("Are you sure?", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
            If (answer = MsgBoxResult.Yes) Then
                For Each type As TransactionHandle In System.Enum.GetValues(GetType(TransactionHandle))
                    If ComboBox1.Text = type.ToString Then
                        payment.updateTransactionLabel(type)
                        UpdateForm()
                        Manage_Pending_Form.Update()
                        Base_Form.updateALL()
                    End If

                Next
            End If
        End If

    End Sub
End Class