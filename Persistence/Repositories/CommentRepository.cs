using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "comment";

        public CommentRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<CommentReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QueryFirstOrDefaultAsync<CommentReadModel>(sql, new { Id = id });
        }

        public Task<CommentReadModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id AND UserId = @UserId";

            return _sqlClient.QueryFirstOrDefaultAsync<CommentReadModel>(sql, new { Id = id, UserId = userId });
        }

        public Task<IEnumerable<CommentReadModel>> GetByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.QueryAsync<CommentReadModel>(sql, new { CampgroundId = campgroundId });
        }

        public Task<int> SaveOrUpdateAsync(CommentWriteModel comment)
        {
            var sql = $"INSERT INTO {TableName} (Id, CampgroundId, Raiting, Text, UserId, DateCreated) VALUES(@Id, @CampgroundId, @Raiting, @Text, @UserId, @DateCreated)" +
                $"ON DUPLICATE KEY UPDATE Raiting = @Raiting, Text = @Text;";

            return _sqlClient.ExecuteAsync(sql, comment);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id;";

            return _sqlClient.ExecuteAsync(sql, new { Id = id });
        }

        public Task<int> DeleteByCampgroundIdAsync(Guid campgroundId)
        {
            var sql = $"DELETE FROM {TableName} WHERE CampgroundId = @CampgroundId;";

            return _sqlClient.ExecuteAsync(sql, new { CampgroundId = campgroundId });
        }
    }
}
