namespace Chemicals.MySqlData
{
    using System.Linq;

    using Models;
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class FluentModelContent : OpenAccessContext
    {
        private static BackendConfiguration backend =
            GetBackendConfiguration();

        private static MetadataSource metadataSource =
            new FluentModel();

        public FluentModelContent()
            : base("Server=localhost;Database=MolybdenumDb;Uid=root;Pwd=Fn:111211065;", backend, metadataSource)
        {
        }

        public IQueryable<Report> Reports
        {
            get
            {
                return this.GetAll<Report>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MySql";
            backend.ProviderName = "MySql.Data.MySqlClient";

            return backend;
        }
    }
}
