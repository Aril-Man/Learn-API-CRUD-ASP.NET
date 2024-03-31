namespace ContactApi.Models
{
    public class Contact
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public long phone { get; set; }
        public string address { get; set; }
    }
}
