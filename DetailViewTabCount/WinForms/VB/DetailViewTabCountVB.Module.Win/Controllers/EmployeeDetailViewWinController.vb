Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Win.Layout
Imports DevExpress.XtraLayout

Namespace DetailViewTabCountVB.Module.Win.Controllers
    Public Class EmployeeDetailViewWinController
        Inherits ObjectViewController(Of DetailView, Employee)
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            View.DelayedItemsInitialization = False
            AddHandler CType(View.LayoutManager, WinLayoutManager).ItemCreated, AddressOf DetailViewTabCountController_ItemCreated
        End Sub
        Private Sub DetailViewTabCountController_ItemCreated(ByVal sender As Object, ByVal e As ItemCreatedEventArgs)
            If TypeOf e.Item Is LayoutGroup AndAlso TypeOf e.ModelLayoutElement.Parent Is IModelTabbedGroup Then
                Dim layoutGroup = DirectCast(e.Item, LayoutGroup)
                For Each item In TryCast(e.ModelLayoutElement, IModelLayoutGroup)
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
        Private layoutGroup As LayoutGroup
        Public Sub New()
            TargetViewNesting = Nesting.Nested
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            UpdateLayoutGroupText()
            AddHandler View.CollectionSource.CollectionChanging, AddressOf CollectionSource_CollectionChanging
            AddHandler View.CollectionSource.CollectionChanged, AddressOf CollectionSource_CollectionChanged
            AddHandler View.CollectionSource.CollectionReloaded, AddressOf CollectionSource_CollectionReloaded
            SubscribeToListChanged()
        End Sub
        Private Sub SubscribeToListChanged()
            Dim bindingList = GetBindingList(View.CollectionSource.Collection)
            If TypeOf bindingList Is IBindingList Then
                AddHandler bindingList.ListChanged, AddressOf CollectionSourceBindingList_ListChanged
            End If
        End Sub
        Public Sub Initialize(ByVal layoutGroup As LayoutGroup)
            Me.layoutGroup = layoutGroup
            UpdateLayoutGroupText()
        End Sub
        Private Sub CollectionSource_CollectionChanging(ByVal sender As Object, ByVal e As EventArgs)
            UnsubscribeFromListChanged()
        End Sub
        Private Sub CollectionSource_CollectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateLayoutGroupText()
            SubscribeToListChanged()
        End Sub
        Private Sub CollectionSource_CollectionReloaded(ByVal sender As Object, ByVal e As EventArgs)
            UpdateLayoutGroupText()
        End Sub
        Private Sub CollectionSourceBindingList_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
            UpdateLayoutGroupText()
        End Sub
        Private Sub UpdateLayoutGroupText()
            If layoutGroup IsNot Nothing Then
                Dim count As Integer = View.CollectionSource.GetCount()
                layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text)
                If count > 0 Then
                    layoutGroup.Text = DetailViewControllerHelper.AddItemCountToTabCaption(layoutGroup.Text, count)
                End If
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            RemoveHandler View.CollectionSource.CollectionChanging, AddressOf CollectionSource_CollectionChanging
            RemoveHandler View.CollectionSource.CollectionChanged, AddressOf CollectionSource_CollectionChanged
            RemoveHandler View.CollectionSource.CollectionReloaded, AddressOf CollectionSource_CollectionReloaded
            UnsubscribeFromListChanged()
            MyBase.OnDeactivated()
        End Sub
        Private Sub UnsubscribeFromListChanged()
            Dim bindingList = GetBindingList(View.CollectionSource.Collection)
            If TypeOf bindingList Is IBindingList Then
                RemoveHandler bindingList.ListChanged, AddressOf CollectionSourceBindingList_ListChanged
            End If
        End Sub
        Private Shared Function GetBindingList(ByVal collection As Object) As IBindingList
            If TypeOf collection Is IBindingList Then
                Return DirectCast(collection, IBindingList)
            ElseIf TypeOf collection Is IListSource Then
                Dim listSource = DirectCast(collection, IListSource)
                Return TryCast(listSource.GetList(), IBindingList)
            End If
            Return Nothing
        End Function
    End Class
End Namespace