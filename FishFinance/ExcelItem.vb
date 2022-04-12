Public Class ExcelItem


    Public name As String
    Public amount As Double
    Public dateMade As Date
    Public reference As String
    Sub New(ByVal amount As Double, ByVal name As String, ByVal reference As String, ByVal dateMade As Date)
        Me.amount = amount
        Me.name = name
        Me.dateMade = dateMade
        Me.reference = reference
    End Sub

End Class
