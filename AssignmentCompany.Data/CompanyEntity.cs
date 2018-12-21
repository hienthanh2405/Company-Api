
namespace AssignmentCompany.Data
{
    public class CompanyEntity : BaseEntity
    {
        public const string LABEL_COMPANY = "Company";

        public CompanyEntity() : base(LABEL_COMPANY) { }

        public string TitleCompany { get; set; }
        public string NameCompany { get; set; }
    }
}
