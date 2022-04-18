Public Class AccountPending
    Inherits FinanceDataBase

    Dim current_Balance As Double
    Public Sub New()
        MyBase.New()
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

End Class
