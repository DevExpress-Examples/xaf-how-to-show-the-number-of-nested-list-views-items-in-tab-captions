Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.ExpressApp.Web.Layout
Imports DevExpress.Web

Namespace DetailViewTabCountVB.Module.Web.Controllers
	Partial Public Class EmployeeDetailViewWebController
		Inherits ViewController(Of DetailView)

		Private pageControl As ASPxPageControl
		Private gridsInTabs As Dictionary(Of String, String)
		Public Sub New()
			InitializeComponent()
			gridsInTabs = New Dictionary(Of String, String)()
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			View.DelayedItemsInitialization = False
			AddHandler CType(View.LayoutManager, WebLayoutManager).PageControlCreated, AddressOf EmployeeDetailViewWebController_PageControlCreated
			View.CustomizeViewItemControl(Of ListPropertyEditor)(Me, Sub(editor)
																		 AddHandler editor.ListView.ControlsCreated, AddressOf ListView_ControlsCreated
																	 End Sub)
		End Sub

		Protected Overrides Sub OnDeactivated()
			RemoveHandler CType(View.LayoutManager, WebLayoutManager).PageControlCreated, AddressOf EmployeeDetailViewWebController_PageControlCreated
			pageControl.Dispose()
			MyBase.OnDeactivated()
		End Sub

		Private Sub EmployeeDetailViewWebController_PageControlCreated(ByVal sender As Object, ByVal e As PageControlCreatedEventArgs)
			' Check this Id in the AppName.Module/Model.DesignedDiffs.xafml file
			If e.Model.Id = "Tabs" Then
				pageControl = e.PageControl
				pageControl.ClientInstanceName = "pageControl"
			End If
		End Sub

		Private Sub ListView_ControlsCreated(ByVal sender As Object, ByVal e As EventArgs)
			Dim gridListEditor As ASPxGridListEditor = TryCast(DirectCast(sender, ListView).Editor, ASPxGridListEditor)
			If gridListEditor IsNot Nothing Then
				If gridListEditor.Grid IsNot Nothing Then
					If Not gridsInTabs.ContainsKey(gridListEditor.Grid.ID) Then
						gridsInTabs.Add(gridListEditor.Grid.ID, gridListEditor.Name)
					End If
					AddHandler gridListEditor.Grid.DataBound, AddressOf Grid_DataBound
				End If
			End If
		End Sub

		Private Sub Grid_DataBound(ByVal sender As Object, ByVal e As EventArgs)
			Dim grid = TryCast(sender, ASPxGridView)

			If pageControl IsNot Nothing Then

				For Each tab As TabPage In pageControl.TabPages
					Dim isBold As Boolean = False
					Dim tabCaption = gridsInTabs(grid.ID)
					Dim count = 0

					If DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text) <> tabCaption Then
						Continue For
					End If

					Dim listPropertyEditor = TryCast(View.FindItem(tabCaption.Replace(" ", "")), ListPropertyEditor)
					If listPropertyEditor IsNot Nothing Then
						count = listPropertyEditor.ListView.CollectionSource.GetCount()
					End If

					tab.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text)

					If count > 0 Then
						isBold = True
						tab.Text &= " (" & count & ")"
						grid.JSProperties("cpCaption") = tab.Text
						grid.ClientSideEvents.EndCallback = $"function(s, e) {{var tab = {pageControl.ClientInstanceName}.GetTabByName('{tab.Name}'); tab.SetText(s.cpCaption); delete s.cpCaption;}}"
					End If
					tab.TabStyle.Font.Bold = isBold
				Next tab
			End If
		End Sub
	End Class
End Namespace
