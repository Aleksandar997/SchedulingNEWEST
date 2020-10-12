using Configuration.Repository.Interfaces;
using Entity.Base;
using Entity.Models;
using SQLContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Repository.Implementations
{
    public class ThemeRepository : RepositoryBase, IThemeRepository
    {
        public ThemeRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<ResponseBase<IEnumerable<Theme>>> SelectAll(long userId) =>
            await QueryMultipleAsync(
              "[dbo].[Theme_SelectAll]",
              new { userId },
              (reader) => reader.Read<Theme>()
            );

        public async Task<ResponseBase<Theme>> SetTheme(Theme theme, long userId) =>
            await ExecuteReaderAsync<Theme>(
                "[dbo].[Theme_SetTheme]",
                new { theme.ThemeId, userId },
                (reader) => reader.ReadFirst<Theme>()
            );
    }
}
