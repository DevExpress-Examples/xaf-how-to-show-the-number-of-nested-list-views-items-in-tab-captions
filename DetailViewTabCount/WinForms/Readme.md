# XAF - How to show the number of a nested ListView's items in tab captions

In this example, we demonstrate how to show the number of nested ListViews' items in tab captions in WinForms applications. This is done in the platfrom-dependent controller: [EmployeeDetailViewWinController.cs](./CS/DetailViewTabCount.Module.Win/Controllers/EmployeeDetailViewWinController.cs). Follow the steps listed below to accomplish this task:

1. Create a controller in the WinForms module project.
2. In the **Properties** window for the controller, set the **TargetViewType** property to the **DetailView** and the **TargetObjectType** to your business object type. As a result, the controller will only be activated in detail forms for your business objects.
3. In the **OnActivated** method, set the [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls and obtain the number of ListView items immediately when the Detail View is created.
4. In the same method, handle the **WinLayoutManager.ItemCreated** event.
5. In the **WinLayoutManager.ItemCreated** event handler, loop through the layout items and get a nested List View's controller.
6. Update tab captions in CollectionsSource's [CollectionReloaded](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionReloaded), [CollectionChanged](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.CollectionChanged) and in the [XPBaseCollection.ListChanged](https://docs.devexpress.com/XPO/DevExpress.Xpo.XPBaseCollection.ListChanged) event handlers. 
