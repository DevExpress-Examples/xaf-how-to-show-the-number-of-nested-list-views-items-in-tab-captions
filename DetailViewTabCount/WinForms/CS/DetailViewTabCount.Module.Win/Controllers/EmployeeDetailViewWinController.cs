using System;
using System.ComponentModel;
using DetailViewTabCount.Module.BusinessObjects;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.XtraLayout;

namespace DetailViewTabCount.Module.Win.Controllers {
    public class EmployeeDetailViewWinController : ObjectViewController<DetailView, Employee> {
        protected override void OnActivated() {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
            ((WinLayoutManager)View.LayoutManager).ItemCreated += DetailViewTabCountController_ItemCreated;
        }
        private void DetailViewTabCountController_ItemCreated(object sender, ItemCreatedEventArgs e) {
            if(e.Item is LayoutGroup layoutGroup && e.ModelLayoutElement.Parent is IModelTabbedGroup) {
                foreach(var item in (IModelLayoutGroup)e.ModelLayoutElement) {
                    if(item is IModelLayoutViewItem layoutViewItem && View.FindItem(layoutViewItem.ViewItem.Id) is ListPropertyEditor propertyEditor) {
                        propertyEditor.Frame.GetController<NestedListViewTabCountController>().Initialize(layoutGroup);
                    }
                }
            }
        }
        protected override void OnDeactivated() {
            ((WinLayoutManager)View.LayoutManager).ItemCreated -= DetailViewTabCountController_ItemCreated;
            base.OnDeactivated();
        }
    }

    public class NestedListViewTabCountController : ViewController<ListView> {
        private LayoutGroup layoutGroup;
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
        private void SubscribeToListChanged() {
            if(GetBindingList(View.CollectionSource.Collection) is IBindingList bindingList) {
                bindingList.ListChanged += CollectionSourceBindingList_ListChanged;
            }
        }
        public void Initialize(LayoutGroup layoutGroup) {
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
            if(layoutGroup != null) {
                int count = View.CollectionSource.GetCount();
                layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text);
                if(count > 0) {
                    layoutGroup.Text = DetailViewControllerHelper.AddItemCountToTabCaption(layoutGroup.Text, count);
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
            if(GetBindingList(View.CollectionSource.Collection) is IBindingList bindingList) {
                bindingList.ListChanged -= CollectionSourceBindingList_ListChanged;
            }
        }
        private static IBindingList GetBindingList(object collection) {
            switch(collection) {
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
