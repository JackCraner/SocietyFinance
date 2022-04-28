Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
<Serializable()>
Public Class AccountHistory
    Inherits FinanceDataBase
    Dim list_transactions As New List(Of Transaction)
    Public Sub New()

    End Sub
    Public Overrides Sub Add_Expense(ByRef exp As Expense)
        If (IsNothing(exp.IDCode)) Then
            exp.IDCode = Base_Form.account_Pending.Create_UID()
        End If
        MyBase.Add_Expense(exp)
    End Sub
    Public Sub Add_Transaction(ByRef trans As Transaction)

        list_transactions.Add(trans)
        Base_Form.updateALL()
    End Sub
    Public Sub Remove_Transaction(ByRef trans As Transaction)
        list_transactions.Remove(trans)
        Base_Form.updateALL()
    End Sub
    Public Function Get_Transactions() As List(Of Transaction)
        Return list_transactions
    End Function

    Public Sub Export_Excel(ByRef excelObject As Excel.Application, ByRef excelWorkSheet As Excel.Worksheet)

        Dim counter = 1
        For Each trans As Transaction In list_transactions
            excelWorkSheet.Cells(counter, 1) = trans.name
            counter += 1
        Next
        For Each trans As Expense In MyBase.Get_Expenses
            excelWorkSheet.Cells(counter, 1) = trans.name
            counter += 1
        Next
    End Sub
End Class
