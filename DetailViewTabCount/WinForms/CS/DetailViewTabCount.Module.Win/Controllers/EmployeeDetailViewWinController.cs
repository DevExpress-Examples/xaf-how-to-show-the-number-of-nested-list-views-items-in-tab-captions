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
                foreach(IModelLayoutItem item in ((IModelLayoutGroup)e.ModelLayoutElement)) {
                    IModelLayoutViewItem layoutViewItem = item as IModelLayoutViewItem;
                    if(layoutViewItem == null)
                        continue;
                    ListPropertyEditor propertyEditor = View.FindItem(layoutViewItem.ViewItem.Id) as ListPropertyEditor;
                    if(propertyEditor != null) {
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
        public NestedListViewTabCountController() {
            TargetViewNesting = Nesting.Nested;
        }
        protected override void OnActivated() {
            base.OnActivated();
            UpdateLayoutGroupText();
            View.CollectionSource.CollectionReloaded += CollectionSource_CollectionReloaded;
            View.CollectionSource.CollectionChanged += CollectionSource_CollectionChanged;
            View.CollectionSource.CollectionChanging += CollectionSource_CollectionChanging;
            SubscribeToListChanged();
        }
        private LayoutGroup layoutGroup;
        public void Initialize(LayoutGroup layoutGroup) {
            this.layoutGroup = layoutGroup;
            UpdateLayoutGroupText();
        }
        private void UpdateLayoutGroupText() {
            if(layoutGroup != null) {
                int count = View.CollectionSource.GetCount();
                layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text);
                if(count > 0) {
                    layoutGroup.Text += " (" + count + ")";
                }
            }
        }
        private void CollectionSource_CollectionReloaded(object sender, EventArgs e) {
            UpdateLayoutGroupText();
        }
        private void CollectionSource_CollectionChanged(object sender, EventArgs e) {
            UpdateLayoutGroupText();
            SubscribeToListChanged();
        }
        private void CollectionSource_CollectionChanging(object sender, EventArgs e) {
            UnsubscribeFromListChanged();
        }

        private void CollectionSourceBindingList_ListChanged(object sender, ListChangedEventArgs e) {
            UpdateLayoutGroupText();
        }
        private void SubscribeToListChanged() {
            if(View.CollectionSource.Collection is IBindingList) {
                ((IBindingList)View.CollectionSource.Collection).ListChanged += CollectionSourceBindingList_ListChanged;
            }
            else if(View.CollectionSource.Collection is IListSource) {
                IBindingList innerList = ((IListSource)View.CollectionSource.Collection).GetList() as IBindingList;
                if(innerList != null) {
                    ((IBindingList)innerList).ListChanged += CollectionSourceBindingList_ListChanged;
                }
            }
        }
        private void UnsubscribeFromListChanged() {
            if(View.CollectionSource.Collection is IBindingList) {
                ((IBindingList)View.CollectionSource.Collection).ListChanged -= CollectionSourceBindingList_ListChanged;
            }
            else if(View.CollectionSource.Collection is IListSource) {
                IBindingList innerList = ((IListSource)View.CollectionSource.Collection).GetList() as IBindingList;
                if(innerList != null) {
                    ((IBindingList)innerList).ListChanged -= CollectionSourceBindingList_ListChanged;
                }
            }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            View.CollectionSource.CollectionReloaded -= CollectionSource_CollectionReloaded;
            View.CollectionSource.CollectionChanged -= CollectionSource_CollectionChanged;
            View.CollectionSource.CollectionChanging -= CollectionSource_CollectionChanging;
            UnsubscribeFromListChanged();
        }
    }
}
