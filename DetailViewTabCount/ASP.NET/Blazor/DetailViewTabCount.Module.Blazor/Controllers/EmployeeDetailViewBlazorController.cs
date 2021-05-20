using System.Collections;
using DetailViewTabCount.Module.BusinessObjects;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;

namespace DetailViewTabCount.Module.Blazor.Controllers {
    public class EmployeeDetailViewBlazorController : ObjectViewController<DetailView, Employee> {
        protected override void OnActivated() {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            RefreshTabCaptions();
        }
        private void RefreshTabCaptions() {
            foreach(var item in View.GetItems<ListPropertyEditor>()) {
                item.Caption = DetailViewControllerHelper.ClearItemCountInTabCaption(item.Caption);
                int count = item.ListView.CollectionSource.GetCount();
                if(count > 0) {
                    item.Caption = DetailViewControllerHelper.AddItemCountToTabCaption(item.Caption, count);
                }
            }
        }
    }
}
