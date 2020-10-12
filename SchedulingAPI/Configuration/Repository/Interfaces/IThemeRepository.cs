using Entity.Base;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Repository.Interfaces
{
    public interface IThemeRepository
    {
        Task<ResponseBase<IEnumerable<Theme>>> SelectAll(long userId);
        Task<ResponseBase<Theme>> SetTheme(Theme theme, long UserId);
    }
}
