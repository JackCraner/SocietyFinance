Public Class Manage_Transaction_Form
    Dim transaction As Transaction
    Dim list_pending As List(Of RepaymentExpenditure)
    Dim list_recoup As List(Of RepaymentExpenditure)
    Public transactionFinished As Boolean
    Private Sub Manage_Transaction_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Behaviour
        'If a outgoing amount equals a recoupment then ignore
        'If a outgoing matches a pending ask to 'close the pending' or 'move to recoup' 
        'Unknown outgoing, ask to name and/or create recoup
        'Unknown incoming, assign to expense or mark as donation
    End Sub

    Public Sub startForm(ByRef transaction As Transaction, ByRef list_pending As List(Of RepaymentExpenditure), ByRef list_recoup As List(Of RepaymentExpenditure))
        Me.transaction = transaction
        Me.list_pending = list_pending
        Me.list_recoup = list_recoup
        transactionFinished = False
        GroupBox1.Visible = False
        GroupBox2.Visible = False
        If transaction.amount > 0 Then
            Label11.Text = "Income From:"
        Else
            Label11.Text = "Outgoing To:"
        End If
        Define_Issue()
        updateForm()
    End Sub
    Public Sub updateForm()
        Label1.Text = transaction.name
        Label2.Text = transaction.amount
        Label3.Text = transaction.dateMade
        Label4.Text = transaction.reference
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        transactionFinished = True
    End Sub

    Private Sub Define_Issue()
        If transaction.amount > 0 Then
            'assign to expense or mark as donation
            DataGridView1.Rows.Clear()
            For Each exp As RepaymentExpenditure In list_pending.Concat(list_recoup)
                GroupBox2.Visible = True
                DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.amount), exp.eventDate, exp.IDCode})
            Next
        Else
            'try to match it to current recoupments (this should be done before launching this form)
            'try to close current pending (or swap to recoup)

            Dim temp_pending As New List(Of Double)
            For Each expense As RepaymentExpenditure In list_pending
                temp_pending.Add(expense.amount)
            Next
            Dim match = temp_pending.IndexOf(Math.Abs(transaction.amount))
            Dim defined_required As Boolean = True
            MsgBox(match)
            If (match >= 0) Then
                Dim expense As RepaymentExpenditure = list_pending(match)
                Dim answer As Integer = MsgBox("Is {" + transaction.name + ", " + transaction.reference + ", £" + CStr(transaction.amount) + "}" + " = " + expense.name + " ?", vbQuestion + vbYesNo + vbDefaultButton2, "Detected Expense Match")
                If answer = vbYes Then
                    defined_required = False
                    answer = MsgBox("Are you still waiting for recoupments?", vbQuestion + vbYesNo + vbDefaultButton2, "Defining Expense")
                    If answer = vbYes Then
                        Base_Form.Convert_Expense_Recoup(expense)

                    Else
                        Base_Form.End_Expendition(expense)
                    End If

                End If
            End If
            'assign as straight payment or recoup expense
            If defined_required = True Then
                Label9.Text = transaction.amount
                TextBox2.Text = Base_Form.Create_UID()
                GroupBox1.Visible = True
            Else
                Me.Close()
            End If


        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If RadioButton2.Checked Then
            Dim newExpense As New RepaymentExpenditure(TextBox2.Text, transaction.amount, transaction.dateMade, ExpenditureTypes.Recoup)
            If (TextBox2.Text.Length = 3) Then
                newExpense.IDCode = TextBox2.Text
            Else
                MsgBox("Error in ID Code")
                Exit Sub
            End If
            Base_Form.Create_Expenditure(newExpense)
        Else
            Base_Form.Create_Expenditure(New Expenditure(transaction.name, transaction.amount, transaction.dateMade))
        End If
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim combinedList = list_pending.Concat(list_recoup)
        combinedList(DataGridView1.CurrentRow.Index).Add_Payment(transaction)
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Base_Form.Add_Incoming(transaction)
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class