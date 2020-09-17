using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.Layout;
using DevExpress.Web;
using ListViewCountInTabs.Module.Helpers;

namespace ListViewCountInTabs.Module.Web.Controllers
{
    public partial class EmployeeDetailViewWebController : ViewController<DetailView>
    {
        private ASPxPageControl pageControl;
        Dictionary<string, string> gridsInTabs;
        public EmployeeDetailViewWebController()
        {
            InitializeComponent();
            gridsInTabs = new Dictionary<string, string>();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
            ((WebLayoutManager)View.LayoutManager).PageControlCreated += EmployeeDetailViewWebController_PageControlCreated;
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
                    if (!gridsInTabs.ContainsKey(gridListEditor.Grid.ID)) {
                        gridsInTabs.Add(gridListEditor.Grid.ID, gridListEditor.Name);
                    }
                    gridListEditor.Grid.DataBound += Grid_DataBound;
                }
            }
        }

        private void Grid_DataBound(object sender, EventArgs e)
        {
            var grid = sender as ASPxGridView;

            if (pageControl != null)
            {
                
                foreach (TabPage tab in pageControl.TabPages)
                {
                    bool isBold = false;
                    var tabCaption = gridsInTabs[grid.ID];
                    var count = ((grid.DataSource as WebDataSource).Collection as ProxyCollection).Count;
                    
                    if (DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text) != tabCaption) continue;

                    tab.Text = DetailViewControllerHelper.ClearItemCountInTabCaption(tab.Text);

                    if (count > 0)
                    {
                        isBold = true;
                        tab.Text += " (" + count + ")";
                        grid.JSProperties["cpCaption"] = tab.Text;
                        grid.ClientSideEvents.EndCallback = $"function(s, e) {{var tab = {pageControl.ClientInstanceName}.GetTabByName('{tab.Name}'); tab.SetText(s.cpCaption); delete s.cpCaption;}}";
                    }
                    tab.TabStyle.Font.Bold = isBold;
                }
            }
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

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            ((WebLayoutManager)View.LayoutManager).PageControlCreated -= EmployeeDetailViewWebController_PageControlCreated;
            pageControl.Dispose();
            base.OnDeactivated();
        }
    }
}
