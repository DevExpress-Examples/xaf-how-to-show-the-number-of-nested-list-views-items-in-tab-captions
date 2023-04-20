//,new MyUpdater(objectSpace,versionFromDB)
//            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/Contact_ListView", SecurityPermissionState.Allow);
//defaultRole.AddTypePermissionsRecursively<Contact>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using MySolution.Module.BusinessObjects;

namespace TabCount.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class MyUpdater : ModuleUpdater {
        public MyUpdater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //,new MyUpdater(objectSpace,versionFromDB)
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            var cnt = ObjectSpace.GetObjects<Contact>().Count;
            if (cnt > 0) {
                return;
            }
            for (int i = 0; i < 5; i++) {
                var contact = ObjectSpace.CreateObject<Contact>();
                contact.FirstName = "FirstName" + i;
                contact.LastName = "LastName" + i;
                contact.Age = i * 10;
                for (int j = 0; j < 2; j++) {
                    var task = ObjectSpace.CreateObject<MyTask>();
                    task.Subject = "Subject" + i + " - " + j;
                    task.AssignedTo = contact;
                }
                for (int j = 0; j < 3; j++) {
                    var task = ObjectSpace.CreateObject<Car>();
                    task.CarName = "Car" + i + " - " + j;
                    task.AssignedTo = contact;
                }

            }
            //secur#0  
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
