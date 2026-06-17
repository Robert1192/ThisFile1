namespace ProjectWebsite10032026.Models
{
    public class Link
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        // URL for the link. Add corresponding DB column: Url (nvarchar(max)).
        public string? Url { get; set; }
    }
}
