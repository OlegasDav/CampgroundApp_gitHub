using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ICampgroundRepository
    {
        Task<IEnumerable<CampgroundReadModel>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CampgroundReadModel> GetAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CampgroundReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CampgroundWriteModel campground);

        Task<int> DeleteAsync(Guid id);
    }
}
