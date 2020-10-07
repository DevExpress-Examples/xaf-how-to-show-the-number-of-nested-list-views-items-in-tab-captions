using System.Collections;
using DetailViewTabCount.Module.BusinessObjects;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;

namespace DetailViewTabCount.Module.Blazor.Controllers
{
    public partial class EmployeeDetailViewBlazorController : ViewController<DetailView>
    {
        public EmployeeDetailViewBlazorController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Employee);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.DelayedItemsInitialization = false;
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            RefreshTabCaptions();
        }

        private void RefreshTabCaptions()
        {
            foreach (var item in View.GetItems<BlazorListPropertyEditor>())
            {
                if (item.MemberInfo.GetValue(item.CurrentObject) is ICollection collection)
                {
                    item.Caption = DetailViewControllerHelper.ClearItemCountInTabCaption(item.Caption);
                    int count = collection.Count;
                    if (count > 0)
                    {
                        item.Caption = $"{item.Caption} ({count})";
                    }
                }
            }
        }
    }
}
