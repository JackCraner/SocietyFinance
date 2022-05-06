Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data
Imports System.Xml.Serialization
Imports System.IO
Public Class Base_Form

    Public account_History As New AccountHistory
    Public account_Pending As New AccountPending
    Public account_Settings As New AccountSettings
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        A_Balance_L.Text = "£0"
        Balance_L.Text = "£0"
        Read_Data()



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
        Balance_L.Text = Math.Round(account_Pending.Get_Current_Balance, 2)
        A_Balance_L.Text = Math.Round(account_Pending.Get_Ava_Balance, 2)
        Label4.Text = Math.Round(account_Pending.Get_Predicted_Balance, 2)
    End Sub




    Private Sub MembershipFeeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        Dim answer = MsgBox("Are you sure?", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
        If (answer = MsgBoxResult.Yes) Then
            ClearALL()
        End If

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
                Add_Data2(OpenFileDialog1.FileName)
                account_Settings.set_LUD(Date.Today) 'should update to date of most recent transaction
            Else
                MsgBox("Incorret File Type")
            End If
        End If

        updateALL()
    End Sub

    Private Sub ToolStripDropDownButton7_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton7.Click
        Save_Data()
    End Sub

    Private Sub ToolStripDropDownButton9_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton9.Click
        Form6.Start_Form(account_History)
    End Sub
    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim answer = MsgBox("Do you want to save", vbQuestion + vbYesNo + vbDefaultButton2, "Waait")
        If (answer = MsgBoxResult.Yes) Then
            Save_Data()
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
        Dim finalBalance As Double = 0
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
    Public Sub Add_Data2(ByVal filename As String)
        Dim lines As List(Of String) = File.ReadAllLines(filename).ToList
        Dim list_unaccounted_in As New List(Of Transaction)
        Dim list_unaccounted_out As New List(Of Transaction)
        Dim rowAccounted As Boolean = False


        lines.Reverse()
        Dim finalBalance As Double = 0
        For Each l As String In lines
            Dim data As String() = l.Split(",")
            If Not (data(0) = "Date" Or data(0) = "") Then
                Dim descrip As New List(Of String)
                For i As Integer = 2 To data.Count - 5

                    descrip.Add(data(i))
                Next
                Dim name As String = descrip(0).Replace("""", "").Trim()
                Dim ref As String
                If (descrip.Count > 1) Then
                    ref = descrip(1)
                Else
                    ref = "NULL"
                End If

                finalBalance = data(3 + descrip.Count).Replace("""", "").Trim()
                Dim newTransaction As New ExcelItem(data(2 + descrip.Count), name, ref, data(0).Replace("""", "").Trim())
                If (DateTime.Compare(newTransaction.dateMade, account_Settings.get_LUD) >= 0) Then
                    If newTransaction.amount >= 0 Then
                        For Each expense As Expense In account_Pending.Get_Expenses()
                            If ref.Contains(expense.IDCode) Then  'THis is a worry, make more strict
                                Dim newT = New Transaction(newTransaction.amount, TransactionHandle.Income, newTransaction.name, newTransaction.reference, newTransaction.dateMade)
                                newT.transID = account_Pending.Create_UTID
                                expense.Add_Income(newT)
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
                End If

            End If
        Next


        For Each Transaction As Transaction In list_unaccounted_out.Concat(list_unaccounted_in)
            Manage_Transaction_Form.startForm(Transaction, account_Pending, account_History)
            Manage_Transaction_Form.ShowDialog()
        Next
        If Not account_Pending.Get_Current_Balance = finalBalance Then
            Dim answer = MsgBox("Update Current balance? Current Balance: " + account_Pending.current_Balance.ToString + vbNewLine + " Data Balance: " + finalBalance.ToString, vbQuestion + vbYesNo + vbDefaultButton2, "Account Balance Mismatch")
            If (answer = MsgBoxResult.Yes) Then
                account_Pending.current_Balance = finalBalance
            End If
        End If
        updateALL()

    End Sub

    Public Sub Read_Data()
        Try
            Dim reader1 As New System.Xml.Serialization.XmlSerializer(GetType(AccountPending))
            Dim file1 As New System.IO.StreamReader(
           "AccountPending.xml")
            Dim overview1 As AccountPending
            overview1 = CType(reader1.Deserialize(file1), AccountPending)
            file1.Close()


            Dim reader2 As New System.Xml.Serialization.XmlSerializer(GetType(AccountHistory))
            Dim file2 As New System.IO.StreamReader(
           "AccountHistory.xml")
            Dim overview2 As AccountHistory
            overview2 = CType(reader2.Deserialize(file2), AccountHistory)
            file2.Close()

            Dim reader3 As New System.Xml.Serialization.XmlSerializer(GetType(AccountSettings))
            Dim file3 As New System.IO.StreamReader(
           "AccountSettings.xml")
            Dim overview3 As AccountSettings
            overview3 = CType(reader3.Deserialize(file3), AccountSettings)
            file3.Close()

            account_Pending = overview1
            account_History = overview2
            account_Settings = overview3

        Catch ex As Exception

            MsgBox("Load Failed")
            MsgBox(ex.ToString)
        End Try
        updateALL()
    End Sub

    Public Sub Save_Data()
        Try
            Dim xml_ser1 As New XmlSerializer(GetType(AccountPending))
            Dim string_writer1 As New StringWriter
            Dim file1 As New System.IO.StreamWriter(
               "AccountPending.xml")
            xml_ser1.Serialize(file1, account_Pending)
            file1.Close()

            Dim xml_ser2 As New XmlSerializer(GetType(AccountHistory))
            Dim string_writer2 As New StringWriter
            Dim file2 As New System.IO.StreamWriter(
               "AccountHistory.xml")
            xml_ser2.Serialize(file2, account_History)
            file2.Close()

            Dim xml_ser3 As New XmlSerializer(GetType(AccountSettings))
            Dim string_writer3 As New StringWriter
            Dim file3 As New System.IO.StreamWriter(
               "AccountSettings.xml")
            xml_ser3.Serialize(file3, account_Settings)
            file3.Close()
            MsgBox("Save Complete")
        Catch ex As Exception
            MsgBox("Save Failed")
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Function getTransactionGlobal(ByVal id As String) As Transaction
        Dim both As New List(Of Expense)(account_History.Get_Expenses.Concat(account_Pending.Get_Expenses))

        For Each exp As Expense In both
            If (exp.get_transactions.Exists(Function(x As Transaction) x.transID = id)) Then
                Return exp.get_transactions.Find(Function(x As Transaction) x.transID = id)
            End If
            If (exp.isPaid) Then
                If (exp.paidFlag.transID = id) Then
                    Return exp.paidFlag
                End If

            End If
        Next
        For Each tran As Transaction In account_History.Get_Transactions()
            If tran.transID = id Then
                Return tran
            End If
        Next
        Return Nothing
    End Function
    Public Function getExpenseGlobal(ByVal id As String) As Expense
        Dim both As New List(Of Expense)(account_History.Get_Expenses.Concat(account_Pending.Get_Expenses))
        Return both.Find(Function(x As Expense) x.IDCode = id)
    End Function

    Private Sub ClearSaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearSaveToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripDropDownButton2_Click(sender As Object, e As EventArgs)
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim checkFile As New IO.FileInfo(OpenFileDialog1.FileName)
            If (checkFile.Extension = ".xml") Then

            End If
        End If

    End Sub
End Class
