Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data
Public Class Base_Form

    Public account_History As New AccountHistory
    Public account_Pending As New AccountPending
    Public account_Settings As New AccountSettings
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        A_Balance_L.Text = "£0"
        Balance_L.Text = "£0"
        account_History.Read_Data(account_Pending, account_Settings)
    End Sub
    Public Sub ClearALL()
        account_History = New AccountHistory
        account_Pending = New AccountPending
        account_Settings = New AccountSettings
        updateALL()
    End Sub
    Public Sub updateALL()


        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        For Each exp As Expense In account_Pending.Get_Expenses()
            If (exp.isPaid()) Then
                DataGridView2.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(exp.getPaidFlag.getABSAmount()), exp.deadline, exp.IDCode})

            Else

                DataGridView1.Rows.Add(New String() {exp.name, FormatCurrency(exp.Get_Recoup), FormatCurrency(Math.Abs(exp.projected_cost)), exp.getProjectedPayback, exp.deadline, exp.IDCode})
            End If

        Next

        Label5.Text = account_Settings.get_LUD
        Balance_L.Text = account_Pending.Get_Current_Balance
        A_Balance_L.Text = account_Pending.Get_Ava_Balance
        Label4.Text = account_Pending.Get_Predicted_Balance
    End Sub




    Private Sub MembershipFeeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MembershipFeeToolStripMenuItem.Click
        Try
            account_Settings.Set_MembershipCost(InputBox("Enter Membership Price", "Enter Price"))
        Catch ex As Exception
            MsgBox("Membership Cost Failed")
        End Try
    End Sub

    Private Sub ToolStripDropDownButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton4.Click
        Define_Expenditure.reset()
        Define_Expenditure.Visible = True
    End Sub

    Private Sub ToolStripDropDownButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton3.Click
        ClearALL()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        'Manage_Pending_Form.Visible = True
        ' Manage_Pending_Form.startForm(list_of_pendingexpenditures(DataGridView1.CurrentRow.Index))
        'Dim highlightedID = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString()
        'Dim highlightedExpenseID = Get_Expense_ID.FindIndex(highlightedID)
        Dim highlightedExpenseID = account_Pending.Get_Expenses.FindIndex(Function(exp) exp.IDCode = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(account_Pending.Get_Expenses(highlightedExpenseID), True)
    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick

        Dim highlightedExpenseID = account_Pending.Get_Expenses.FindIndex(Function(exp) exp.IDCode = DataGridView2.Rows(e.RowIndex).Cells(4).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(account_Pending.Get_Expenses(highlightedExpenseID), True)
    End Sub

    Private Sub TextBox1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)
        'TextBox1.Text = e.Data.GetData(DataFormats.FileDrop)
    End Sub

    Private Sub ToolStripDropDownButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton5.Click
        'Fix Drag and Drop https://stackoverflow.com/questions/11686631/drag-drop-and-get-file-path-in-vb-net

        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim checkFile As New IO.FileInfo(OpenFileDialog1.FileName)
            If (checkFile.Extension = ".csv" Or checkFile.Extension = ".xlsx" Or checkFile.Extension = ".xls") Then
                Add_Data(OpenFileDialog1.FileName)
                account_Settings.set_LUD(Date.Today) 'should update to date of most recent transaction
            Else
                MsgBox("Incorret File Type")
            End If
        End If

        updateALL()
    End Sub

    Private Sub ToolStripDropDownButton7_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton7.Click
        account_History.Save_Date(account_Pending, account_Settings)
    End Sub

    Private Sub ToolStripDropDownButton9_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton9.Click
        Form6.Start_Form(account_History)
    End Sub
    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim answer = MsgBox("Do you want to save", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
        If (answer = MsgBoxResult.Yes) Then
            account_History.Save_Date(account_Pending, account_Settings)
        End If
    End Sub


    Public Sub Add_Data(ByVal fileName As String)
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet

        xlWorkBook = xlApp.Workbooks.Open(fileName)
        xlWorkSheet = xlWorkBook.Worksheets("sheet1")
        Dim list_unaccounted_in As New List(Of Transaction)
        Dim list_unaccounted_out As New List(Of Transaction)
        Dim counter As Integer = 4
        Dim rowAccounted As Boolean = False
        'filters out dates which have already been updated
        While (Not (xlWorkSheet.Cells(counter, 1).Value = Nothing)) And (DateTime.Compare(xlWorkSheet.Cells(counter, 1).Value, account_Settings.get_LUD) >= 0)
            Dim transactionID As String = xlWorkSheet.Cells(counter, 3).Value.Split(New Char() {","c})(1)
            Dim newTransaction As New ExcelItem(xlWorkSheet.Cells(counter, 4).Value, xlWorkSheet.Cells(counter, 3).Value.Split(New Char() {","c})(0), transactionID, xlWorkSheet.Cells(counter, 1).Value)
            If newTransaction.amount >= 0 Then
                For Each expense As Expense In account_Pending.Get_Expenses()
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
        If (counter = 4) Then
            MsgBox("No New Data Found")
        End If
        For Each Transaction As Transaction In list_unaccounted_out.Concat(list_unaccounted_in)
            Manage_Transaction_Form.startForm(Transaction, account_Pending, account_History)
            Manage_Transaction_Form.ShowDialog()
        Next
    End Sub
End Class
