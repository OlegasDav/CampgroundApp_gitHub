using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "image";

        public ImageRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<ImageReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QueryFirstOrDefaultAsync<ImageReadModel>(sql, new { Id = id });
        }

        public Task<IEnumerable<ImageReadModel>> GetByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.QueryAsync<ImageReadModel>(sql, new { CampgroundId = campgroundId });
        }

        public Task<int> SaveOrUpdateAsync(ImageWriteModel image)
        {
            var sql = $"INSERT INTO {TableName} (Id, CampgroundId, Url) VALUES(@Id, @CampgroundId, @Url)" +
                $"ON DUPLICATE KEY UPDATE Url = @Url;";

            return _sqlClient.ExecuteAsync(sql, image);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new { Id = id });
        }

        public Task<int> DeleteByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"DELETE FROM {TableName} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.ExecuteAsync(sql, new { CampgroundId = campgroundId });
        }
    }
}
