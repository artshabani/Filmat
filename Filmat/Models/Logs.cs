namespace Filmat.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string? User { get; set; }
        public string? Details { get; set; }
        public string Item { get; set; }
    }
}
