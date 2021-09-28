using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.ResponseModels
{
    public class SaveCommentResponseModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public int Raiting { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
