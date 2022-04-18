Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel

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
        Dim dtb As New System.Data.DataTable
        dtb.Columns.Add("Name")
        dtb.Columns.Add("Amount", GetType(Double))
        dtb.Columns.Add("Date", GetType(Date))
        dtb.Columns.Add("IDCode")
        For Each exp As Expense In list_finished_expenses

            dtb.Rows.Add(exp.name, exp.getPaidFlag.getABSAmount(), exp.getPaidFlag.dateMade, exp.IDCode)
        Next
        For Each trans As Transaction In list_single_transactions
            dtb.Rows.Add(trans.name, trans.getABSAmount(), trans.dateMade, trans.getLabel.ToString)
        Next
        Dim dvw As DataView = dtb.DefaultView
        Dim dtbSorted As DataTable = dvw.ToTable()
        DataGridView1.DataSource = dtbSorted
        DataGridView1.Sort(DataGridView1.Columns(2), ListSortDirection.Ascending)

    End Sub



    Private Sub DataGridView1_CellDoubleClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        Dim highlightedExpense = list_finished_expenses.Find(Function(exp As Expense) exp.IDCode = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(highlightedExpense, False)

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class


