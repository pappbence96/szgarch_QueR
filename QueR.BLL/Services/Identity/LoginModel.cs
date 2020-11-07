namespace QueR.BLL.Services.Identity
{
    public class LoginModel
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public bool Valid { get => !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Username); }
    }
}