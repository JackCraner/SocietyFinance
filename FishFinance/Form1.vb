﻿Imports Excel = Microsoft.Office.Interop.Excel
Public Class Base_Form

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        A_Balance_L.Text = "£0"
        Balance_L.Text = "£0"

    End Sub
    Dim list_of_pendingexpenditures As New List(Of RepaymentExpenditure)
    Dim list_of_recoups As New List(Of RepaymentExpenditure)
    Dim list_of_history As New List(Of Expenditure)
    Dim current_Balance As Double
    Dim membership_cost As Double = 0
    Dim last_time_updated As Date = Nothing


    Public Sub Add_Payment(ByRef expenditure As RepaymentExpenditure, ByVal amount As String, ByVal name As String)
        current_Balance += Double.Parse(amount)
        expenditure.Add_Payment(New Transaction(Double.Parse(amount), name))
    End Sub
    Public Sub Remove_Payment(ByRef transaction As Transaction)
        current_Balance -= transaction.amount

    End Sub
    Public Sub Add_Incoming(ByRef transaction As Transaction)
        current_Balance += transaction.amount
        updateALL()
    End Sub
    Public Function Create_UID()
        Dim validchars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        Dim sb As New System.Text.StringBuilder()
        Dim rand As New Random()
        For i As Integer = 1 To 3
            Dim idx As Integer = rand.Next(0, validchars.Length)
            Dim randomChar As Char = validchars(idx)
            sb.Append(randomChar)
        Next i


        Return sb.ToString()
    End Function
    Public Sub Create_Expenditure(ByRef expenditure As Expenditure)
        If expenditure.GetType() Is GetType(Expenditure) Then
            'take money out of account and add to account history
            current_Balance -= expenditure.amount
            list_of_history.Add(expenditure)
        ElseIf expenditure.GetType() Is GetType(RepaymentExpenditure) Then
            If CType(expenditure, RepaymentExpenditure).expenditureType = ExpenditureTypes.Pending Then
                list_of_pendingexpenditures.Add(expenditure)
                CType(expenditure, RepaymentExpenditure).IDCode = Create_UID()
            ElseIf CType(expenditure, RepaymentExpenditure).expenditureType = ExpenditureTypes.Recoup Then
                current_Balance -= expenditure.amount
                list_of_recoups.Add(expenditure)
                CType(expenditure, RepaymentExpenditure).IDCode = Create_UID()
            End If
        End If

        'create the expense UID

        updateALL()

    End Sub
    Public Sub End_Expendition(ByRef exp As RepaymentExpenditure)

        If (exp.expenditureType = ExpenditureTypes.Recoup) Then
            list_of_recoups.Remove(exp)
        ElseIf (exp.expenditureType = ExpenditureTypes.Pending) Then
            list_of_pendingexpenditures.Remove(exp)
            current_Balance -= exp.amount
        End If


        list_of_history.Add(exp)
        updateALL()
    End Sub
    Public Sub Convert_Expense_Recoup(ByRef exp As RepaymentExpenditure)
        list_of_pendingexpenditures.Remove(exp)
        list_of_recoups.Add(exp)
        exp.expenditureType = ExpenditureTypes.Recoup
        current_Balance -= exp.amount
    End Sub
    Public Sub updateALL()
        Dim balance_count As Double = 0
        'PendingExpenditureLB.Items.Clear()
        DataGridView1.Rows.Clear()
        For Each exp As RepaymentExpenditure In list_of_pendingexpenditures
            'PendingExpenditureLB.Items.Add(exp.ToListBox)
            DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.amount), exp.eventDate, exp.IDCode})
        Next
        'RecoupableExpenditureLB.Items.Clear()
        DataGridView2.Rows.Clear()
        For Each exp As RepaymentExpenditure In list_of_recoups
            'RecoupableExpenditureLB.Items.Add(exp.ToListBox)
            DataGridView2.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.amount), exp.eventDate, exp.IDCode})
        Next
        AccountHistory_LB.Items.Clear()
        For Each exp As Expenditure In list_of_history
            AccountHistory_LB.Items.Add(exp.ToListBox)
        Next
        Label5.Text = last_time_updated
        Balance_L.Text = current_Balance
        calculateAvaliableBalance()
    End Sub
    Public Sub calculateAvaliableBalance()
        Dim temp_balance As Double = current_Balance
        For Each exp As RepaymentExpenditure In list_of_pendingexpenditures
            If (exp.amount = 0) Then
                temp_balance -= exp.Get_Recoup()
            Else
                temp_balance -= exp.amount
            End If

        Next
        A_Balance_L.Text = temp_balance
    End Sub



    Private Sub InitalBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InitalBalanceToolStripMenuItem.Click
        Try
            current_Balance = InputBox("Enter Current Balance", "Set Balance")
        Catch ex As Exception

        End Try

        Balance_L.Text = current_Balance
    End Sub



    Private Sub MembershipFeeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MembershipFeeToolStripMenuItem.Click
        Try
            membership_cost = InputBox("Enter Membership Price", "Enter Price")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripDropDownButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton4.Click
        Define_Expenditure.reset()
        Define_Expenditure.Visible = True
    End Sub

    Private Sub ToolStripDropDownButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton3.Click

    End Sub

    Private Sub ToolStripDropDownButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton2.Click
        Try
            Dim numberOfMembers As Integer = InputBox("How Many Members to Add", "Adding Membership Numbers")
            current_Balance += numberOfMembers * membership_cost
            updateALL()
        Catch ex As Exception
            MsgBox("Error with Input")
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(list_of_pendingexpenditures(DataGridView1.CurrentRow.Index))
    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(list_of_recoups(DataGridView2.CurrentRow.Index))
    End Sub

    Private Sub TextBox1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)
        'TextBox1.Text = e.Data.GetData(DataFormats.FileDrop)
    End Sub

    Private Sub ToolStripDropDownButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton5.Click
        'Fix Drag and Drop https://stackoverflow.com/questions/11686631/drag-drop-and-get-file-path-in-vb-net


        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet

        xlWorkBook = xlApp.Workbooks.Open("C:\Personal Files\Frisbee\test.xlsx")
        xlWorkSheet = xlWorkBook.Worksheets("sheet1")
        Dim list_unaccounted_in As New List(Of Transaction)
        Dim list_unaccounted_out As New List(Of Transaction)
        Dim counter As Integer = 4
        Dim rowAccounted As Boolean = False
        'filters out dates which have already been updated
        While (Not (xlWorkSheet.Cells(counter, 1).Value = Nothing)) And (DateTime.Compare(xlWorkSheet.Cells(counter, 1).Value, last_time_updated) >= 0)
            Dim transactionID As String = xlWorkSheet.Cells(counter, 3).Value.Split(New Char() {","c})(1)
            Dim newTransaction As New Transaction(xlWorkSheet.Cells(counter, 4).Value, xlWorkSheet.Cells(counter, 3).Value.Split(New Char() {","c})(0), transactionID, xlWorkSheet.Cells(counter, 1).Value)
            If newTransaction.amount <= 0 Then
                For Each expense As RepaymentExpenditure In list_of_pendingexpenditures.Concat(list_of_recoups)
                    If transactionID.Contains(expense.IDCode) Then  'THis is a worry, make more strict
                        expense.Add_Payment(newTransaction)
                        rowAccounted = True
                    End If

                Next
            End If

            If rowAccounted = False Then
                If newTransaction.amount > 0 Then
                    list_unaccounted_in.Add(newTransaction)
                Else
                    list_unaccounted_out.Add(newTransaction)

                End If

            End If
            rowAccounted = False
            counter += 1
        End While
        For Each Transaction As Transaction In list_unaccounted_out.Concat(list_unaccounted_in)
            Manage_Transaction_Form.startForm(Transaction, list_of_pendingexpenditures, list_of_recoups)
            Manage_Transaction_Form.ShowDialog()
        Next
        last_time_updated = Date.Today
        updateALL()
    End Sub
End Class
