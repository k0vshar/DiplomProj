namespace Diplom.Domain.Response
{
    public interface BaseResponse<T> : IBaseResponse<T>
    {
        string Description { get; }
        public StatusCode StatusCode { get; }
        public T Data { get; }
    }
}
