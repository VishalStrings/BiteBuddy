using BiteBuddy.Web.Models;

namespace BiteBuddy.Web.Service.IService
{
    public interface IBaseService
    {
      Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
      Task<ResponseDto?> GetAsync(RequestDto requestDto);
    }
}
