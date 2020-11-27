using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Identity.DTOs
{
    public class RegisterResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
