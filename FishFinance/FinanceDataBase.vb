Public Class FinanceDataBase
    Dim list_expense As New List(Of Expense)
    Public Sub New()

    End Sub

    Public Overridable Sub Add_Expense(ByRef exp As Expense)
        list_expense.Add(exp)
        Base_Form.updateALL()
    End Sub
    Public Overridable Sub Remove_Expense(ByRef exp As Expense)
        list_expense.Remove(exp)
        Base_Form.updateALL()
    End Sub
    Public Overridable Sub Add_Expenses(ByRef expList As List(Of Expense))
        list_expense.AddRange(expList)
        Base_Form.updateALL()
    End Sub


    Public Function Get_Expenses() As List(Of Expense)
        Return list_expense
    End Function

    Public Function Get_Expense(ByVal id As String)
        Return list_expense.Find(Function(x As Expense) x.IDCode = id)
    End Function

    Public Function get_Topics()
        Dim list_topics As New List(Of Topic)
        For Each exp As Expense In list_expense
            If exp.hasTopic() And Not list_topics.Contains(exp.expense_topic) Then
                list_topics.Add(exp.expense_topic)
            End If
        Next
        Return list_topics
    End Function
    Public Function Set_Topic(ByVal name As String)
        Dim test As List(Of Topic) = get_Topics()
        Dim found_topic = test.Find(Function(t As Topic) t.topicID = Topic.convert_name_ID(name))
        If (IsNothing(found_topic)) Then
            Return New Topic(name)
        Else
            Return found_topic
        End If

    End Function
End Class
