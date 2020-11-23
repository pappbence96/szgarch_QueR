using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QueR.Application.Middlewares.ExceptionHandling
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() } }); ;
        }
    }
}