Public Class Topic
    Public name As String
    Public topicID As String
    Public Sub New(ByVal name As String)
        Me.name = name
        Me.topicID = convert_name_ID(name)

    End Sub


    Public Shared Function convert_name_ID(ByVal name As String)
        Dim tempID As New List(Of String)
        For Each ch As Char In name
            tempID.Add(Asc(ch))
        Next
        Return String.Join("", tempID)
    End Function


End Class
