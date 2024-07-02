namespace HealthInsurance.Models
{
    public class Join
    {
        public User user { get; set; }
        public Subscription subscription { get; set; }
        public Beneficiary beneficiary { get; set; }
    }
}
