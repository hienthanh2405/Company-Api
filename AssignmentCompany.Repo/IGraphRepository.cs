using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Neo4jClient;

namespace AssignmentCompany.Repo
{
    public interface IGraphRepository
    {
        IGraphClient GraphClient { get; set; }
        IDriver Driver { get; set; }
    }
}
