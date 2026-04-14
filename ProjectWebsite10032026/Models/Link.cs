namespace ProjectWebsite10032026.Models
{
    public class Link
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        // Use Url to avoid naming collision with the class name
        public string Url { get; set; }
    }
}
