
Imports System.Xml.Serialization

<Serializable()>
Public Class Transaction

    Public name As String
    Public amount As Double
    Public dateMade As Date
    Public reference As String
    Public label As TransactionHandle

    Public expLink As String
    Public transID As String
    Public topic As Topic
    Public Sub New()

    End Sub
    Public Sub New(ByVal amount As Double, ByVal label As TransactionHandle, Optional ByVal name As String = "FISH", Optional ByVal reference As String = "", Optional ByVal dateMade As Date = Nothing)
        Me.amount = amount
        Me.label = label
        Me.name = name
        Me.dateMade = dateMade
        Me.reference = reference
    End Sub

    Public Function getAmount()
        Return amount * getLabelResult()
    End Function
    Public Function getABSAmount()
        Return amount
    End Function
    Public Sub setAmount(ByVal a As Double)
        amount = Math.Abs(a)
    End Sub
    Public Sub updateTransactionLabel(ByVal newLabel As TransactionHandle)
        label = newLabel
    End Sub
    Public Function getLabel()
        Return label
    End Function
    Private Function getLabelResult()
        If label > 0 Then
            Return 1
        Else
            Return -1
        End If
    End Function
End Class

Public Enum TransactionHandle
    Membership = 4
    Loan = 3
    Donation = 2
    Income = 1
    Outgoing = -1
    Refund = -2

End Enum
