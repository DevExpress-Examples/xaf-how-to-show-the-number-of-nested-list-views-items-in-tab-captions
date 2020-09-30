Option Infer On
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl

Public Class Updater
    Inherits ModuleUpdater

    Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
        MyBase.New(objectSpace, currentDBVersion)
    End Sub
    Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
        MyBase.UpdateDatabaseAfterUpdateSchema()

        Dim employee = ObjectSpace.FindObject(Of Employee)(Nothing)
        If employee Is Nothing Then
            Dim maria = ObjectSpace.CreateObject(Of Employee)()
            maria.FirstName = "Maria"
            maria.LastName = "Anders"

            Dim ana = ObjectSpace.CreateObject(Of Employee)()
            ana.FirstName = "Ana"
            ana.LastName = "Trujillo"

            Dim antonio = ObjectSpace.CreateObject(Of Employee)()
            antonio.FirstName = "Antonio"
            antonio.LastName = "Moreno"

            Dim task = ObjectSpace.CreateObject(Of Task)()
            task.Subject = "Ana's task 1"
            task.AssignedTo = ana
            task = ObjectSpace.CreateObject(Of Task)()
            task.Subject = "Ana's task 2"
            task.AssignedTo = ana

            task = ObjectSpace.CreateObject(Of Task)()
            task.Subject = "Antonio's task"
            task.AssignedTo = antonio

            Dim phoneNumber = ObjectSpace.CreateObject(Of PhoneNumber)()
            phoneNumber.Number = "+1234567890"
            phoneNumber.Party = ana

            phoneNumber = ObjectSpace.CreateObject(Of PhoneNumber)()
            phoneNumber.Number = "+1234567890"
            phoneNumber.Party = antonio
        End If

        ObjectSpace.CommitChanges() 'Uncomment this line to persist created object(s).
    End Sub
    Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
        MyBase.UpdateDatabaseBeforeUpdateSchema()
        'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        '}
    End Sub
End Class