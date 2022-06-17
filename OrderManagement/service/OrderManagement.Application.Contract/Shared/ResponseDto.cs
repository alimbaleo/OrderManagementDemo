namespace OrderManagement.Application.Contract.Shared
{
    public class ResponseDto<T> where T : class
    {
        public ResponseDto(string errorMessage, int responseCode)
        {
            ResponseCode = responseCode;
            ErrorMessage = errorMessage;
        }

        public ResponseDto( T data)
        {
            Status = true;
            Data = data;
            ResponseCode = data == null? 204: 200;
        }

        public bool Status { get; set; }
        public T Data { get; set; }
        public int ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
