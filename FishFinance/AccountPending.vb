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
            If (depth > 100) Then

                Return "AAAA"
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

        Dim counter = 3
        Dim startX = 3
        For Each exp As Expense In MyBase.Get_Expenses
            Dim expenseSpace As Excel.Range
            excelWorkSheet.Cells(counter, startX - 1) = "Name"
            excelWorkSheet.Cells(counter, startX) = exp.name
            excelWorkSheet.Cells(counter + 1, startX - 1) = "Deadline"
            excelWorkSheet.Cells(counter + 1, startX) = exp.deadline
            Dim rangeToFormat As Excel.Range


            excelWorkSheet.Cells(counter + 2, startX - 1) = "ID Code"
            excelWorkSheet.Cells(counter + 2, startX) = exp.IDCode
            excelWorkSheet.Cells(counter + 3, startX - 1) = "Topic"
            If (exp.hasTopic) Then

                excelWorkSheet.Cells(counter + 3, startX) = exp.topic.name
            Else
                excelWorkSheet.Cells(counter + 3, startX) = ""
            End If
            excelWorkSheet.Cells(counter + 5, startX - 1) = "Projected Cost"
            excelWorkSheet.Cells(counter + 5, startX) = exp.projected_cost
            excelWorkSheet.Cells(counter + 6, startX - 1) = "Projected Payback"
            excelWorkSheet.Cells(counter + 6, startX) = exp.projected_payback
            excelWorkSheet.Cells(counter + 7, startX - 1) = "Projected Net"
            excelWorkSheet.Cells(counter + 7, startX) = exp.projected_payback - exp.projected_cost



            excelWorkSheet.Cells(counter + 5, startX + 3) = "Paid Cost"
            If (exp.isPaid) Then
                excelWorkSheet.Cells(counter + 5, startX + 4) = exp.paidFlag.amount
            Else
                excelWorkSheet.Cells(counter + 5, startX + 4) = 0
            End If
            excelWorkSheet.Cells(counter + 6, startX + 3) = "Current Payback"
            excelWorkSheet.Cells(counter + 6, startX + 4) = exp.Get_Recoup
            excelWorkSheet.Cells(counter + 7, startX + 3) = "Net"




            excelWorkSheet.Cells(counter + 9, startX - 1) = "Payment"
            excelWorkSheet.Cells(counter + 10, startX - 1) = "Name"
            excelWorkSheet.Cells(counter + 10, startX) = "Amount"
            excelWorkSheet.Cells(counter + 10, startX + 1) = "Date"
            If (exp.isPaid) Then
                excelWorkSheet.Cells(counter + 11, startX - 1) = exp.paidFlag.name
                excelWorkSheet.Cells(counter + 11, startX) = exp.paidFlag.amount
                excelWorkSheet.Cells(counter + 11, startX + 1) = exp.paidFlag.dateMade
            End If

            excelWorkSheet.Cells(counter + 13, startX - 1) = "Repayments"
            excelWorkSheet.Cells(counter + 14, startX - 1) = "Name"
            excelWorkSheet.Cells(counter + 14, startX) = "Amount"
            excelWorkSheet.Cells(counter + 14, startX + 1) = "Date"
            Dim counter2 As Integer = 0
            For Each trans As Transaction In exp.get_transactions()
                counter2 += 1
                excelWorkSheet.Cells(counter + 14 + counter2, startX - 1) = trans.name
                excelWorkSheet.Cells(counter + 14 + counter2, startX) = trans.amount
                excelWorkSheet.Cells(counter + 14 + counter2, startX + 1) = trans.dateMade
            Next
            expenseSpace = excelWorkSheet.Range(excelWorkSheet.Cells(counter, startX - 1), excelWorkSheet.Cells(counter + counter2 + 14, startX + 5))
            expenseSpace.Interior.Color = ColorTranslator.ToOle(Color.Gray)


            rangeToFormat = excelWorkSheet.Range(excelWorkSheet.Cells(counter + 1, startX), excelWorkSheet.Cells(counter + 1, startX))
            If (DateTime.Compare(exp.deadline, DateTime.Today) >= 0 And (IsNothing(exp.isPaid))) Then

                rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Red)
            Else
                rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.LightGreen)
            End If

            rangeToFormat = excelWorkSheet.Range(excelWorkSheet.Cells(counter + 7, startX), excelWorkSheet.Cells(counter + 7, startX))
            If ((exp.projected_payback - exp.projected_cost) > 0) Then
                rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.LightGreen)
            Else
                rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Red)
            End If

            rangeToFormat = excelWorkSheet.Range(excelWorkSheet.Cells(counter + 7, startX + 4), excelWorkSheet.Cells(counter + 7, startX + 4))
            If (exp.isPaid) Then
                excelWorkSheet.Cells(counter + 7, startX + 4) = exp.Get_Recoup - exp.paidFlag.amount
                If (exp.Get_Recoup - exp.paidFlag.amount > 0) Then
                    rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.LightGreen)
                Else
                    rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Red)
                End If


            Else
                excelWorkSheet.Cells(counter + 5, startX + 4) = exp.Get_Recoup
                rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.LightGreen)
            End If
            counter = counter + counter2 + 17
        Next
    End Sub
End Class
