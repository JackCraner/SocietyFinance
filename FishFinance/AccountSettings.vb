<Serializable()>
Public Class AccountSettings
    Public last_updated_date As Date
    Public membership_cost As Double = 10
    Public Sub New()


    End Sub

    Public Function get_LUD()
        Return last_updated_date
    End Function

    Public Sub set_LUD(ByRef lud As Date)
        Me.last_updated_date = lud
    End Sub
    Public Function get_MembershipCost()
        Return membership_cost
    End Function
    Public Sub Set_MembershipCost(ByVal amount As Double)
        membership_cost = amount
    End Sub
End Class
