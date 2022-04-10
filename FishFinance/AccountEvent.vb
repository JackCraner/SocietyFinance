Public Class AccountEvent

    Public name As String
    Public amount As Double
    Public eventDate As Date

    Public Sub New(ByVal name As String, ByVal amount As Double, ByVal dateOccured As Date)
        Me.name = name
        Me.amount = amount
        Me.eventDate = dateOccured
    End Sub
End Class
