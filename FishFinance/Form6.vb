Imports System.Xml
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class Form6
    Dim list_finished_expenses As New List(Of Expense)
    Dim list_single_transactions As New List(Of Transaction)

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Sub Retire_Expense(ByRef exp As Expense)
        'A retired expense must atleast have some repayments or been paid

        list_finished_expenses.Add(exp)
    End Sub
    Public Sub Retire_Transaction(ByRef transaction As Transaction)
        list_single_transactions.Add(transaction)
    End Sub

    Public Function get_history_expenses()
        Return list_finished_expenses
    End Function
    Public Sub Set_Date_Order()
        'list.Sort(New Comparison(Of Date)(Function(x As Date, y As Date) y.CompareTo(x)))
        Dim dtb As New System.Data.DataTable
        dtb.Columns.Add("Name")
        dtb.Columns.Add("Amount", GetType(Double))
        dtb.Columns.Add("Date", GetType(Date))
        dtb.Columns.Add("IDCode")
        For Each exp As Expense In list_finished_expenses

            dtb.Rows.Add(exp.name, exp.getPaidFlag.getABSAmount(), exp.getPaidFlag.dateMade, exp.IDCode)
        Next
        For Each trans As Transaction In list_single_transactions
            dtb.Rows.Add(trans.name, trans.getABSAmount(), trans.dateMade, "AAA")
        Next
        Dim dvw As DataView = dtb.DefaultView
        Dim dtbSorted As DataTable = dvw.ToTable()
        DataGridView1.DataSource = dtbSorted
        DataGridView1.Sort(DataGridView1.Columns(2), ListSortDirection.Ascending)

    End Sub
    Public Sub Update_Form()
        Set_Date_Order()
    End Sub
    Public Sub Start_Form()
        Me.Visible = True
        Set_Date_Order()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        Dim highlightedExpense = list_finished_expenses.Find(Function(exp) exp.IDCode = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString())
        Manage_Pending_Form.Visible = True
        Manage_Pending_Form.startForm(highlightedExpense)
    End Sub
End Class
Public Class AccountHistory
    'does not include pending events

    'save previous versions

    Shared Sub Save_Date(ByRef expenses As List(Of Expense), ByRef historyExpenses As List(Of Expense))
        Dim writer As New XmlTextWriter("C:\Uni\Year 3\Dissertation\Frisbee\FinanceApp\SocietyFinance\SaveData.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("SaveData")
        writer.WriteStartElement("Pending")
        For Each exp As Expense In expenses
            Save_Data_Expense(writer, exp)
        Next
        If (expenses.Count = 0) Then
            writer.WriteString(Nothing)
        End If
        writer.WriteEndElement()
        writer.WriteStartElement("History")
        For Each exp As Expense In historyExpenses
            Save_Data_Expense(writer, exp)
        Next
        If (historyExpenses.Count = 0) Then
            writer.WriteString(Nothing)
        End If
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteEndDocument()
            writer.Close()
    End Sub
    Shared Sub Load_Data()

        Dim reader As New XmlTextReader("C:\Uni\Year 3\Dissertation\Frisbee\FinanceApp\SocietyFinance\SaveData.xml")
        Dim list_expenses As New List(Of Expense)
        While reader.Read()
            If (reader.NodeType = XmlNodeType.Element) Then
                If (reader.Name = "Pending") Then
                    While (reader.Read())
                        If (reader.NodeType = XmlNodeType.EndElement And reader.Name = "Pending") Then
                            Exit While
                        End If

                        If (reader.Name = "Expense") Then
                            'list_expenses.Add(Load_Data_Expense(reader))
                            Base_Form.Create_Expenditure(Load_Data_Expense(reader))
                        ElseIf (reader.Name = "Transaction") Then
                            MsgBox("Test1")
                        Else
                            MsgBox("p")
                        End If
                    End While
                ElseIf (reader.Name = "History") Then
                    While (reader.Read())
                        If (reader.NodeType = XmlNodeType.EndElement And reader.Name = "History") Then
                            Exit While
                        End If

                        If (reader.Name = "Expense") Then
                            'list_expenses.Add(Load_Data_Expense(reader))
                            Form6.Retire_Expense((Load_Data_Expense(reader)))
                        ElseIf (reader.Name = "Transaction") Then
                            MsgBox("Test1")
                        Else
                            MsgBox("h")
                        End If
                    End While
                End If
            End If

        End While

    End Sub
    Shared Function Load_Data_Transaction(ByRef reed As XmlTextReader)
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
    Shared Function Load_Data_Expense(ByRef reed As XmlTextReader)
        Dim list_parameters As New List(Of String)
        Dim Name As String
        Dim Amount As Double
        Dim Deadline As Date
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
                    Case NameOf(Deadline)
                        Deadline = Date.Parse(Load_Next_String(reed))
                    Case NameOf(Topic)
                        Dim output = Load_Next_String(reed)
                        If output Then
                            Topic = New Topic(output)  'try link it to existings topics first
                        End If
                    Case NameOf(PaidFlag)
                        While (reed.Read())
                            If (reed.NodeType = XmlNodeType.Element And (reed.Name = "Transaction")) Then
                                PaidFlag = Load_Data_Transaction(reed)
                            ElseIf (reed.NodeType = XmlNodeType.EndElement And reed.Name = "PaidFlag") Then
                                Exit Select
                            End If

                        End While
                    Case NameOf(Repayments)
                        While (reed.Read())
                            If (reed.NodeType = XmlNodeType.Element And (reed.Name = "Transaction")) Then
                                Repayments.Add(Load_Data_Transaction(reed))
                            ElseIf (reed.NodeType = XmlNodeType.Element And Not (reed.Name = "Transaction")) Then
                                Exit Select
                            End If
                        End While
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
        Return return_Expense

    End Function
    Shared Function Load_Next_String(ByRef reed As XmlTextReader)

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
    Shared Sub Save_Data_Expense(ByRef writer As XmlTextWriter, ByRef exp As Expense)
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
        writer.WriteEndElement()

    End Sub
    Shared Sub Save_Data_Transaction(ByRef writer As XmlTextWriter, ByRef transaction As Transaction)
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

