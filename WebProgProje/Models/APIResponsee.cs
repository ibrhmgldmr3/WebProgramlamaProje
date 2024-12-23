namespace WebProgramlamaProje.Models
{
    public class APIResponsee
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string image { get; set; } // Base64 formatındaki image
        }
    }
}