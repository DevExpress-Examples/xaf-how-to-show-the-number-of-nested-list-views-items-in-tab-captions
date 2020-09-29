using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using ListViewCountInTabs.Module.BusinessObjects;
using ListViewCountInTabs.Module.Helpers;

namespace ListViewCountInTabs.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EmployeeDetailViewWinController : ViewController<DetailView>
    {
        public EmployeeDetailViewWinController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.DelayedItemsInitialization = false;
            ((WinLayoutManager)View.LayoutManager).ItemCreated += DetailViewTabCountController_ItemCreated;
        }

        private void DetailViewTabCountController_ItemCreated(object sender, ItemCreatedEventArgs e)
        {
            if (e.Item is LayoutGroup layoutGroup && e.ModelLayoutElement.Parent is IModelTabbedGroup)
            {
                foreach (IModelLayoutItem item in ((IModelLayoutGroup)e.ModelLayoutElement))
                {
                    IModelLayoutViewItem layoutViewItem = item as IModelLayoutViewItem;
                    if (layoutViewItem == null) continue;
                    ListPropertyEditor propertyEditor = View.FindItem(layoutViewItem.ViewItem.Id) as ListPropertyEditor;
                    if (propertyEditor != null)
                    {
                        propertyEditor.Frame.GetController<NestedListViewTabCountController>().Initialize(layoutGroup);
                    }
                }
            }
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            ((WinLayoutManager)View.LayoutManager).ItemCreated -= DetailViewTabCountController_ItemCreated;
            base.OnDeactivated();
        }
    }

    public class NestedListViewTabCountController : ViewController<ListView>
    {
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            Active["Context is NestedFrame"] = Frame.Context == TemplateContext.NestedFrame;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            UpdateLayoutGroupText();
            View.CollectionSource.CollectionReloaded += CollectionSource_CollectionReloaded;
            View.CollectionSource.CollectionChanged += CollectionSource_CollectionChanged;
            SubscribeToListChanged();
        }
        private LayoutGroup layoutGroup;
        public void Initialize(LayoutGroup layoutGroup)
        {
            this.layoutGroup = layoutGroup;
            UpdateLayoutGroupText();
        }
        private void UpdateLayoutGroupText()
        {
            if (layoutGroup != null)
            {
                int count = View.CollectionSource.GetCount();
                layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text);
                if (count > 0)
                {
                    layoutGroup.Text += " (" + count + ")";
                }
            }
        }
        private void CollectionSource_CollectionReloaded(object sender, EventArgs e)
        {
            UpdateLayoutGroupText();
        }
        private void CollectionSource_CollectionChanged(object sender, EventArgs e)
        {
            UpdateLayoutGroupText();
            SubscribeToListChanged();
        }
        private void CollectionSourceBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateLayoutGroupText();
        }
        private void SubscribeToListChanged()
        {
            if (View.CollectionSource.Collection is IBindingList)
            {
                ((IBindingList)View.CollectionSource.Collection).ListChanged += CollectionSourceBindingList_ListChanged;
            }
            else if (View.CollectionSource.Collection is IListSource)
            {
                IBindingList innerList = ((IListSource)View.CollectionSource.Collection).GetList() as IBindingList;
                if (innerList != null)
                {
                    ((IBindingList)innerList).ListChanged += CollectionSourceBindingList_ListChanged;
                }
            }
        }
        private void UnsubscribeFromListChanged()
        {
            if (View.CollectionSource.Collection is IBindingList)
            {
                ((IBindingList)View.CollectionSource.Collection).ListChanged -= CollectionSourceBindingList_ListChanged;
            }
            else if (View.CollectionSource.Collection is IListSource)
            {
                IBindingList innerList = ((IListSource)View.CollectionSource.Collection).GetList() as IBindingList;
                if (innerList != null)
                {
                    ((IBindingList)innerList).ListChanged -= CollectionSourceBindingList_ListChanged;
                }
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.CollectionSource.CollectionReloaded -= CollectionSource_CollectionReloaded;
            View.CollectionSource.CollectionChanged -= CollectionSource_CollectionChanged;
            UnsubscribeFromListChanged();
        }
    }
}
