using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models.ReadModels
{
    public class CampgroundReadModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Url { get; set; }
    }
}
