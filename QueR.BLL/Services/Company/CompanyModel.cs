namespace QueR.BLL.Services.Company
{
    public class CompanyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public bool IsValid { get => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address); }
    }
}
