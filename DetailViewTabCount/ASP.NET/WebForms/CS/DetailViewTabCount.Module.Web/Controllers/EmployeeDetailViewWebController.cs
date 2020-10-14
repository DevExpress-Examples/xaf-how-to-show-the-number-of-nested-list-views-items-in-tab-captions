using System;
using System.Collections.Generic;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.Layout;
using DevExpress.ExpressApp.Web.Utils;
using DevExpress.Web;

namespace DetailViewTabCount.Module.Web.Controllers
{
    public partial class EmployeeDetailViewWebController : ViewController<DetailView>
    {
        private ASPxPageControl pageControl;

        public EmployeeDetailViewWebController()
        {
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
            View.CurrentObjectChanged += View_CurrentObjectChanged;
            ((WebLayoutManager)View.LayoutManager).PageControlCreated += EmployeeDetailViewWebController_PageControlCreated;
            WebWindow.CurrentRequestWindow.PagePreRender += CurrentRequestWindow_PagePreRender;
        }

        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            if (pageControl != null) UpdatePageControl(pageControl);
        }

        protected override void OnDeactivated()
        {
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
            ((WebLayoutManager)View.LayoutManager).PageControlCreated -= EmployeeDetailViewWebController_PageControlCreated;
            WebWindow.CurrentRequestWindow.PagePreRender -= CurrentRequestWindow_PagePreRender;
            base.OnDeactivated();
        }
        protected override void OnViewControlsDestroying()
        {
            pageControl = null;
            base.OnViewControlsDestroying();
        }
        private void EmployeeDetailViewWebController_PageControlCreated(object sender, PageControlCreatedEventArgs e)
        {
            // Check this Id in the AppName.Module/Model.DesignedDiffs.xafml file
            if (e.Model.Id == "Tabs")
            {
                pageControl = e.PageControl;
                pageControl.ClientInstanceName = "pageControl";
            }
        }
        private void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            if (pageControl != null)
            {
                UpdatePageControl(pageControl);
            }
        }
        private void UpdatePageControl(ASPxPageControl pageControl)
        {
            foreach (TabPage tab in pageControl.TabPages)
            {
                tab.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text);
                var listPropertyEditor = View.FindItem(tab.Name) as ListPropertyEditor;
                if (listPropertyEditor != null)
                {
                    var count = listPropertyEditor.ListView.CollectionSource.GetCount();
                    if (count > 0)
                    {
                        tab.Text += " (" + count + ")";
                    }
                    if (listPropertyEditor.ListView.Editor is ASPxGridListEditor editor && editor.Grid != null)
                    {
                        editor.Grid.JSProperties["cpCaption"] = tab.Text;
                        ClientSideEventsHelper.AssignClientHandlerSafe(editor.Grid, "EndCallback", $"function(s, e) {{ " +
                            $"if (!s.cpCaption) return;" +
                            $"var tab = {pageControl.ClientInstanceName}.GetTabByName('{tab.Name}');" +
                            $"tab.SetText(s.cpCaption);" +
                            $"delete s.cpCaption}}", nameof(EmployeeDetailViewWebController));
                    }
                }
            }
        }
    }
}
