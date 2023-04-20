using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySolution.Module.BusinessObjects;
[DefaultClassOptions]
public class Contact : BaseObject {
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual DateTime BirthDate { get; set; }

    public virtual ObservableCollection<MyTask> MyTasks { get; set; } = new ObservableCollection<MyTask>();
    public virtual ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
}
[DefaultClassOptions]
public class MyTask : BaseObject {
    public virtual string Subject{get;set;}
    public virtual Contact AssignedTo{ get; set; }
}
[DefaultClassOptions]
public class Car : BaseObject {
    public virtual string CarName { get; set; }
    public virtual Contact AssignedTo { get; set; }
}


