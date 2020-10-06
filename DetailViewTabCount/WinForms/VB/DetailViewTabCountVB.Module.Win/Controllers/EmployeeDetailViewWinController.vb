Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Win.Layout
Imports DevExpress.XtraLayout

Namespace DetailViewTabCountVB.Module.Win.Controllers
    Partial Public Class EmployeeDetailViewWinController
        Inherits ViewController(Of DetailView)

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            View.DelayedItemsInitialization = False
            AddHandler CType(View.LayoutManager, WinLayoutManager).ItemCreated, AddressOf DetailViewTabCountController_ItemCreated
        End Sub

        Private Sub DetailViewTabCountController_ItemCreated(ByVal sender As Object, ByVal e As ItemCreatedEventArgs)
            If TypeOf e.Item Is LayoutGroup AndAlso TypeOf e.ModelLayoutElement.Parent Is IModelTabbedGroup Then
                Dim layoutGroup = DirectCast(e.Item, LayoutGroup)
                For Each item As IModelLayoutItem In (DirectCast(e.ModelLayoutElement, IModelLayoutGroup))
                    Dim layoutViewItem As IModelLayoutViewItem = TryCast(item, IModelLayoutViewItem)
                    If layoutViewItem Is Nothing Then
                        Continue For
                    End If
                    Dim propertyEditor As ListPropertyEditor = TryCast(View.FindItem(layoutViewItem.ViewItem.Id), ListPropertyEditor)
                    If propertyEditor IsNot Nothing Then
                        propertyEditor.Frame.GetController(Of NestedListViewTabCountController)().Initialize(layoutGroup)
                    End If
                Next item
            End If
        End Sub

        Protected Overrides Sub OnDeactivated()
            RemoveHandler CType(View.LayoutManager, WinLayoutManager).ItemCreated, AddressOf DetailViewTabCountController_ItemCreated
            MyBase.OnDeactivated()
        End Sub
    End Class

    Public Class NestedListViewTabCountController
        Inherits ViewController(Of ListView)

        Protected Overrides Sub OnFrameAssigned()
            MyBase.OnFrameAssigned()
            Active("Context is NestedFrame") = Frame.Context = TemplateContext.NestedFrame
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            UpdateLayoutGroupText()
            AddHandler View.CollectionSource.CollectionReloaded, AddressOf CollectionSource_CollectionReloaded
            AddHandler View.CollectionSource.CollectionChanged, AddressOf CollectionSource_CollectionChanged
            AddHandler View.CollectionSource.CollectionChanging, AddressOf CollectionSource_CollectionChanging
            SubscribeToListChanged()
        End Sub
        Private layoutGroup As LayoutGroup
        Public Sub Initialize(ByVal layoutGroup As LayoutGroup)
            Me.layoutGroup = layoutGroup
            UpdateLayoutGroupText()
        End Sub
        Private Sub UpdateLayoutGroupText()
            If layoutGroup IsNot Nothing Then
                Dim count As Integer = View.CollectionSource.GetCount()
                layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text)
                If count > 0 Then
                    layoutGroup.Text &= " (" & count & ")"
                End If
            End If
        End Sub
        Private Sub CollectionSource_CollectionReloaded(ByVal sender As Object, ByVal e As EventArgs)
            UpdateLayoutGroupText()
        End Sub
        Private Sub CollectionSource_CollectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateLayoutGroupText()
            SubscribeToListChanged()
        End Sub
        Private Sub CollectionSource_CollectionChanging(ByVal sender As Object, ByVal e As EventArgs)
            UnsubscribeFromListChanged()
        End Sub
        Private Sub CollectionSourceBindingList_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
            UpdateLayoutGroupText()
        End Sub
        Private Sub SubscribeToListChanged()
            If TypeOf View.CollectionSource.Collection Is IBindingList Then
                AddHandler DirectCast(View.CollectionSource.Collection, IBindingList).ListChanged, AddressOf CollectionSourceBindingList_ListChanged
            ElseIf TypeOf View.CollectionSource.Collection Is IListSource Then
                Dim innerList As IBindingList = TryCast(DirectCast(View.CollectionSource.Collection, IListSource).GetList(), IBindingList)
                If innerList IsNot Nothing Then
                    AddHandler DirectCast(innerList, IBindingList).ListChanged, AddressOf CollectionSourceBindingList_ListChanged
                End If
            End If
        End Sub
        Private Sub UnsubscribeFromListChanged()
            If TypeOf View.CollectionSource.Collection Is IBindingList Then
                RemoveHandler DirectCast(View.CollectionSource.Collection, IBindingList).ListChanged, AddressOf CollectionSourceBindingList_ListChanged
            ElseIf TypeOf View.CollectionSource.Collection Is IListSource Then
                Dim innerList As IBindingList = TryCast(DirectCast(View.CollectionSource.Collection, IListSource).GetList(), IBindingList)
                If innerList IsNot Nothing Then
                    RemoveHandler DirectCast(innerList, IBindingList).ListChanged, AddressOf CollectionSourceBindingList_ListChanged
                End If
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            RemoveHandler View.CollectionSource.CollectionReloaded, AddressOf CollectionSource_CollectionReloaded
            RemoveHandler View.CollectionSource.CollectionChanged, AddressOf CollectionSource_CollectionChanged
            RemoveHandler View.CollectionSource.CollectionChanging, AddressOf CollectionSource_CollectionChanging
            UnsubscribeFromListChanged()
        End Sub
    End Class
End Namespace