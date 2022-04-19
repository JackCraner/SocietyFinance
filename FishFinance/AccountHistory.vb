Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.IO
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

    Private Class HistoryItem
        Dim name, amount As String
        Dim dateOccured As Date
        Dim attachedExpense As Expense
        Dim transaction_type As TransactionHandle
        Public Sub New(ByRef transaction As Transaction)

        End Sub
        Public Sub New(ByRef expense As Expense)

        End Sub
    End Class
End Class
