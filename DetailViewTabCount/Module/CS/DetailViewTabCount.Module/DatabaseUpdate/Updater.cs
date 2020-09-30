using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DetailViewTabCount.Module.BusinessObjects;
using DevExpress.Persistent.BaseImpl;
using Task = DetailViewTabCount.Module.BusinessObjects.Task;

namespace DetailViewTabCount.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            var employee = ObjectSpace.FindObject<Employee>(null);
            if (employee == null)
            {
                var maria = ObjectSpace.CreateObject<Employee>();
                maria.FirstName = "Maria";
                maria.LastName = "Anders";

                var ana = ObjectSpace.CreateObject<Employee>();
                ana.FirstName = "Ana";
                ana.LastName = "Trujillo";

                var antonio = ObjectSpace.CreateObject<Employee>();
                antonio.FirstName = "Antonio";
                antonio.LastName = "Moreno";

                var task = ObjectSpace.CreateObject<Task>();
                task.Subject = "Ana's task 1";
                task.AssignedTo = ana;
                task = ObjectSpace.CreateObject<Task>();
                task.Subject = "Ana's task 2";
                task.AssignedTo = ana;

                task = ObjectSpace.CreateObject<Task>();
                task.Subject = "Antonio's task";
                task.AssignedTo = antonio;

                var phoneNumber = ObjectSpace.CreateObject<PhoneNumber>();
                phoneNumber.Number = "+1234567890";
                phoneNumber.Party = ana;

                phoneNumber = ObjectSpace.CreateObject<PhoneNumber>();
                phoneNumber.Number = "+1234567890";
                phoneNumber.Party = antonio;
            }

            ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
