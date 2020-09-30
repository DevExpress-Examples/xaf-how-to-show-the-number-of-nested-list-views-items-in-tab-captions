Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace DetailViewTabCountVB.Module.Win
    <ToolboxItemFilter("Xaf.Platform.Win")>
    Partial Public NotInheritable Class DetailViewTabCountWindowsFormsModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Return ModuleUpdater.EmptyModuleUpdaters
        End Function
        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
        End Sub
    End Class
End Namespace
