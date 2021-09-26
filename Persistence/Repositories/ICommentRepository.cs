using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentReadModel>> GetByCampgroundIdAsync(Guid campgroundId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CommentReadModel> GetAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CommentReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CommentWriteModel comment);

        Task<int> DeleteAsync(Guid id);

        Task<int> DeleteByCampgroundIdAsync(Guid campgroundId);
    }
}
