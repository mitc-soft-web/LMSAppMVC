namespace LMSAppMVC.Models.DTOs.IDS
{
    public class LoginAttemptsResponse
    {
        public bool IsBlocked { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public int? RemainingSeconds { get; set; }
    }

}
