using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.RequestModels
{
    public class UpdateCommentRequestModel
    {
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5.")]
        public int Raiting { get; set; }

        public string Text { get; set; }
    }
}
