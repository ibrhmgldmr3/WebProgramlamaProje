namespace WebProgProje.Models
{
    public class ApiResponse
    {
        public string RequestId { get; set; }
        public string LogId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorCodeStr { get; set; }
        public string ErrorMsg { get; set; }
        public ErrorDetail ErrorDetail { get; set; }
        public Data Data { get; set; }
    }

    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Code { get; set; }
        public string CodeMessage { get; set; }
        public string Message { get; set; }
    }

    public class Data
    {
        public string Image { get; set; }
    }

}
