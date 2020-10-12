using Entity.Base;
using FileManagement.Models;
using SQLContext;
using SQLContext.Factories;
using System.Threading.Tasks;

namespace FileManagement.Repository
{
    public class FileRepository : RepositoryBase, IFileRepository
    {
        public FileRepository(string connectionString) : base(connectionString)
        {
        }
        public async Task<ResponseBase<FormFileBase>> UploadFile(FormFileBase formFileBase) =>
            await ExecuteReaderAsync(
                "[dbo].[File_Save]",
                new 
                {
                    formFileBase.FileId,
                    formFileBase.FileName,
                    formFileBase.Extension,
                    formFileBase.BlobData,
                    formFileBase.PreviousFileName,
                    formFileBase.PreviousFileExtension
                },
                (reader) =>
                {
                    var res = reader.ReadFirst<int>();
                    formFileBase.FileId = res;
                    return formFileBase;
                }
            );
    }
}
