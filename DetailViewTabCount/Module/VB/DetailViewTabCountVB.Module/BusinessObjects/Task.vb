Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

<DefaultClassOptions>
Public Class Task
	Inherits BaseObject

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Private _subject As String
	Public Property Subject() As String
		Get
			Return _subject
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Subject), _subject, value)
		End Set
	End Property

	Private _assignedTo As Employee
	<Association("Employee-Tasks")>
	Public Property AssignedTo() As Employee
		Get
			Return _assignedTo
		End Get
		Set(ByVal value As Employee)
			SetPropertyValue(NameOf(AssignedTo), _assignedTo, value)
		End Set
	End Property
End Class
