# XAF - How to show the number of nested ListView items in tab captions

In this example, we demonstrate how to show the number of nested List View items in tab captions in ASP.NET Web Forms applications. We use a platform-dependent controller: [EmployeeDetailViewWebController.cs](./CS/DetailViewTabCount.Module.Web/Controllers/EmployeeDetailViewWebController.cs). Follow the steps listed below to accomplish this task:
1. Create a controller in the WebForms module project.
2. To activate this controller only in particular Detail Views, specify its **TargetViewId** property.
3. In the **OnActivated** method, set [CompositeView.DelayedItemsInitialization](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CompositeView.DelayedItemsInitialization) to **false** to initialize all View Item controls immediately when your Detail View is created. This is required to access a nested List View when its tab page is created.
4. In the **OnActivated** method, subscribe to the [View.CurrentObjectChanged](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.View.CurrentObjectChanged), **WebLayoutManager.PageControlCreated**, and [WebWindow.PagePreRender](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebWindow.PagePreRender) events.
5. In the **PageControlCreated** event handler, access the **ASPxPageControl** instance that shows List View tabs and save it to a private field. Refer to the following help topic for additional information: [How to access a tab control in a Detail View layout](https://github.com/DevExpress-Examples/XAF_how-to-access-a-tab-control-in-a-detail-view-layout-e372).
6. In the **WebWindow.CurrentRequestWindow.PagePreRender** and **View.CurrentObjectChanged** event handlers, iterate through ASPxPageControl tabs and update their captions based on the number of items returned by the [CollectionSourceBase.GetCount](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.CollectionSourceBase.GetCount) method.  

<!-- default file list --> 
*Files to look at*:

* [EmployeeDetailViewWebController.cs](./CS/DetailViewTabCount.Module.Web/Controllers/EmployeeDetailViewWebController.cs) (VB: [EmployeeDetailViewWebController.vb](./VB/DetailViewTabCountVB.Module.Web/Controllers/EmployeeDetailViewWebController.vb))
<!-- default file list end -->
