namespace ProjectWebsite10032026.Models
{
    public class Download
    {
        public int ID { get; set; }
        // Optional display name for the file
        public string? FileName { get; set; }
        // Path or URL to the file
        public string? FilePath { get; set; }
        public string? Description { get; set; }

        // Timestamp when the download record was created
        public DateTime CreatedAt { get; set; }
    }
}
