using Utils.ResponseModels.Enums;

namespace Utils.ResponseModels
{
    public class BaseResponse<T>
    {
        public string ErrorDescription { get; set; } = string.Empty;
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}