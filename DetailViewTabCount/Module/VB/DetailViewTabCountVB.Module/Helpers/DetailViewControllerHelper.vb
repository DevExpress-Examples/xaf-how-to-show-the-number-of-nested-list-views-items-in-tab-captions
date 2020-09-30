Public Module DetailViewControllerHelper
	Public Function ClearItemCountInTabCaption(ByVal caption As String) As String
		Dim index As Integer = caption.IndexOf("("c)
		If index <> -1 Then
			Return caption.Remove(index - 1)
		End If
		Return caption
	End Function
End Module
