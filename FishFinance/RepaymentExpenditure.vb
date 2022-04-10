Public Class RepaymentExpenditure
    Inherits Expenditure


    Public list_of_payments As New List(Of Transaction)
    Public IDCode As String

    Public expenditureType As ExpenditureTypes
    Public Sub New(ByVal name As String, ByVal amount As Double, ByRef date_to_pay As Date, ByVal type As ExpenditureTypes)
        MyBase.New(name, amount, date_to_pay)
        Me.expenditureType = type
    End Sub

    Public Sub Add_Payment(ByRef payment As Transaction)
        list_of_payments.Add(payment)
    End Sub

    Public Function Get_Recoup()
        Dim count As Double = 0
        For Each Payment As Transaction In list_of_payments
            count += Payment.amount
        Next
        Return count
    End Function

    ''ability to partial pay but only for pending payments
End Class
Public Enum ExpenditureTypes
    Pending
    Recoup
End Enum