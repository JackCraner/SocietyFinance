﻿Public Class Manage_Transaction_Form
    Dim transaction As Transaction
    Dim list_expenses As List(Of Expense)

    Public transactionFinished As Boolean
    Private Sub Manage_Transaction_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Behaviour
        'If a outgoing amount equals a recoupment then ignore
        'If a outgoing matches a pending ask to 'close the pending' or 'move to recoup' 
        'Unknown outgoing, ask to name and/or create recoup
        'Unknown incoming, assign to expense or mark as donation
    End Sub

    Public Sub startForm(ByRef transaction As Transaction, ByRef list_expense As List(Of Expense))
        Me.transaction = transaction
        Me.list_expenses = list_expense

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
            For Each exp As Expense In list_expenses
                GroupBox2.Visible = True
                DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.projected_cost), exp.deadline, exp.IDCode})
            Next
        Else
            'try to match it to current recoupments (this should be done before launching this form)
            'try to close current pending (or swap to recoup)



            For Each exp As Expense In list_expenses
                If (exp.projected_cost = transaction.amount And Not exp.isPaid) Then
                    Dim answer As Integer = MsgBox("Is {" + transaction.name + ", " + transaction.reference + ", £" + CStr(transaction.amount) + "}" + " = " + exp.name + " ?", vbQuestion + vbYesNo + vbDefaultButton2, "Detected Expense Match")
                    If answer = vbYes Then
                        answer = MsgBox("Are you still waiting for recoupments?", vbQuestion + vbYesNo + vbDefaultButton2, "Defining Expense")
                        If answer = vbYes Then
                            exp.paidFlag = transaction
                        Else
                            Base_Form.End_Expendition(exp)
                        End If
                        Exit Sub
                    End If
                End If
            Next

            'assign as straight payment or recoup expense

            Label9.Text = transaction.amount
            TextBox2.Text = Base_Form.Create_UID()
            GroupBox1.Visible = True




        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If RadioButton2.Checked Then
            Dim newExpense As New Expense(TextBox2.Text, transaction.amount, transaction.dateMade)
            If (TextBox2.Text.Length = 3) Then
                newExpense.IDCode = TextBox2.Text
            Else
                MsgBox("Error in ID Code")
                Exit Sub
            End If
            newExpense.paidFlag = transaction
            Base_Form.Create_Expenditure(newExpense)
        Else
            Base_Form.handle_transaction(transaction)
        End If
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        list_expenses(DataGridView1.CurrentRow.Index).Add_Payment(transaction)
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Base_Form.handle_transaction(transaction)
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class