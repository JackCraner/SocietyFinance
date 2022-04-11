Public Class Define_Expenditure

    Private Sub B_Submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Submit.Click
        Dim amount As Double = 0
        If Not TextBox2.Text = "" Then
            amount = TextBox2.Text
        End If

        Dim new_exp As New Expense(TextBox1.Text, Double.Parse(amount), DateTimePicker1.Value.Date)

        If (Not ComboBox1.Text = "") Then

            new_exp.expense_topic = Base_Form.Set_Topic(ComboBox1.Text)
        End If
        Base_Form.Create_Expenditure(new_exp)
        Me.Close()
    End Sub

    Private Sub Define_Expenditure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TextBox2.Visible = False
    End Sub

    Public Sub reset()
        TextBox1.Text = ""
        TextBox2.Text = ""
        Dim tempList As List(Of Topic) = Base_Form.Get_Topics()
        'Dim nameList As List(Of String) = From name In tempList Select name.name
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim AllowedChars As String = "0123456789." & vbBack
        If AllowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
        
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TextBox2.Visible = Not TextBox2.Visible
    End Sub
End Class