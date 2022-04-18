Public Class Manage_Transaction_Form
    Dim transaction As Transaction
    Dim account_Pending As AccountPending
    Dim account_History As AccountHistory
    Public transactionFinished As Boolean
    Private Sub Manage_Transaction_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Behaviour
        'If a outgoing amount equals a recoupment then ignore
        'If a outgoing matches a pending ask to 'close the pending' or 'move to recoup' 
        'Unknown outgoing, ask to name and/or create recoup
        'Unknown incoming, assign to expense or mark as donation
    End Sub

    Public Sub startForm(ByRef transaction As Transaction, ByRef accountPending As AccountPending, ByRef accountHistory As AccountHistory)
        Me.transaction = transaction
        Me.account_Pending = accountPending
        Me.account_History = accountHistory
        transactionFinished = False
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        If transaction.getLabel = TransactionHandle.Income Then
            Label11.Text = "Income From:"
        Else
            Label11.Text = "Outgoing To:"
        End If
        Define_Issue()
        updateForm()
    End Sub
    Public Sub updateForm()

        Label1.Text = transaction.name
        Label2.Text = transaction.getABSAmount
        Label3.Text = transaction.dateMade
        Label4.Text = transaction.reference
        DataGridView2.Rows.Clear()
        RadioButton3.Select()
        Dim counter As Integer = 0
        For Each expense As Expense In account_Pending.Get_Expenses()
            If Not (expense.isPaid()) Then
                DataGridView2.Rows.Add(New String() {expense.name, FormatCurrency(expense.Get_Recoup), FormatCurrency(expense.projected_cost), expense.deadline, expense.IDCode})


                If (expense.projected_cost = transaction.getABSAmount) Then

                    DataGridView2.Rows(counter).DefaultCellStyle.BackColor = Color.Green
                End If
                counter += 1
            End If
        Next

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        transactionFinished = True
    End Sub

    Private Sub Define_Issue()
        If transaction.getLabel > 0 Then
            'assign to expense or mark as donation
            GroupBox2.Visible = True
            DataGridView1.Rows.Clear()
            For Each exp As Expense In account_Pending.Get_Expenses()
                DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.projected_cost), exp.deadline, exp.IDCode})
            Next
        Else
            'try to match it to current recoupments (this should be done before launching this form)
            'try to close current pending (or swap to recoup)





            'assign as straight payment or recoup expense
            TextBox3.Text = transaction.name
            Label9.Text = transaction.getABSAmount
            GroupBox1.Visible = True




        End If

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim newExpense As New Expense(TextBox3.Text, transaction.getABSAmount, transaction.dateMade)
        newExpense.Add_Paid(transaction)
        If RadioButton2.Checked Then


            account_Pending.Add_Expense(newExpense)
        Else
            account_History.Add_Expense(newExpense)
        End If

        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        account_Pending.Handle_Transaction(transaction)
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim answer = MsgBox("Are you sure", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
        If answer = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Define_Expenditure.reset(transaction)
        Define_Expenditure.Visible = True
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If RadioButton3.Checked Then
            transaction.updateTransactionLabel(TransactionHandle.Income)
        ElseIf RadioButton4.Checked Then
            transaction.updateTransactionLabel(TransactionHandle.Donation)
        ElseIf RadioButton5.Checked Then
            transaction.updateTransactionLabel(TransactionHandle.Loan)
        ElseIf RadioButton6.Created Then
            transaction.updateTransactionLabel(TransactionHandle.Membership)
        End If
        If (DataGridView1.SelectedRows.Count > 0) Then
            account_Pending.Get_Expense(DataGridView1.SelectedCells(4).Value.ToString()).Add_Income(transaction)
        Else
            account_Pending.Handle_Transaction(transaction)
            account_History.Add_Transaction(transaction)
        End If
        Me.Close()
    End Sub


    Private Sub DataGridView1_MouseUp(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseUp
        If e.Button = MouseButtons.Left Then
            ''# Check the HitTest information for this click location
            If Equals(DataGridView1.HitTest(e.X, e.Y), DataGridView.HitTestInfo.Nowhere) Then
                DataGridView1.ClearSelection()
                DataGridView1.CurrentCell = Nothing
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentDoubleClick
        Dim highlightedExpenseID As Expense = account_Pending.Get_Expenses.Find(Function(exp) exp.IDCode = DataGridView2.Rows(e.RowIndex).Cells(4).Value.ToString())
        Dim answer = MsgBox("Is this Expense Completely Finised?", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
        highlightedExpenseID.Add_Paid(transaction)
        If answer = MsgBoxResult.Yes Then
            account_Pending.Remove_Expense(highlightedExpenseID)
            account_History.Add_Expense(highlightedExpenseID)
        End If
        Me.Close()
    End Sub
End Class