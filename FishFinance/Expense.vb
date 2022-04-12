﻿Public Class Expense

    Public name As String
    Public projected_cost As Double
    Public deadline As Date


    Public list_of_payments As New List(Of Transaction)
    Public IDCode As String
    Private paidFlag As Transaction
    Public expense_topic As Topic

    Public Sub New(ByVal name As String, ByVal amount As Double, Optional ByVal date_to_pay As Date = Nothing)
        Me.name = name
        Me.projected_cost = amount
        Me.deadline = date_to_pay

    End Sub

    Public Sub Add_Income(ByRef payment As Transaction)
        If (payment.getLabel = TransactionHandle.Income) Then

            list_of_payments.Add(payment)
            Base_Form.Handle_Transaction(payment)
        Else
            MsgBox("Invalid Income")
        End If

    End Sub
    Public Sub Remove_Payment(ByRef payment As Transaction)
        If (list_of_payments.Contains(payment)) Then
            payment.updateTransactionLabel(TransactionHandle.Refund)
            Base_Form.Handle_Transaction(payment)
            list_of_payments.Remove(payment)
        Else
            MsgBox("Payment Not Found")
        End If

    End Sub
    Public Sub Add_Paid(ByRef payment As Transaction)
        If (payment.getLabel = TransactionHandle.Outgoing) Then
            paidFlag = payment
            Base_Form.Handle_Transaction(payment)
        Else
            MsgBox("Failed assigning Flag")
        End If

        'Base_Form

    End Sub
    Public Function getPaidFlag()
        Return paidFlag
    End Function
    Public Function Get_Recoup()
        Dim count As Double = 0
        For Each Payment As Transaction In list_of_payments
            count += Payment.getAmount
        Next
        Return count
    End Function

    Public Function isPaid()
        Return Not paidFlag Is Nothing
    End Function
    Public Function hasTopic()
        Return Not expense_topic Is Nothing
    End Function

    ''ability to partial pay but only for pending payments
End Class