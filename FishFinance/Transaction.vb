Public Class Transaction

    Public name As String
    Public amount As Double
    Public dateMade As Date
    Public reference As String

    Public Sub New(ByVal amount As Double, Optional ByVal name As String = "FISH", Optional ByVal reference As String = "", Optional ByVal dateMade As Date = Nothing)
        Me.amount = amount
        Me.name = name
        Me.dateMade = dateMade
        Me.reference = reference
    End Sub

End Class
