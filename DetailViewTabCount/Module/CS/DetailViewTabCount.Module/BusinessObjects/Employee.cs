using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace DetailViewTabCount.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Employee : Person
    {
        public Employee(Session session) : base(session) { }

        [Association("Employee-Tasks")]
        public XPCollection<Task> Tasks
        {
            get { return GetCollection<Task>(nameof(Tasks)); }
        }
    }
}
