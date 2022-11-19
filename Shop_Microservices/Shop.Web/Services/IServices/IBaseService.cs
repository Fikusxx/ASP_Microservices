namespace Shop.Web;

public interface IBaseService : IDisposable
{
    public ResponseDTO ResponseDTO { get; set; }

    public Task<T> SendAsync<T>(ApiRequest apiRequest);
}
