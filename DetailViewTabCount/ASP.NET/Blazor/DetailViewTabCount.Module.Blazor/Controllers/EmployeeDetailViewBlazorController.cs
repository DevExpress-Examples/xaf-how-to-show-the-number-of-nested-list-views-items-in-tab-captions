using DetailViewTabCount.Module.BusinessObjects;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Layout;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using System;
using System.ComponentModel;

namespace DetailViewTabCount.Module.Blazor.Controllers {
    public class EmployeeDetailViewBlazorController : ObjectViewController<DetailView, Employee> {
        protected override void OnActivated() {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
            ((BlazorLayoutManager)View.LayoutManager).ItemCreated += DxFormLayoutItemController_ItemCreated;
        }

        private void DxFormLayoutItemController_ItemCreated(object sender, BlazorLayoutManager.ItemCreatedEventArgs e) {
            if (e.LayoutControlItem is DxFormLayoutTabPageModel layoutGroup && e.ModelLayoutElement.Parent is IModelTabbedGroup) {
                foreach (var item in (IModelLayoutGroup)e.ModelLayoutElement) {
                    if (item is IModelLayoutViewItem layoutViewItem && View.FindItem(layoutViewItem.ViewItem.Id) is ListPropertyEditor propertyEditor) {
                        propertyEditor.Frame.GetController<NestedListViewTabCountController>().Initialize(layoutGroup);
                        propertyEditor.ValueRead += (s, e) => { propertyEditor.Frame.GetController<NestedListViewTabCountController>().SubscribeToListChanged(); };
                    }
                }
            }
        }
        protected override void OnDeactivated() {
            ((BlazorLayoutManager)View.LayoutManager).ItemCreated -= DxFormLayoutItemController_ItemCreated;
            base.OnDeactivated();
        }
    }
    public class NestedListViewTabCountController : ViewController<ListView> {
        private DxFormLayoutTabPageModel layoutGroup;
        public NestedListViewTabCountController() {
            TargetViewNesting = Nesting.Nested;
        }
        protected override void OnActivated() {
            base.OnActivated();
            UpdateLayoutGroupText();
            View.CollectionSource.CollectionChanging += CollectionSource_CollectionChanging;
            View.CollectionSource.CollectionChanged += CollectionSource_CollectionChanged;
            View.CollectionSource.CollectionReloaded += CollectionSource_CollectionReloaded;
            SubscribeToListChanged();
        }

        internal void SubscribeToListChanged() {
            if (GetBindingList(View.CollectionSource.Collection) is IBindingList bindingList) {
                bindingList.ListChanged += CollectionSourceBindingList_ListChanged;
            }
        }
        public void Initialize(DxFormLayoutTabPageModel layoutGroup) {
            this.layoutGroup = layoutGroup;
            UpdateLayoutGroupText();
        }
        private void CollectionSource_CollectionChanging(object sender, EventArgs e) {
            UnsubscribeFromListChanged();
        }
        private void CollectionSource_CollectionChanged(object sender, EventArgs e) {
            UpdateLayoutGroupText();
            SubscribeToListChanged();
        }
        private void CollectionSource_CollectionReloaded(object sender, EventArgs e) {
            UpdateLayoutGroupText();
        }
        private void CollectionSourceBindingList_ListChanged(object sender, ListChangedEventArgs e) {
            UpdateLayoutGroupText();
        }
        private void UpdateLayoutGroupText() {
            if (layoutGroup != null) {
                int count = View.CollectionSource.GetCount();
                layoutGroup.Caption = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Caption);
                if (count > 0) {
                    layoutGroup.Caption = DetailViewControllerHelper.AddItemCountToTabCaption(layoutGroup.Caption, count);
                }
            }
        }
        protected override void OnDeactivated() {
            View.CollectionSource.CollectionChanging -= CollectionSource_CollectionChanging;
            View.CollectionSource.CollectionChanged -= CollectionSource_CollectionChanged;
            View.CollectionSource.CollectionReloaded -= CollectionSource_CollectionReloaded;
            UnsubscribeFromListChanged();
            base.OnDeactivated();
        }
        private void UnsubscribeFromListChanged() {
            if (GetBindingList(View.CollectionSource.Collection) is IBindingList bindingList) {
                bindingList.ListChanged -= CollectionSourceBindingList_ListChanged;
            }
        }
        private static IBindingList GetBindingList(object collection) {
            switch (collection) {
                case IBindingList bindingList:
                    return bindingList;
                case IListSource listSource:
                    return listSource.GetList() as IBindingList;
                default:
                    return null;
            }
        }
    }
}
