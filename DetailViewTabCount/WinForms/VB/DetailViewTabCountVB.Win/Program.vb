Imports System
Imports System.Configuration
Imports System.Windows.Forms
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.XtraEditors

Namespace DetailViewTabCountVB.Win
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
#If EASYTEST Then
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If
			WindowsFormsSettings.LoadApplicationSettings()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
			'EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
			If Tracing.GetFileLocationFromSettings() = DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder Then
				Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath
			End If
			Tracing.Initialize()
			Dim winApplication As New DetailViewTabCountWindowsFormsApplication()
			If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
			End If
#If EASYTEST Then
			If ConfigurationManager.ConnectionStrings("EasyTestConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("EasyTestConnectionString").ConnectionString
			End If
#End If
#If DEBUG Then
			If System.Diagnostics.Debugger.IsAttached AndAlso winApplication.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema Then
				winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
			End If
#End If
			Try
				winApplication.Setup()
				winApplication.Start()
			Catch e As Exception
				winApplication.StopSplash()
				winApplication.HandleException(e)
			End Try
		End Sub
	End Module
End Namespace
