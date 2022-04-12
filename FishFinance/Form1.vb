Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data
Public Class Base_Form

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        A_Balance_L.Text = "£0"
        Balance_L.Text = "£0"

    End Sub
    Dim list_of_expenditures As New List(Of Expense)
    Public current_Balance As Double = 0
    Dim membership_cost As Double = 0
    Dim last_time_updated As Date = Nothing

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
    Public Sub Handle_Transaction(ByRef transaction As Transaction)
        current_Balance += transaction.getAmount()

        updateALL()
        'add to account history
    End Sub
    Public Sub Add_History()

    End Sub
    Public Sub Create_Expenditure(ByRef expenditure As Expense)
        expenditure.IDCode = Create_UID()
        list_of_expenditures.Add(expenditure)

        updateALL()

    End Sub
    Public Sub End_Expendition(ByRef exp As Expense)
        If (list_of_expenditures.Contains(exp)) Then
            If (exp.isPaid) Then
                'current_Balance += exp.paidFlag.amount
                list_of_expenditures.Remove(exp)
            Else
                MsgBox("Expense Not Paid and thus cannot be closed")
            End If

        Else
            MsgBox("No Expense Found")
        End If
        'ADD expense to account history
        updateALL()
    End Sub
    Public Sub Cancel_Expedition(ByRef exp As Expense)
        If (list_of_expenditures.Contains(exp)) Then
            If exp.isPaid Then
            Else
                list_of_expenditures.Remove(exp)
            End If
        End If


    End Sub
    Public Sub updateALL()
        Dim balance_count As Double = 0

        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        For Each exp As Expense In list_of_expenditures
            If (exp.isPaid()) Then
                DataGridView2.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(Math.Abs(exp.projected_cost)), exp.deadline, exp.IDCode})
            Else

                DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(Math.Abs(exp.projected_cost)), exp.deadline, exp.IDCode})
            End If

        Next

        Label5.Text = last_time_updated
        Balance_L.Text = current_Balance
        calculateAvaliableBalance()
    End Sub
    Public Sub calculateAvaliableBalance()
        Dim temp_balance As Double = current_Balance
        For Each exp As Expense In list_of_expenditures
            If Not (exp.isPaid) Then
                If (exp.projected_cost = 0) Then
                    temp_balance -= exp.Get_Recoup()
                Else
                    temp_balance -= exp.projected_cost
                End If
            End If



        Next
        A_Balance_L.Text = temp_balance
    End Sub
    Public Function Get_Topics()
        Dim list_topics As New List(Of Topic)
        For Each exp As Expense In list_of_expenditures
            If exp.hasTopic() And Not list_topics.Contains(exp.expense_topic) Then
                list_topics.Add(exp.expense_topic)
            End If
        Next
        Return list_topics
    End Function

    Public Function Set_Topic(ByVal name As String)
        Dim test As List(Of Topic) = Get_Topics()
        Dim found_topic = test.Find(Function(t As Topic) t.topicID = Topic.convert_name_ID(name))
        If (IsNothing(found_topic)) Then
            Return New Topic(name)
        Else
            Return found_topic
        End If

    End Function

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
        'Manage_Pending_Form.Visible = True
        ' Manage_Pending_Form.startForm(list_of_pendingexpenditures(DataGridView1.CurrentRow.Index))
        'Dim highlightedID = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()
        'Dim highlightedExpenseID = Get_Expense_ID.FindIndex(highlightedID)
        Dim highlightedExpenseID = list_of_expenditures.FindIndex(Function(exp) exp.IDCode = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(list_of_expenditures(highlightedExpenseID))
    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick

        Dim highlightedExpenseID = list_of_expenditures.FindIndex(Function(exp) exp.IDCode = DataGridView2.Rows(e.RowIndex).Cells(4).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(list_of_expenditures(highlightedExpenseID))
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
            Dim newTransaction As New ExcelItem(xlWorkSheet.Cells(counter, 4).Value, xlWorkSheet.Cells(counter, 3).Value.Split(New Char() {","c})(0), transactionID, xlWorkSheet.Cells(counter, 1).Value)
            If newTransaction.amount >= 0 Then
                For Each expense As Expense In list_of_expenditures
                    If transactionID.Contains(expense.IDCode) Then  'THis is a worry, make more strict
                        expense.Add_Income(New Transaction(newTransaction.amount, TransactionHandle.Income, newTransaction.name, newTransaction.reference, newTransaction.dateMade))
                        rowAccounted = True
                    End If

                Next
            End If

            If rowAccounted = False Then
                If newTransaction.amount > 0 Then
                    list_unaccounted_in.Add((New Transaction(newTransaction.amount, TransactionHandle.Income, newTransaction.name, newTransaction.reference, newTransaction.dateMade)))
                Else
                    list_unaccounted_out.Add((New Transaction(Math.Abs(newTransaction.amount), TransactionHandle.Outgoing, newTransaction.name, newTransaction.reference, newTransaction.dateMade)))

                End If

            End If
            rowAccounted = False
            counter += 1
        End While
        For Each Transaction As Transaction In list_unaccounted_out.Concat(list_unaccounted_in)
            Manage_Transaction_Form.startForm(Transaction, list_of_expenditures)
            Manage_Transaction_Form.ShowDialog()
        Next
        last_time_updated = Date.Today
        updateALL()
    End Sub
End Class
