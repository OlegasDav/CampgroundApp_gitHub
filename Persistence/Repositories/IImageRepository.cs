using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageReadModel>> GetByCampgroundIdAsync(Guid campgroundId);

        Task<ImageReadModel> GetAsync(Guid id);

        Task<int> SaveOrUpdateAsync(ImageWriteModel image);

        Task<int> DeleteAsync(Guid id);

        Task<int> DeleteByCampgroundIdAsync(Guid campgroundId);
    }
}
