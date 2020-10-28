# XAF for Blazor - How to show the number of nested List Views' items in tab captions

>At the moment, tab captions are not updated immediately after the List View item count is modified.

In this example, we demonstrate how to show the number of nested List Views' items in tab captions. We use the following controller: [EmployeeDetailViewController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs). Follow the steps listed below to accomplish this task:
1. Create a controller in the Blazor module project.
2. Set the **TargetViewType** property to **DetailView** and **TargetObjectType** to your business object type in the controller. As a result, the controller will only be activated in detail forms for your business objects.
3. In the **OnActivated** method, set [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls immediately when your Detail View is created. This will allow you to obtain the number of List View items.
8. Loop through [View Items](https://docs.devexpress.com/eXpressAppFramework/112612/concepts/ui-construction/view-items) in the **CompositeView.ViewControlsCreated** event handler to access *BlazorListPropertyEditors*. Then, use the **BlazorListPropertyEditor.MemberInfo.GetValue** method to get the collection of the editor's items and evaluate their number. Finally, update tab captions in your editors accordingly. For more details, review the implementation of the following method in this example: [RefreshTabCaptions](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs#L27).

<!-- default file list -->
*Files to look at*:

* [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)
<!-- default file list end -->
