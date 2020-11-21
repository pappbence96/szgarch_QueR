using Newtonsoft.Json;

namespace QueR.BLL.Services.Company
{
    public class CompanyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public bool IsValid { get => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address); }
    }
}
