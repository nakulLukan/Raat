namespace Raat.Shared
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public ResponseDto(T data)
        {
            Data = data;
        }
    }
}
