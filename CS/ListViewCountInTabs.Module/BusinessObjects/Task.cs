using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace ListViewCountInTabs.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Task : BaseObject
    {
        public Task(Session session) : base(session) { }

        string subject;
        public string Subject
        {
            get { return subject; }
            set { SetPropertyValue(nameof(Subject), ref subject, value); }
        }

        Employee assignedTo;
        [Association("Employee-Tasks")]
        public Employee AssignedTo
        {
            get { return assignedTo; }
            set { SetPropertyValue(nameof(AssignedTo), ref assignedTo, value); }
        }
    }
}
