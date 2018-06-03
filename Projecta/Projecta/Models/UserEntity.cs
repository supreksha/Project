using Newtonsoft.Json;

namespace UserData.Models
{
    public class UserEntity
    {
        [JsonProperty("id")]
        public uint Id {get; set;}
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
        [JsonProperty("emailId")]
        public string EmailId { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}