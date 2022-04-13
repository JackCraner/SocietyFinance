Imports System.Xml
Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class
Public Class AccountHistory
    'does not include pending events



    Public Sub Save_Date()
        Dim writer As New XmlTextWriter("product.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("Table")

    End Sub
    Private Sub Save_Data_Expense(ByRef writer As XmlTextWriter, ByRef exp As Expense)
        writer.WriteStartElement("Expense")
        writer.WriteStartElement("Name")
        writer.WriteString(exp.name)
        writer.WriteEndElement()
        writer.WriteStartElement("Topic")
        writer.W
        writer.WriteStartElement("Amount")
        writer.WriteString(exp.projected_cost)
        writer.WriteEndElement()
        writer.WriteStartElement("Deadline")
        writer.WriteString(exp.deadline)
        writer.WriteEndElement()
        writer.WriteStartElement("PaidFlag")
        Save_Data_Transaction(writer, exp.getPaidFlag())
        writer.WriteEndElement()
        writer.WriteStartElement("Repayments")
        For Each transaction As Transaction In exp.list_of_payments
            Save_Data_Transaction(writer, transaction)
        Next

    End Sub
    Private Sub Save_Data_Transaction(ByRef writer As XmlTextWriter, ByRef transaction As Transaction)
        writer.WriteStartElement("Transaction")
        writer.WriteStartElement("Name")
        writer.WriteString(transaction.name)
        writer.WriteEndElement()
        writer.WriteStartElement("Refernece")
        writer.WriteString(transaction.reference)
        writer.WriteEndElement()
        writer.WriteStartElement("TransactionType")
        writer.WriteString(transaction.getLabel)
        writer.WriteStartElement("Amount")
        writer.WriteString(transaction.getABSAmount)
        writer.WriteEndElement()
        writer.WriteStartElement("Date")
        writer.WriteString(transaction.dateMade)
        writer.WriteEndElement()
        writer.WriteEndElement()

    End Sub
    Private Class HistoryItem
        Dim name, amount As String
        Dim dateOccured As Date
        Dim attachedExpense As Expense
        Dim transaction_type As TransactionHandle
        Public Sub New(ByRef transaction As Transaction)

        End Sub
        Public Sub New(ByRef expense As Expense)

        End Sub
    End Class
End Class

