Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel
Public Class AccountHistory
    Inherits FinanceDataBase
    Dim list_transactions As New List(Of Transaction)
    Public Sub New()

    End Sub
    Public Sub Add_Transaction(ByRef trans As Transaction)
        list_transactions.Add(trans)
        Base_Form.updateALL()
    End Sub
    Public Sub Remove_Transaction(ByRef trans As Transaction)
        list_transactions.Remove(trans)
        Base_Form.updateALL()
    End Sub
    Public Function Get_Transactions() As List(Of Transaction)
        Return list_transactions
    End Function

    Public Sub Save_Date(ByRef account_pending As AccountPending, ByRef account_Settings As AccountSettings)
        Dim writer As New XmlTextWriter("SaveData.xml", System.Text.Encoding.UTF8)
        Try
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2

            writer.WriteStartElement("SaveData")
            writer.WriteStartElement("Settings")
            writer.WriteStartElement("Name")
            writer.WriteString("Frisbee")
            writer.WriteEndElement()
            writer.WriteStartElement("LastDate")
            writer.WriteString(account_Settings.get_LUD)
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteStartElement("Pending")
            For Each exp As Expense In account_pending.Get_Expenses()
                Save_Data_Expense(writer, exp)
            Next
            If (account_pending.Get_Expenses().Count = 0) Then
                writer.WriteString(Nothing)
            End If
            writer.WriteEndElement()
            writer.WriteStartElement("History")
            For Each exp As Expense In MyBase.Get_Expenses()
                Save_Data_Expense(writer, exp)
            Next
            If (MyBase.Get_Expenses().Count = 0) Then
                writer.WriteString(Nothing)
            End If
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
            MsgBox("Save Success")
        Catch ex As Exception
            MsgBox("Save Failed")
            writer.Close()
        End Try

    End Sub
    Public Sub Read_Data(ByRef account_pending As AccountPending, ByRef account_Settings As AccountSettings)
        Dim reader As New XmlTextReader("SaveData.xml")
        Try

            Dim list_expenses As New List(Of Expense)
            While reader.Read()

                If (reader.NodeType = XmlNodeType.Element) Then
                    If (reader.Name = "Pending") Then
                        If (reader.IsEmptyElement) Then
                        Else
                            While (reader.Read())
                                If (reader.NodeType = XmlNodeType.EndElement And reader.Name = "Pending") Then
                                    Exit While
                                End If
                                If (reader.Name = "Expense") Then
                                    'list_expenses.Add(Load_Data_Expense(reader))
                                    account_pending.Add_Expense(Load_Data_Expense(reader))
                                ElseIf (reader.Name = "Transaction") Then
                                    MsgBox("Test1")

                                End If
                            End While
                        End If

                    ElseIf (reader.Name = "History") Then
                        While (reader.Read())

                            If (reader.Name = "Expense") Then
                                'list_expenses.Add(Load_Data_Expense(reader))

                                Add_Expense((Load_Data_Expense(reader)))
                            ElseIf (reader.Name = "Transaction") Then
                                MsgBox("Test1")

                            End If
                        End While
                    ElseIf (reader.Name = "Settings") Then
                        While (reader.Read())
                            If (reader.NodeType = XmlNodeType.Element And reader.Name = "LastDate") Then
                                account_Settings.set_LUD(Load_Next_String(reader))
                            End If
                            If (reader.NodeType = XmlNodeType.EndElement And reader.Name = "Settings") Then
                                Exit While
                            End If
                        End While
                    End If
                End If

            End While
            reader.Close()
        Catch ex As Exception
            reader.Close()
            MsgBox("Load Failed")
        End Try

    End Sub
    Public Function Load_Data_Transaction(ByRef reed As XmlTextReader)
        Dim Name As String = ""
        Dim Amount As Double
        Dim TransactionType As TransactionHandle
        Dim DateMade As Date
        Dim Reference As String = ""

        While (reed.Read())

            If (reed.NodeType = XmlNodeType.EndElement And reed.Name = "Transaction") Then
                Exit While
            End If
            If (reed.NodeType = XmlNodeType.Element) Then
                Select Case reed.Name
                    Case NameOf(Name)
                        Name = Load_Next_String(reed)
                    Case NameOf(Amount)
                        Amount = Double.Parse(Load_Next_String(reed))
                    Case NameOf(DateMade)
                        DateMade = Load_Next_String(reed)
                    Case NameOf(Reference)
                        Reference = Load_Next_String(reed)
                    Case NameOf(TransactionType)
                        TransactionType = Load_Next_String(reed)
                End Select
            End If



            'list_parameters.Add(reed.Value)
        End While

        Dim returnTransaction = New Transaction(Amount, TransactionType, Name, Reference, DateMade)
        Return returnTransaction
    End Function
    Public Function Load_Data_Expense(ByRef reed As XmlTextReader)
        Dim list_parameters As New List(Of String)
        Dim Name As String
        Dim Amount As Double
        Dim Deadline As Date
        Dim ID As String = "AAA"
        Dim Topic As Topic
        Dim PaidFlag As Transaction
        Dim Repayments As New List(Of Transaction)

        While (reed.Read())
            If (reed.NodeType = XmlNodeType.EndElement And reed.Name = "Expense") Then
                Exit While
            End If
            If (reed.NodeType = XmlNodeType.Element) Then
                Select Case reed.Name
                    Case NameOf(Name)
                        Name = Load_Next_String(reed)
                    Case NameOf(Amount)
                        Amount = Double.Parse(Load_Next_String(reed))
                    Case NameOf(ID)
                        ID = Load_Next_String(reed)
                    Case NameOf(Deadline)
                        Deadline = Date.Parse(Load_Next_String(reed))
                    Case NameOf(Topic)
                        Dim output = Load_Next_String(reed)
                        If output Then
                            Topic = New Topic(output)  'try link it to existings topics first
                        End If
                    Case NameOf(PaidFlag)
                        If Not (reed.IsEmptyElement) Then
                            While (reed.Read())
                                If (reed.NodeType = XmlNodeType.Element And (reed.Name = "Transaction")) Then
                                    PaidFlag = Load_Data_Transaction(reed)
                                    Exit While
                                End If
                            End While
                        End If

                    Case NameOf(Repayments)
                        If Not (reed.IsEmptyElement) Then
                            While (reed.Read())
                                If (reed.NodeType = XmlNodeType.Element And (reed.Name = "Transaction")) Then
                                    Repayments.Add(Load_Data_Transaction(reed))

                                End If
                                If (reed.NodeType = XmlNodeType.EndElement And reed.Name = "Repayments") Then
                                    Exit While
                                End If
                            End While
                        End If

                End Select
            End If
        End While
        Dim return_Expense = New Expense(Name, Amount, Deadline)
        For Each exp As Transaction In Repayments
            return_Expense.Add_Income(exp)
        Next
        If Not (IsNothing(PaidFlag)) Then
            return_Expense.Add_Paid(PaidFlag)
        End If
        return_Expense.IDCode = ID
        Return return_Expense

    End Function
    Public Function Load_Next_String(ByRef reed As XmlTextReader)

        While (reed.Read())
            If (reed.NodeType = XmlNodeType.Text) Then

                Return reed.Value

            End If
            If (reed.NodeType = XmlNodeType.EndElement) Then
                Exit While
            End If
        End While

        Return Nothing
    End Function
    Public Sub Save_Data_Expense(ByRef writer As XmlTextWriter, ByRef exp As Expense)
        writer.WriteStartElement("Expense")
        writer.WriteStartElement("Name")
        writer.WriteString(exp.name)
        writer.WriteEndElement()
        writer.WriteStartElement("Topic")
        If (exp.hasTopic()) Then
            Save_Data_Transaction(writer, exp.getPaidFlag())
        Else
            writer.WriteString(Nothing)
        End If

        writer.WriteEndElement()
        writer.WriteStartElement("Amount")
        writer.WriteString(exp.projected_cost)
        writer.WriteEndElement()
        writer.WriteStartElement("Deadline")
        writer.WriteString(exp.deadline)
        writer.WriteEndElement()
        writer.WriteStartElement("PaidFlag")
        If (exp.isPaid()) Then
            Save_Data_Transaction(writer, exp.getPaidFlag())
        Else
            writer.WriteString(Nothing)
        End If

        writer.WriteEndElement()
        writer.WriteStartElement("Repayments")
        For Each transaction As Transaction In exp.list_of_payments
            Save_Data_Transaction(writer, transaction)
        Next
        writer.WriteEndElement()
        writer.WriteStartElement("ID")
        writer.WriteString(exp.IDCode)
        writer.WriteEndElement()
        writer.WriteEndElement()

    End Sub
    Public Sub Save_Data_Transaction(ByRef writer As XmlTextWriter, ByRef transaction As Transaction)
        writer.WriteStartElement("Transaction")
        writer.WriteStartElement("Name")
        writer.WriteString(transaction.name)
        writer.WriteEndElement()
        writer.WriteStartElement("Reference")
        writer.WriteString(transaction.reference)
        writer.WriteEndElement()
        writer.WriteStartElement("TransactionType")
        writer.WriteString(transaction.getLabel)
        writer.WriteEndElement()
        writer.WriteStartElement("Amount")
        writer.WriteString(transaction.getABSAmount)
        writer.WriteEndElement()
        writer.WriteStartElement("DateMade")
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
