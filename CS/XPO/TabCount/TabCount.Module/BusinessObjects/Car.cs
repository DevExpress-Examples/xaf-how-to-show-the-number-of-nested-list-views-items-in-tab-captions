using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace dxTestSolution.Module.BusinessObjects {
    [DefaultClassOptions]

    public class Car : BaseObject {
        public Car(Session session)
            : base(session) {
        }
        string _carName;
        public string CarName {
            get {
                return _carName;
            }
            set {
                SetPropertyValue(nameof(CarName), ref _carName, value);
            }
        }
        Contact _assignedTo;
        [Association]
        public Contact AssignedTo {
            get {
                return _assignedTo;
            }
            set {
                SetPropertyValue(nameof(AssignedTo), ref _assignedTo, value);
            }
        }
    }
}