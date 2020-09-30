Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Xpo

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
Partial Public Class DetailViewTabCountAspNetApplication
	Inherits WebApplication

	Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
	Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
	Private module3 As DetailViewTabCountVB.Module.DetailViewTabCountModuleVB
	Private module4 As DetailViewTabCountVB.Module.Web.DetailViewTabCountAspNetModule

#Region "Default XAF configuration options (https:" 'www.devexpress.com/kb=T501418)
	Shared Sub New()
		EnableMultipleBrowserTabsSupport = True
		DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = True
		DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3
		DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = True
		DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3
		DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = True
		DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = False
		DevExpress.ExpressApp.BaseObjectSpace.ThrowExceptionForNotRegisteredEntityType = True
	End Sub
	Private Sub InitializeDefaults()
		LinkNewObjectToParentImmediately = False
		OptimizedControllersCreation = True
	End Sub
#End Region
	Public Sub New()
		InitializeComponent()
		InitializeDefaults()
	End Sub
	Protected Overrides Function CreateViewUrlManager() As IViewUrlManager
		Return New ViewUrlManager()
	End Function
	Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
		args.ObjectSpaceProvider = New XPObjectSpaceProvider(GetDataStoreProvider(args.ConnectionString, args.Connection), True)
		args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
	End Sub
	Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As System.Data.IDbConnection) As IXpoDataStoreProvider
		Dim application As System.Web.HttpApplicationState = If(System.Web.HttpContext.Current IsNot Nothing, System.Web.HttpContext.Current.Application, Nothing)
		Dim dataStoreProvider As IXpoDataStoreProvider = Nothing
		If application IsNot Nothing AndAlso application("DataStoreProvider") IsNot Nothing Then
			dataStoreProvider = TryCast(application("DataStoreProvider"), IXpoDataStoreProvider)
		Else
			dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, True)
			If application IsNot Nothing Then
				application("DataStoreProvider") = dataStoreProvider
			End If
		End If
		Return dataStoreProvider
	End Function
	Private Sub DetailViewTabCountAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
#If EASYTEST Then
			e.Updater.Update()
			e.Handled = True
#Else
		If System.Diagnostics.Debugger.IsAttached Then
			e.Updater.Update()
			e.Handled = True
		Else
			Dim message As String = "The application cannot connect to the specified database, " & "because the database doesn't exist, its version is older " & "than that of the application or its schema does not match " & "the ORM data model structure. To avoid this error, use one " & "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."

			If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
				message &= vbCrLf & vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
			End If
			Throw New InvalidOperationException(message)
		End If
#End If
	End Sub
	Private Sub InitializeComponent()
		Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
		Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
		Me.module3 = New DetailViewTabCountVB.Module.DetailViewTabCountModuleVB()
		Me.module4 = New DetailViewTabCountVB.Module.Web.DetailViewTabCountAspNetModule()
		DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		' 
		' DetailViewTabCountAspNetApplication
		' 
		Me.ApplicationName = "DetailViewTabCount"
		Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
		Me.Modules.Add(Me.module1)
		Me.Modules.Add(Me.module2)
		Me.Modules.Add(Me.module3)
		Me.Modules.Add(Me.module4)
		'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
		'ORIGINAL LINE: this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.DetailViewTabCountAspNetApplication_DatabaseVersionMismatch);
		DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
End Class