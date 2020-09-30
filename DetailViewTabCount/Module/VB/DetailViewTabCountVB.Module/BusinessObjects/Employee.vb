Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

<DefaultClassOptions>
Public Class Employee
	Inherits Person

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	<Association("Employee-Tasks")>
	Public ReadOnly Property Tasks() As XPCollection(Of Task)
		Get
			Return GetCollection(Of Task)(NameOf(Tasks))
		End Get
	End Property
End Class