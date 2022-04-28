Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Form6

    Dim list_finished_expenses As List(Of Expense)
    Dim list_single_transactions As List(Of Transaction)
    Public Sub Start_Form(ByRef accHist As AccountHistory)
        Me.Visible = True
        Me.list_finished_expenses = accHist.Get_Expenses
        Me.list_single_transactions = accHist.Get_Transactions
        Set_Date_Order()
    End Sub
    Public Sub Update_Form()
        Set_Date_Order()
    End Sub

    Public Sub Set_Date_Order()
        'list.Sort(New Comparison(Of Date)(Function(x As Date, y As Date) y.CompareTo(x)))
        DataGridView1.DataSource = Nothing
        DataGridView2.DataSource = Nothing
        Dim dtb As New System.Data.DataTable
        dtb.Columns.Add("Name")
        dtb.Columns.Add("Amount", GetType(Double))
        dtb.Columns.Add("Date", GetType(Date))
        dtb.Columns.Add("IDCode")
        For Each exp As Expense In list_finished_expenses

            Add_Exp_Table(exp, dtb)
        Next
        For Each trans As Transaction In list_single_transactions
            Add_Trans_Table(trans, dtb)
        Next
        Dim dvw As DataView = dtb.DefaultView
        Dim dtbSorted As DataTable = dvw.ToTable()
        DataGridView1.DataSource = dtbSorted
        DataGridView1.Sort(DataGridView1.Columns(2), ListSortDirection.Ascending)

        Dim dtb2 As New System.Data.DataTable
        dtb2.Columns.Add("Name")
        dtb2.Columns.Add("Amount", GetType(Double))
        dtb2.Columns.Add("Date", GetType(Date))
        dtb2.Columns.Add("IDCode")
        For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
            Add_Exp_Table(exp, dtb2)
        Next
        Dim dvw2 As DataView = dtb2.DefaultView
        Dim dtbSorted2 As DataTable = dvw2.ToTable()
        DataGridView2.DataSource = dtbSorted2
        DataGridView2.Sort(DataGridView2.Columns(2), ListSortDirection.Descending)
        ColourCode()
    End Sub



    Private Sub DataGridView1_CellDoubleClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        Dim highlightedExpense = list_finished_expenses.Find(Function(exp As Expense) exp.IDCode = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(highlightedExpense, False)

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'export to excel
        Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim xlWorkBook As Excel.Workbook

        Dim misValue As Object = System.Reflection.Missing.Value
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        Dim ws As Excel.Worksheet = CType(xlWorkBook.Sheets.Add(Count:=10), Excel.Worksheet)
        CType(xlWorkBook.Sheets(1), Excel.Worksheet).Name = "Account History"
        CType(xlWorkBook.Sheets(2), Excel.Worksheet).Name = "Account Pending"
        Base_Form.account_History.Export_Excel(xlApp, xlWorkBook.Sheets(1))
        Base_Form.account_Pending.Export_Excel(xlApp, xlWorkBook.Sheets(2))
        xlWorkBook.SaveAs("D:\Personal Files\FrisbeeExec\Finance\csharp-Excel.xlsx")
        xlWorkBook.Close(True, misValue, misValue)
        xlApp.Quit()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If (TextBox1.Text = "") Then
            Set_Date_Order()
        Else
            DataGridView2.DataSource = Nothing
            Dim dtb2 As New System.Data.DataTable
            dtb2.Columns.Add("Name")
            dtb2.Columns.Add("Amount", GetType(Double))
            dtb2.Columns.Add("Date", GetType(Date))
            dtb2.Columns.Add("IDCode")
            Dim dtb As New System.Data.DataTable
            dtb.Columns.Add("Name")
            dtb.Columns.Add("Amount", GetType(Double))
            dtb.Columns.Add("Date", GetType(Date))
            dtb.Columns.Add("IDCode")
            If (RadioButton1.Checked) Then
                Dim t As Double
                If (Double.TryParse(TextBox1.Text, t)) Then

                    For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
                        For Each trans As Transaction In exp.get_transactions

                            If (trans.getLabel = t) Then
                                Add_Trans_Table(trans, dtb2)

                            End If


                        Next
                        If (exp.isPaid) Then
                            If (exp.paidFlag.getLabel = t) Then
                                Add_Trans_Table(exp.paidFlag, dtb2)
                            End If
                        End If
                    Next
                    For Each exp As Expense In Base_Form.account_History.Get_Expenses_Date_Order
                        For Each trans As Transaction In exp.get_transactions

                            If (trans.getLabel = t) Then
                                Add_Trans_Table(trans, dtb)

                            End If

                        Next
                        If (exp.isPaid) Then
                            If (exp.paidFlag.getLabel = t) Then
                                Add_Trans_Table(exp.paidFlag, dtb)
                            End If
                        End If
                    Next
                    For Each Tran As Transaction In Base_Form.account_History.Get_Transactions
                        If (Tran.getLabel = t) Then
                            Add_Trans_Table(Tran, dtb)
                        End If
                    Next
                End If

            ElseIf (RadioButton2.Checked) Then
                For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
                    For Each trans As Transaction In exp.get_transactions

                        If (trans.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                            Add_Trans_Table(trans, dtb2)

                        End If

                    Next
                    If (exp.isPaid) Then
                        If (exp.paidFlag.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                            Add_Trans_Table(exp.paidFlag, dtb2)
                        End If
                    End If
                Next
                For Each exp As Expense In Base_Form.account_History.Get_Expenses_Date_Order
                    For Each trans As Transaction In exp.get_transactions

                        If (trans.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                            Add_Trans_Table(trans, dtb)

                        End If

                    Next
                    If (exp.isPaid) Then
                        If (exp.paidFlag.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                            Add_Trans_Table(exp.paidFlag, dtb)
                        End If
                    End If
                Next
                For Each Tran As Transaction In Base_Form.account_History.Get_Transactions
                    If (Tran.name.ToLower.Contains(TextBox1.Text)) Then
                        Add_Trans_Table(Tran, dtb)
                    End If
                Next


            ElseIf (RadioButton3.Checked) Then
                For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
                    If (exp.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                        Add_Exp_Table(exp, dtb2)
                    End If
                Next
                For Each exp As Expense In Base_Form.account_History.Get_Expenses_Date_Order
                    If (exp.name.ToLower.Contains(TextBox1.Text.ToLower)) Then
                        Add_Exp_Table(exp, dtb)
                    End If
                Next
            ElseIf (RadioButton4.Checked) Then
                For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
                    If (exp.isPaid And exp.paidFlag.amount = TextBox1.Text) Then
                        Add_Exp_Table(exp, dtb2)
                    ElseIf ((Not exp.isPaid) And exp.projected_cost = TextBox1.Text) Then
                        Add_Exp_Table(exp, dtb2)
                    End If

                    For Each trans As Transaction In exp.get_transactions
                        If (trans.amount = TextBox1.Text) Then
                            Add_Trans_Table(trans, dtb2)
                        End If
                    Next
                Next
                For Each exp As Expense In Base_Form.account_History.Get_Expenses_Date_Order
                    If (exp.isPaid And exp.paidFlag.amount = TextBox1.Text) Then
                        Add_Exp_Table(exp, dtb)
                    ElseIf ((Not exp.isPaid) And exp.projected_cost = TextBox1.Text) Then
                        Add_Exp_Table(exp, dtb)
                    End If
                    For Each trans As Transaction In exp.get_transactions
                        If (trans.amount = TextBox1.Text) Then
                            Add_Trans_Table(trans, dtb)
                        End If
                    Next
                Next
                For Each Tran As Transaction In Base_Form.account_History.Get_Transactions
                    If (Tran.amount = TextBox1.Text) Then
                        Add_Trans_Table(Tran, dtb)
                    End If
                Next
            ElseIf (RadioButton5.Checked()) Then
                For Each exp As Expense In Base_Form.account_Pending.Get_Expenses_Date_Order
                    If (Not IsNothing(exp.topic)) Then
                        If (exp.topic.name = TextBox1.Text) Then
                            Add_Exp_Table(exp, dtb2)
                        End If
                    End If


                Next
                For Each exp As Expense In Base_Form.account_History.Get_Expenses_Date_Order
                    If (Not IsNothing(exp.topic)) Then
                        If (exp.topic.name = TextBox1.Text) Then
                            Add_Exp_Table(exp, dtb)
                        End If
                    End If


                Next
                For Each Tran As Transaction In Base_Form.account_History.Get_Transactions
                    If (Not IsNothing(Tran.topic)) Then
                        If (Tran.topic.name = TextBox1.Text) Then
                            Add_Trans_Table(Tran, dtb)
                        End If
                    End If
                Next
            End If
            Dim dvw2 As DataView = dtb2.DefaultView
            Dim dtbSorted2 As DataTable = dvw2.ToTable()
            DataGridView2.DataSource = dtbSorted2
            Dim dvw As DataView = dtb.DefaultView
            Dim dtbSorted As DataTable = dvw.ToTable()
            DataGridView1.DataSource = dtbSorted
            DataGridView1.Sort(DataGridView1.Columns(2), ListSortDirection.Descending)
            DataGridView2.Sort(DataGridView2.Columns(2), ListSortDirection.Descending)
        End If

        ColourCode()

    End Sub
    Public Sub Add_Exp_Table(ByRef exp As Expense, ByRef dtb As DataTable)
        If exp.isPaid Then
            dtb.Rows.Add(exp.name, exp.getPaidFlag.getABSAmount(), exp.getPaidFlag.dateMade, exp.IDCode)

        Else
            dtb.Rows.Add(exp.name, exp.Get_Recoup, exp.deadline, exp.IDCode)
        End If
    End Sub
    Public Sub Add_Trans_Table(ByRef trans As Transaction, ByRef dtb As DataTable)
        dtb.Rows.Add(trans.name, trans.getABSAmount(), trans.dateMade, trans.transID)
    End Sub

    Public Sub ColourCode()
        For Each row As DataGridViewRow In DataGridView2.Rows
            If (Len(row.Cells(3).Value) = 3) Then
                Dim temp As Expense = Base_Form.getExpenseGlobal(row.Cells(3).Value)
                row.DefaultCellStyle.BackColor = Color.Red
            ElseIf (Len(row.Cells(3).Value) = 4) Then
                Dim temp As Transaction = Base_Form.getTransactionGlobal(row.Cells(3).Value)

                If temp.getLabel.ToString = "Income" Then
                    row.DefaultCellStyle.BackColor = Color.Green
                ElseIf (temp.getLabel.ToString = "Donation") Then
                    row.DefaultCellStyle.BackColor = Color.DarkGreen
                ElseIf (temp.getLabel.ToString = "Loan") Then
                    row.DefaultCellStyle.BackColor = Color.Yellow
                ElseIf (temp.getLabel.ToString = "Membership") Then
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                ElseIf (temp.getLabel.ToString = "Outgoing") Then
                    row.DefaultCellStyle.BackColor = Color.Red
                ElseIf (temp.getLabel.ToString = "Refund") Then
                    row.DefaultCellStyle.BackColor = Color.Purple
                Else
                    row.DefaultCellStyle.BackColor = Color.Red
                End If
            Else
                row.DefaultCellStyle.BackColor = Color.Red
            End If

        Next
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.DefaultCellStyle.BackColor = Color.Red
        Next
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        If (Len(DataGridView2.Rows(e.RowIndex).Cells(3).Value) = 3) Then
            Dim highlightedExpense = Base_Form.account_Pending.Get_Expense(DataGridView2.Rows(e.RowIndex).Cells(3).Value)
            Manage_Pending_Form.Visible = True
            Manage_Pending_Form.startForm(highlightedExpense, False)
        Else
            Dim highlightedTrans As Transaction = Base_Form.getTransactionGlobal(DataGridView2.Rows(e.RowIndex).Cells(3).Value)
            Manage_Payment_Form.Visible = True
            Manage_Payment_Form.StartForm(highlightedTrans)
        End If

    End Sub
End Class


