# XAF for Blazor - How to show the number of nested List View items in tab captions

In this example, we demonstrate how to show the number of nested List View items in tab captions in Blazor applications. We use a platform-dependent controller: EmployeeDetailViewBlazorController.cs. Follow the steps listed below to accomplish this task:

Create a controller for Nested List Views in the Blazor module project. To activate it only for nested List Views, set the ViewController.TargetViewNesting property to "Nesting.Nested".
In the OnActivated method, handle CollectionsSource's CollectionReloaded, CollectionChanged and XPBaseCollection.ListChanged events. In these event handlers, update tab captions using the DxFormLayoutTabPageModel.Caption property.
Create an Initialize method with the DxFormLayoutTabPageModel parameter. In this method, update a tab caption for a particular tab.
Create a controller for Detail Views in your Blazor module project.
To activate this controller only in a particular Detail View, specify its TargetViewId property.
In the OnActivated method, set CompositeView.DelayedItemsInitialization to false to initialize all View Item controls immediately when your Detail View is created. This is required to access a nested List View when its tab page is created.
In the same method, handle the BlazorLayoutManager.ItemCreated event.
In the BlazorLayoutManager.ItemCreated event handler, loop through tabbed groups. Then, find ListPropertyEditors using the CompositeView.FindItem method and get controller of nested List Views using the Frame.GetController method. Then, call the Initialize method created in step 3.

<!-- default file list -->
*Files to look at*:

* [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)
<!-- default file list end -->
