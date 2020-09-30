using System.Collections;
using DetailViewTabCount.Module.BusinessObjects;
using DetailViewTabCount.Module.Helpers;
using DevExpress.ExpressApp;

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
            foreach (var item in View.Items)
            {
                var listPropertyEditor = item as DevExpress.ExpressApp.Blazor.Editors.BlazorListPropertyEditor;
                if (listPropertyEditor != null)
                {
                    var collection = listPropertyEditor.MemberInfo.GetValue(item.CurrentObject);
                    if (collection != null && collection is ICollection)
                    {
                        listPropertyEditor.Caption = DetailViewControllerHelper.ClearItemCountInTabCaption(listPropertyEditor.Caption);
                        int count = (collection as ICollection).Count;

                        if (count > 0)
                        {
                            listPropertyEditor.Caption = $"{item.Caption} ({count})";
                        }
                    }
                }
            }
        }
    }
}
