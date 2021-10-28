<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/307963996/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T943913)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# XAF - How to show the number of nested List View items in tab captions

## Scenario
In this example, we demonstrate how to show the number of nested List View items in tab captions. You can add, delete, link or unlink items in nested List Views, or move between different records in the Detail View. The record count in tab caption will be automatically updated (for the Tasks detail collection of the Employee class).

### WinForms
<img src="./media/example-win.png" width="600">


### ASP.NET Web Forms & Blazor

<img src="./media/example-web.png" width="600">

## Solution
For implementation details and platform specificities, refer to the following links:
* [Cross-platform Module](./DetailViewTabCount/Module) | [DetailViewControllerHelper.cs](./DetailViewTabCount/Module/CS/DetailViewTabCount.Module/Helpers/DetailViewControllerHelper.cs) | [Employee.cs](./DetailViewTabCount/Module/CS/DetailViewTabCount.Module/BusinessObjects/Employee.cs)
* [WinForms](./DetailViewTabCount/WinForms) | [EmployeeDetailViewWinController.cs](./DetailViewTabCount/WinForms/CS/DetailViewTabCount.Module.Win/Controllers/EmployeeDetailViewWinController.cs)
* [ASP.NET Web Forms](./DetailViewTabCount/ASP.NET/WebForms) | [EmployeeDetailViewWebController.cs](./DetailViewTabCount/ASP.NET/WebForms/CS/DetailViewTabCount.Module.Web/Controllers/EmployeeDetailViewWebController.cs)
* [ASP.NET Core Blazor](./DetailViewTabCount/ASP.NET/Blazor) | [EmployeeDetailViewBlazorController.cs](./DetailViewTabCount/ASP.NET/Blazor/DetailViewTabCount.Module.Blazor/Controllers/EmployeeDetailViewBlazorController.cs)

***See also:***  
[XAF - How to show the number of List View items in the Navigation Control](https://github.com/DevExpress-Examples/XAF-How-to-show-the-number-of-list-view-items-in-the-navigation-control)
