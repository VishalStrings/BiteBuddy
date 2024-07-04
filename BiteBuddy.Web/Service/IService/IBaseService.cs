using BiteBuddy.Web.Models;

namespace BiteBuddy.Web.Service.IService
{
    public interface IBaseService
    {

      Task<ResponseDto?> SendAsync(RequestDto requestDto);
      Task<ResponseDto?> GetAsync(RequestDto requestDto);

    }
}
