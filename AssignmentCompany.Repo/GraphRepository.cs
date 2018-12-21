using Microsoft.Extensions.Options;
using Neo4j.Driver.V1;
using Neo4jClient;

namespace AssignmentCompany.Repo
{
    public class GraphRepository : IGraphRepository
    {
        private IOptions<Data.Neo4j> _injectedOptions;

        public GraphRepository(IOptions<Data.Neo4j> injectedOptions, IGraphClient graphClient)
        {
            _injectedOptions = injectedOptions;

            Driver = GraphDatabase.Driver(
                _injectedOptions.Value.Uri,
                AuthTokens.Basic(_injectedOptions.Value.User, _injectedOptions.Value.Password));

            GraphClient = graphClient;
        }

        public IGraphClient GraphClient { get; set; }
        public IDriver Driver { get; set; }
    }
}
