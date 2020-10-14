Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.ExpressApp.Web.Layout
Imports DevExpress.ExpressApp.Web.Utils
Imports DevExpress.Web

Namespace DetailViewTabCountVB.Module.Web.Controllers
    Partial Public Class EmployeeDetailViewWebController
        Inherits ViewController(Of DetailView)

        Private pageControl As ASPxPageControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            View.DelayedItemsInitialization = False
            AddHandler View.CurrentObjectChanged, AddressOf View_CurrentObjectChanged
            AddHandler(CType(View.LayoutManager, WebLayoutManager)).PageControlCreated, AddressOf EmployeeDetailViewWebController_PageControlCreated
            AddHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
        End Sub

        Private Sub CurrentRequestWindow_PagePreRender(ByVal sender As Object, ByVal e As EventArgs)
            If pageControl IsNot Nothing Then
                UpdatePageControl(pageControl)
            End If
        End Sub

        Protected Overrides Sub OnDeactivated()
            RemoveHandler View.CurrentObjectChanged, AddressOf View_CurrentObjectChanged
            RemoveHandler(CType(View.LayoutManager, WebLayoutManager)).PageControlCreated, AddressOf EmployeeDetailViewWebController_PageControlCreated
            RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
            MyBase.OnDeactivated()
        End Sub

        Protected Overrides Sub OnViewControlsDestroying()
            pageControl = Nothing
            MyBase.OnViewControlsDestroying()
        End Sub

        Private Sub EmployeeDetailViewWebController_PageControlCreated(ByVal sender As Object, ByVal e As PageControlCreatedEventArgs)
            If e.Model.Id = "Tabs" Then
                pageControl = e.PageControl
                pageControl.ClientInstanceName = "pageControl"
            End If
        End Sub

        Private Sub View_CurrentObjectChanged(ByVal sender As Object, ByVal e As EventArgs)
            If pageControl IsNot Nothing Then
                UpdatePageControl(pageControl)
            End If
        End Sub

        Private Sub UpdatePageControl(ByVal pageControl As ASPxPageControl)
            For Each tab As TabPage In pageControl.TabPages
                tab.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text)
                Dim listPropertyEditor = TryCast(View.FindItem(tab.Name), ListPropertyEditor)

                If listPropertyEditor IsNot Nothing Then
                    Dim count = listPropertyEditor.ListView.CollectionSource.GetCount()

                    If count > 0 Then
                        tab.Text += " (" & count & ")"
                    End If
                    If TypeOf listPropertyEditor.ListView.Editor Is ASPxGridListEditor AndAlso DirectCast(listPropertyEditor.ListView.Editor, ASPxGridListEditor).Grid IsNot Nothing Then
                        Dim editor = DirectCast(listPropertyEditor.ListView.Editor, ASPxGridListEditor)
                        editor.Grid.JSProperties("cpCaption") = tab.Text
                        ClientSideEventsHelper.AssignClientHandlerSafe(editor.Grid, "EndCallback", $"function(s, e) {{ 
                        if (!s.cpCaption) return;
                        var tab = {pageControl.ClientInstanceName}.GetTabByName('{tab.Name}'); 
                        tab.SetText(s.cpCaption); 
                        delete s.cpCaption;}}", NameOf(EmployeeDetailViewWebController))
                    End If
                End If
            Next
        End Sub
    End Class

End Namespace
