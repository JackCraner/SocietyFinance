Imports Excel = Microsoft.Office.Interop.Excel
<Serializable()>
Public Class AccountPending
    Inherits FinanceDataBase

    Public current_Balance As Double

    Public Sub New()

        Me.current_Balance = 0
    End Sub
    Public Overrides Sub Add_Expenses(ByRef expList As List(Of Expense))
        For Each exp As Expense In expList
            Add_Expense(exp)
        Next

    End Sub
    Public Function Get_Current_Balance()
        Return current_Balance
    End Function
    Public Overrides Sub Add_Expense(ByRef exp As Expense)
        If (IsNothing(exp.IDCode)) Then
            exp.IDCode = Create_UID()
        End If

        MyBase.Add_Expense(exp)
    End Sub
    Public Overrides Sub Remove_Expense(ByRef exp As Expense)
        MyBase.Remove_Expense(exp)
    End Sub
    Public Sub Return_Expense(ByRef exp As Expense)
        MyBase.Add_Expense(exp)
    End Sub
    Public Sub Handle_Transaction(ByRef transaction As Transaction)
        If (IsNothing(transaction.transID)) Then
            transaction.transID = Create_UTID()
        End If
        current_Balance += transaction.getAmount()
        Base_Form.updateALL()
    End Sub

    Public Function Get_Ava_Balance()
        Dim temp_balance As Double = current_Balance
        For Each exp As Expense In MyBase.Get_Expenses
            If Not (exp.isPaid) Then
                If (exp.projected_cost = 0) Then
                    temp_balance -= exp.Get_Recoup()
                Else
                    temp_balance -= exp.projected_cost
                End If
            End If



        Next
        Return temp_balance
    End Function
    Public Function Get_Predicted_Balance()
        Dim temp_balance As Double = Get_Ava_Balance()
        For Each exp As Expense In MyBase.Get_Expenses
            temp_balance += exp.projected_payback
        Next
        Return temp_balance
    End Function

    Public Function Create_UID(Optional ByVal depth As Integer = 0)
        Dim validchars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        Dim sb As New System.Text.StringBuilder()
        Dim rand As New Random()
        For i As Integer = 1 To 3
            Dim idx As Integer = rand.Next(0, validchars.Length)
            Dim randomChar As Char = validchars(idx)
            sb.Append(randomChar)
        Next i

        If (Check_UID(sb.ToString)) Then
            Return sb.ToString()
        Else
            depth += 1
            If (depth > 10) Then
                MsgBox("No UID Left")
                Return "AAA"
            Else
                Return Create_UID(depth)
            End If

        End If
    End Function
    Public Function Check_UID(ByVal id As String)
        Dim both As New List(Of Expense)(Base_Form.account_History.Get_Expenses.Concat(MyBase.Get_Expenses))
        Return Not both.Exists(Function(x As Expense) x.IDCode = id)
    End Function

    Public Function Create_UTID(Optional ByVal depth As Integer = 0)
        Dim validchars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        Dim sb As New System.Text.StringBuilder()
        Dim rand As New Random()
        For i As Integer = 1 To 4
            Dim idx As Integer = rand.Next(0, validchars.Length)
            Dim randomChar As Char = validchars(idx)
            sb.Append(randomChar)
        Next i

        If (Check_UTID(sb.ToString)) Then
            Return sb.ToString()
        Else
            depth += 1
            If (depth > 10) Then
                MsgBox("No UTID Left")
                Return "AAA"
            Else
                Return Create_UTID(depth)
            End If

        End If
    End Function
    Public Function Check_UTID(ByVal id As String)
        Dim both As New List(Of Expense)(Base_Form.account_History.Get_Expenses.Concat(MyBase.Get_Expenses))
        Dim found = False
        For Each exp As Expense In both
            If (exp.get_transactions.Exists(Function(x As Transaction) x.transID = id)) Then
                found = True
            End If
        Next
        Return Not found
    End Function
    Public Sub Export_Excel(ByRef excelObject As Excel.Application, ByRef excelWorkSheet As Excel.Worksheet)

        Dim counter = 1

        For Each trans As Expense In MyBase.Get_Expenses
            excelWorkSheet.Cells(counter, 1) = trans.name
            counter += 1
        Next
    End Sub
End Class
