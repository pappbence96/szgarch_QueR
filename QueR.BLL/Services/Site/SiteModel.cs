using Newtonsoft.Json;

namespace QueR.BLL.Services.Site
{
    public class SiteModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public int CompanyId { get; set; }

        [JsonIgnore]
        public bool IsValid { get => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address); }
    }
}