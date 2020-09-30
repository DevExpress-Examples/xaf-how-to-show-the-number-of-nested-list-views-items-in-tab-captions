using System;
using DevExpress.ExpressApp.Xpo;

namespace DetailViewTabCount.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        public IXpoDataStoreProvider DataStoreProvider { get; set; }
    }
}
