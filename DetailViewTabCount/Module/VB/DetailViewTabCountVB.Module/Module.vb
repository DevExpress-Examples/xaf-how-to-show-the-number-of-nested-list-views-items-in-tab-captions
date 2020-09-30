Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Xpo

Partial Public NotInheritable Class DetailViewTabCountModuleVB
    Inherits ModuleBase

    Public Sub New()
        InitializeComponent()
        BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction
    End Sub
    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        Return New ModuleUpdater() {updater}
    End Function
    Public Overrides Sub Setup(ByVal application As XafApplication)
        MyBase.Setup(application)
    End Sub
    Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
        MyBase.CustomizeTypesInfo(typesInfo)
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
    End Sub
End Class
