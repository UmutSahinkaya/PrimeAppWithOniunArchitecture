namespace PrimeApp.MVC.Models
{
    public class PrimeInputViewModel
    {
        public string UserEmail { get; set; } = string.Empty;

        public string InputNumbers { get; set; } = string.Empty;

        public int? MaxPrime { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
