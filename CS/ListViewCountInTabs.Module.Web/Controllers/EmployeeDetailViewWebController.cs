using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.Layout;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Web;
using DevExpress.Xpo;
using ListViewCountInTabs.Module.Helpers;

namespace ListViewCountInTabs.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EmployeeDetailViewWebController : ViewController<DetailView>
    {
        private ASPxPageControl pageControl;
        private RecordsNavigationController recordsNavigationController;
        private RefreshController refreshController;
        private ASPxGridView tasksGrid;
        private ASPxGridView phonesGrid;
        public EmployeeDetailViewWebController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.DelayedItemsInitialization = false;
            ((WebLayoutManager)View.LayoutManager).PageControlCreated += EmployeeDetailViewWebController_PageControlCreated;
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            recordsNavigationController = Frame.GetController<RecordsNavigationController>();
            recordsNavigationController.NextObjectAction.Executed += RibbonAction_Executed;
            recordsNavigationController.PreviousObjectAction.Executed += RibbonAction_Executed;
            refreshController = Frame.GetController<RefreshController>();
            refreshController.RefreshAction.Executed += RibbonAction_Executed;
            View.CustomizeViewItemControl<ListPropertyEditor>(this, (editor) => {
                editor.ListView.ControlsCreated += ListView_ControlsCreated;
            });
        }

        private void ListView_ControlsCreated(object sender, EventArgs e)
        {
            ASPxGridListEditor gridListEditor = ((ListView)sender).Editor as ASPxGridListEditor;
            if (gridListEditor != null)
            {
                if (gridListEditor.Grid != null)
                {
                    if (gridListEditor.Name == "Tasks")
                    {
                        tasksGrid = gridListEditor.Grid;
                    }
                    else if (gridListEditor.Name == "Phone Numbers")
                    {
                        phonesGrid = gridListEditor.Grid;
                    }
                }
            }
        }

        private void RibbonAction_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
        {
            UpdatePageControl();
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            UpdatePageControl();
        }

        private void EmployeeDetailViewWebController_PageControlCreated(object sender, PageControlCreatedEventArgs e)
        {
            // Check this Id in the AppName.Module/Model.DesignedDiffs.xafml file
            if (e.Model.Id == "Tabs")
            {
                pageControl = e.PageControl;
                //((WebLayoutManager)View.LayoutManager).PageControlCreated -= EmployeeDetailViewWebController_PageControlCreated;
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            UpdatePageControl();
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            ((WebLayoutManager)View.LayoutManager).PageControlCreated -= EmployeeDetailViewWebController_PageControlCreated;
            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            recordsNavigationController.NextObjectAction.Executed -= RibbonAction_Executed;
            recordsNavigationController.PreviousObjectAction.Executed -= RibbonAction_Executed;
            refreshController.RefreshAction.Executed -= RibbonAction_Executed;
            base.OnDeactivated();
        }

        private void UpdatePageControl()
        {
            if (pageControl != null)
            {
                foreach (TabPage tab in pageControl.TabPages)
                {
                    var listEditor = View.FindItem(tab.Name) as ListPropertyEditor;
                    if (listEditor == null) continue;

                    object currentValue = listEditor.MemberInfo.GetValue(listEditor.CurrentObject);
                    if (currentValue == null) continue;

                    bool isBold = false;
                    ICollection collection = currentValue as ICollection;
                    if (collection != null)
                    {
                        int count = 0;

                        if (tasksGrid == null || phonesGrid == null)
                        {
                            XPBaseCollection xpCollection = currentValue as XPBaseCollection;
                            if ((xpCollection != null) && listEditor.MemberInfo.IsAssociation
                                && !listEditor.MemberInfo.IsManyToMany)
                            {
                                if (xpCollection.IsLoaded)
                                {
                                    count = xpCollection.Count;
                                }
                                else
                                {
                                    count = View.ObjectSpace.GetObjectsCount(listEditor.MemberInfo.ListElementType,
                                    CriteriaOperator.Parse($"[{listEditor.MemberInfo.AssociatedMemberInfo.Name}] = ?",
                                    View.CurrentObject));
                                }
                            }
                        }
                        else if (listEditor.Id == "Tasks")
                        {
                            count = tasksGrid.VisibleRowCount;
                        }
                        else if (listEditor.Id == "PhoneNumbers")
                        {
                            count = phonesGrid.VisibleRowCount;
                        }

                        tab.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text);

                        if (count > 0)
                        {
                            isBold = true;
                            tab.Text += " (" + count + ")";
                        }
                    }
                    tab.TabStyle.Font.Bold = isBold;
                }
            }
        }
    }
}
