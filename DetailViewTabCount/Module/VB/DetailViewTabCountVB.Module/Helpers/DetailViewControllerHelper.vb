Public Module DetailViewControllerHelper
	Public Function ClearItemCountInTabCaption(ByVal caption As String) As String
		Dim index As Integer = caption.IndexOf("("c)
		If index <> -1 Then
			Return caption.Remove(index - 1)
		End If
		Return caption
	End Function
	Public Function AddItemCountToTabCaption(ByVal caption As String, ByVal count As Integer) As String
		Return $"{caption} ({count})"
	End Function
End Module
