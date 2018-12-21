using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentCompany.Data
{
    public class BaseEntity
    {
        private string LABEL;

        public BaseEntity(string LABEL)
        {
            this.LABEL = LABEL;
        }

        public Guid GlobalId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
