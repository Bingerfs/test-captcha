namespace TestAccurateSecurityClient.Models
{
    public class CaptchaResponseCloudFlare
    {
        public bool Success { get; set; }
        public DateTime ChallengeTimestamp { get; set; }
        public string Hostname { get; set; }
        public string[] ErrorCodes { get; set; }
        public string Action { get; set; }
        public string CustomData { get; set; }
    }
}
