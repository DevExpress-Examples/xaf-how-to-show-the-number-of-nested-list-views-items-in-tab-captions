# XAF for Blazor - How to show the number of nested List View items in tab captions

In this example, we demonstrate how to show the number of nested List View items in tab captions in Blazor applications. We use a platform-dependent controller: [EmployeeDetailViewBlazorController.cs](DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs). Follow the steps listed below to accomplish this task:

1. Create a controller for Nested List Views in the Blazor module project. To activate it only for nested List Views, set the [ViewController.TargetViewNesting](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.TargetViewNesting) property to "Nesting.Nested".
2. In the **OnActivated** method, handle CollectionsSource's [CollectionReloaded](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionReloaded), [CollectionChanged](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionChanged) and [XPBaseCollection.ListChanged](https://docs.devexpress.com/XPO/DevExpress.Xpo.XPBaseCollection.ListChanged) events. In these event handlers, update tab captions using the [DxFormLayoutTabPage.Caption](https://docs.devexpress.com/Blazor/DevExpress.Blazor.Base.FormLayoutItemBase.Caption) property.
3. Create an **Initialize** method with the **DxFormLayoutTabPageModel** parameter. In this method, update a tab caption for a particular tab.
4. Create a controller for Detail Views in your Blazor module project.
5. To activate this controller only in a particular Detail View, specify its **TargetViewId** property.
6. In the **OnActivated** method, set [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls immediately when your Detail View is created. This is required to access a nested List View when its tab page is created.
7. In the same method, handle the **BlazorLayoutManager.ItemCreated** event.
8. In the **BlazorLayoutManager.ItemCreated** event handler, loop through tabbed groups. Then, find **ListPropertyEditor**s using the [CompositeView.FindItem](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.FindItem(System.String)) method and get controller of nested List Views using the [Frame.GetController](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Frame.GetController--1) method. Then, call the **Initialize** method created in step 3.

<!-- default file list -->
*Files to look at*:

* [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)
<!-- default file list end -->
