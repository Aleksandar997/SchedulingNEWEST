using Entity.Base;
using SQLContext.Models;

namespace SQLContext.Services.Interfaces
{
    public interface IOrderByClauseService
    {
        OrderByClauseModel OrderBy(BasePaging paging);
    }
}
