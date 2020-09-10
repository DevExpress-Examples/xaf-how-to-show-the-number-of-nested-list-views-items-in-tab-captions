using System;
using System.Collections;
using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraLayout;
using ListViewCountInTabs.Module.Helpers;

namespace ListViewCountInTabs.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EmployeeDetailViewWinController : ViewController<DetailView>
    {
        private LayoutControl layoutControl;
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
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            RefreshLayoutControl();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            RefreshLayoutControl();

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            base.OnDeactivated();
        }

        private void RefreshLayoutControl()
        {
            layoutControl = (LayoutControl)((DetailView)View).LayoutManager.Container;
            UpdateTabs(layoutControl.Root, Application, (DetailView)View);
        }

        public void UpdateTabs(LayoutItemContainer group, XafApplication application, DetailView view)
        {
            group.BeginUpdate();
            LayoutGroup layoutGroup = group as LayoutGroup;
            if ((layoutGroup != null) && layoutGroup.IsInTabbedGroup)
            {
                bool isBold = false;
                foreach (BaseLayoutItem item in layoutGroup.Items)
                {
                    LayoutControlItem layoutControlItem = item as LayoutControlItem;
                    if (layoutControlItem == null) continue;
                    
                    PropertyEditor propertyEditor = FindPropertyEditor(view, layoutControlItem.Control);
                    if (propertyEditor == null) continue;
                    
                    object currentValue = propertyEditor.MemberInfo.GetValue(propertyEditor.CurrentObject);
                    if (currentValue == null) continue;
                    
                    ICollection collection = currentValue as ICollection;
                    if (collection != null)
                    {
                        int count = collection.Count;
                        
                        layoutGroup.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(layoutGroup.Text);

                        if (count > 0)
                        {
                            isBold = true;
                            layoutGroup.Text += " (" + count + ")";
                        }
                    }
                }
                HighlightTabs(layoutGroup, isBold);
            }
            IEnumerable items;
            if (group is TabbedGroup)
            {
                items = ((TabbedGroup)group).TabPages;
            }
            else
            {
                items = ((LayoutGroup)group).Items;
            }
            foreach (BaseLayoutItem item in items)
            {
                if (item is LayoutItemContainer)
                {
                    UpdateTabs((LayoutItemContainer)item, application, view);
                }
            }
            group.EndUpdate();
        }

        private PropertyEditor FindPropertyEditor(DetailView view, System.Windows.Forms.Control control)
        {
            foreach (PropertyEditor pe in view.GetItems<PropertyEditor>())
            {
                if (Equals(control, pe.Control)) return pe;
            }
            return null;
        }

        private void HighlightTabs(LayoutGroup layoutGroup, bool isBold)
        {
            Font font = layoutGroup.AppearanceTabPage.Header.Font;
            if (isBold)
            {
                layoutGroup.AppearanceTabPage.Header.Font = new Font(font, FontStyle.Bold);
                layoutGroup.AppearanceTabPage.HeaderHotTracked.Font = new Font(font, FontStyle.Bold);
            }
            else
            {
                layoutGroup.AppearanceTabPage.Header.Font = new Font(font, FontStyle.Regular);
                layoutGroup.AppearanceTabPage.HeaderHotTracked.Font = new Font(font, FontStyle.Regular);
            }
        }
    }
}
