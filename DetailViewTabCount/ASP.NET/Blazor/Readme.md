# XAF for Blazor - How to show the number of nested List View items in tab captions

In this example, we demonstrate how to show the number of nested List View items in tab captions. We use the following controller: [EmployeeDetailViewController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs). Follow the steps listed below to accomplish this task:
1. Create a controller in the Blazor module project.
2. Set the **TargetViewType** property to **DetailView** and **TargetObjectType** to your business object type in the controller. As a result, the controller will only be activated in detail forms for your business objects.
3. In the **OnActivated** method, set [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls immediately when your Detail View is created. This will allow you to obtain the number of List View items.
4. Loop through [View Items](https://docs.devexpress.com/eXpressAppFramework/112612/concepts/ui-construction/view-items) in the **CompositeView.ViewControlsCreated** event handler to access *BlazorListPropertyEditors*. Then, use the **[CollectionSourceBase.GetCount](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.GetCount)** method to get the item count. Finally, update tab captions in your editors accordingly. For more details, review the implementation of the following method in this example: [RefreshTabCaptions](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs#L27).

This solution is intended for tabs that are automatically generated for collection properties. To update captions of tabs created in the Model Editor, use the following solution: <a href="https://supportcenter.devexpress.com/ticket/details/t1084394/xaf-blazor-dashboard-view-count-record-in-nested-listiview">Xaf Blazor - Dashboard View - Count record in nested listiview</a>.

Tab captions are not updated immediately after List View items are changed. Blazor Layout Manager does not have the required API. As a workaround, use the following JavaScript-based solution: <a href="https://supportcenter.devexpress.com/ticket/details/t1050385/blazor-how-to-update-a-layout-item-text-caption-of-a-property-editor-based-on-property">Blazor - How to update a layout item text (Caption of a property editor) based on property value changes</a>

<!-- default file list -->
*Files to look at*:

* [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)
<!-- default file list end -->
