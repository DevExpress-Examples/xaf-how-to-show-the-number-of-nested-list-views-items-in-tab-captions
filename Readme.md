# XAF - How to show the number of nested List View items in tab captions

## Scenario
In this example, we demonstrate how to show the number of nested List View items in tab captions. You can add, delete, link or unlink items in nested List Views, or move between different records in the Detail View. The record count in tab caption will be automatically updated (for the Tasks detail collection of the Employee class).

### WinForms

<img src="./media/example-win.png" width="600">

### ASP.NET Web Forms & Blazor

<img src="./media/example-web.png" width="600">

## Solution
For implementation details, refer to the following links:
* [Cross-platform Module](./DetailViewTabCount/Module) | [DetailViewControllerHelper.cs](./DetailViewTabCount/Module/CS/DetailViewTabCount.Module/Helpers/DetailViewControllerHelper.cs) | [Employee.cs](./DetailViewTabCount/Module/CS/DetailViewTabCount.Module/BusinessObjects/Employee.cs)
* [WinForms](./DetailViewTabCount/WinForms) | [EmployeeDetailViewWinController.cs](./DetailViewTabCount/WinForms/CS/DetailViewTabCount.Module.Win/Controllers/EmployeeDetailViewWinController.cs)
* [ASP.NET Web Forms](./DetailViewTabCount/ASP.NET/WebForms) | [EmployeeDetailViewWebController.cs](./DetailViewTabCount/ASP.NET/WebForms/CS/DetailViewTabCount.Module.Web/Controllers/EmployeeDetailViewWebController.cs)
* [ASP.NET Core Blazor](./DetailViewTabCount/ASP.NET/Blazor) | [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount/ASP.NET/Blazor/DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)
