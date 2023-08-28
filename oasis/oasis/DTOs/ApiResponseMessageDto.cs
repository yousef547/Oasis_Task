namespace oasis.DTOs
{
    public class ApiResponseMessageDto
    {
        public object Date { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object Messages { get; set; }
    }
}
