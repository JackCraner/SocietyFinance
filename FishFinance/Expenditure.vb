Public Class Expenditure
    Inherits AccountEvent

    Public Sub New(ByVal name As String, ByVal amount As Double, ByVal dateOccured As Date)
        MyBase.New(name, amount, dateOccured)
    End Sub


    Public Overridable Function ToListBox()
        Return name + "  £" + CStr(amount)
    End Function


End Class
