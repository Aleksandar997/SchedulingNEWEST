using Entity.Base;
using Localization.Interfaces;
using Localization.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SQLContext;
using SQLContext.Factories;
using SQLContext.Helpers;

namespace Localization.Implementation
{
    public class LocalizationRepository : RepositoryBase, ILocalizationRepository
    {
        public LocalizationRepository(string connectionString) : base(connectionString) 
        {
        }

        public async Task<ResponseBase<IEnumerable<Resource>>> SelectAll(LocalizationPaging paging) =>
            await QueryMultipleAsync(
              "[dbo].[Translate_SelectAll]",
              new
              {
                  paging.SortBy,
                  paging.SortOrder,
                  paging.Skip,
                  paging.Take,
                  paging.Resource,
                  paging.Translate
              },
              (reader) =>
              {
                  var resources = reader.Read<Resource>().ToList();
                  var translates = reader.Read<TranslateModel, Culture, TranslateModel>((translate, culture) =>
                  {
                      translate.Culture = culture;
                      return translate;
                  }, splitOn: "CultureId");

                  resources.ForEach(r => r.Translates = translates.Where(t => t.ResourceId == r.ResourceId));

                  var count = reader.ReadFirstOrDefault<int>();
                  return (resources.AsEnumerable(), count);
              }
            );

        public async Task<ResponseBase<IEnumerable<Culture>>> SelectAllByCulture() =>
            await QueryMultipleAsync(
              "[dbo].[Localization_Select]",
              (reader) =>
              {
                  var cultures = reader.Read<Culture>().ToList();
                  var localization = reader.Read<TranslateModel, Resource, TranslateModel>((translate, resource) =>
                  {
                      translate.Resource = resource;
                      return translate;
                  }, splitOn: "ResourceId");
                  cultures.ForEach(c =>
                  {
                      c.LocalizationPair = localization.Where(l => l.CultureId == c.CultureId).ToDictionary(k => k.Resource.Name, v => v.Value);
                  });
                  return cultures.AsEnumerable();
              }
            );

        public async Task<ResponseBase<Resource>> SelectById(int resourceId) =>
            await QueryMultipleAsync(
                "[dbo].[Translate_SelectById]",
                 new { resourceId },
                (reader) =>
                {
                    var resource = reader.ReadFirst<Resource>();
                    resource.Translates = reader.Read<TranslateModel, Culture, TranslateModel>((translate, culture) =>
                    {
                        translate.Culture = culture;
                        return translate;
                    }, splitOn: "CultureId");

                    return resource;
                }
            );

        public async Task<ResponseBase<Resource>> Save(Resource resource, int userId) =>
            await ExecuteReaderAsync(
                "[dbo].[Translate_Save]",
                new
                {
                    resource.ResourceId,
                    resource.Name,
                    Translates = ParameterHelper.ToUserDefinedTableType(resource.Translates.Select(x => new
                    {
                        x.TranslateId,
                        x.CultureId,
                        x.Value
                    }), "translate_list"),
                    userId
                },
                (reader) =>
                {
                    var resource = reader.ReadFirst<Resource>();
                    resource.Translates = reader.Read<TranslateModel, Culture, TranslateModel>((translate, culture) =>
                    {
                        translate.Culture = culture;
                        return translate;
                    }, splitOn: "CultureId");

                    return resource;
                }

            );

        public async Task<ResponseBase<IEnumerable<Culture>>> CultureSelectlist() =>
            await QueryMultipleAsync(
                "[dbo].[Culture_Selectlist]",
                (reader) => reader.Read<Culture>()
            );
    }
}
