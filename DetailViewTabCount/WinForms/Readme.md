# XAF - How to show the number of nested List Views' items in tab captions

In this example, we demonstrate how to show the number of nested List Views' items in tab captions in WinForms applications. This is done in the platfrom-dependent controller: [EmployeeDetailViewWinController.cs](./CS/DetailViewTabCount.Module.Win/Controllers/EmployeeDetailViewWinController.cs). Follow the steps listed below to accomplish this task:

1. Create a controller for Nested List Views in the WinForms module project. To activate it only for nested List Views, set the [ViewController.TargetViewNesting](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.TargetViewNesting) property to "Nesting.Nested".
2. In the **OnActivated** method, handle CollectionsSource's [CollectionReloaded](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionReloaded), [CollectionChanged](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionChanged) and the [XPBaseCollection.ListChanged](https://docs.devexpress.com/XPO/DevExpress.Xpo.XPBaseCollection.ListChanged) events. In these event handlers, update tab captions using the [BaseLayoutItem.Text](https://docs.devexpress.com/WindowsForms/DevExpress.XtraLayout.BaseLayoutItem.Text) property.
3. Create the **Initialize** method with the [LayoutGroup](https://docs.devexpress.com/WindowsForms/DevExpress.XtraLayout.LayoutGroup._members) parameter. In this method, update a tab caption for a particular tab.
4. Create a controller for Detail Views in the WinForms module project.
5. To activate this controller only in a particular Detail View, specify its **TargetViewId** property.
6. In the **OnActivated** method, set the [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls immediately when the Detail View is created. This is required to access a nested List View when its tab page is created.
7. In the same method, handle the **WinLayoutManager.ItemCreated** event.
8. In the **WinLayoutManager.ItemCreated** event handler, loop through the tabbed groups. Then, find **ListPropertyEditor**s using the [CompositeView.FindItem](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.FindItem(System.String)) method and get nested List Views' controllers using the [Frame.GetController](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Frame.GetController--1) method. Then, call the **Initialize** method created in step 3.

<!-- default file list --> 
*Files to look at*:

* [EmployeeDetailViewWinController.cs](./CS/DetailViewTabCount.Module.Win/Controllers/EmployeeDetailViewWinController.cs) (VB: [EmployeeDetailViewWinController.vb](./VB/DetailViewTabCountVB.Module.Win/Controllers/EmployeeDetailViewWinController.vb))
<!-- default file list end -->
