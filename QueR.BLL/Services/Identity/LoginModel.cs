using Newtonsoft.Json;

namespace QueR.BLL.Services.Identity
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public bool Valid { get => !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Username); }
    }
}