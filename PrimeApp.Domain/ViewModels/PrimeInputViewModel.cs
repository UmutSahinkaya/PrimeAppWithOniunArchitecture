namespace PrimeApp.MVC.Models
{
    public class PrimeInputViewModel
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string InputNumbers { get; set; } = "";
        public int? MaxPrime { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
