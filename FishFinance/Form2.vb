Public Class Define_Expenditure

    Private Sub B_Submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Submit.Click
        If RadioButton3.Checked Then
            Base_Form.Create_Expenditure(New Expenditure(TextBox1.Text, Double.Parse(TextBox2.Text), Date.Today))
        End If
        If RadioButton4.Checked Then
            Base_Form.Create_Expenditure(New RepaymentExpenditure(TextBox1.Text, Double.Parse(TextBox2.Text), Date.Today, ExpenditureTypes.Recoup))
        End If
        If RadioButton5.Checked Then
            If String.IsNullOrEmpty(TextBox2.Text) Then
                Base_Form.Create_Expenditure(New RepaymentExpenditure(TextBox1.Text, 0, Date.Today, ExpenditureTypes.Pending))
            Else
                Base_Form.Create_Expenditure(New RepaymentExpenditure(TextBox1.Text, Double.Parse(TextBox2.Text), Date.Today, ExpenditureTypes.Pending))
            End If

        End If
        Me.Close()
    End Sub

    Private Sub Define_Expenditure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GroupBox4.Visible = False
        TextBox2.Visible = False
    End Sub

    Public Sub reset()
        TextBox1.Text = ""
        TextBox2.Text = ""
        RadioButton1.Checked = True
        GroupBox4.Visible = False
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim AllowedChars As String = "0123456789." & vbBack
        If AllowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
        
    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If (RadioButton5.Checked) Then
            TextBox2.Visible = False
            GroupBox4.Visible = True
        Else
            RadioButton2.Checked = False
            RadioButton1.Checked = False
            GroupBox4.Visible = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If (RadioButton2.Checked) Then
            TextBox2.Visible = True
        Else
            TextBox2.Visible = False
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If (RadioButton4.Checked) Then
            TextBox2.Visible = True
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If (RadioButton3.Checked) Then
            TextBox2.Visible = True
        End If
    End Sub
End Class