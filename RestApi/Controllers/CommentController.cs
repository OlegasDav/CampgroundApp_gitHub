using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICampgroundRepository _campgroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICampgroundRepository campgroundRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _campgroundRepository = campgroundRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult<CommentsResponseModel>> GetComment(Guid id)
        //{
        //    var comment = await _commentRepository.GetAsync(id);

        //    if (comment is null)
        //    {
        //        return NotFound($"Comment with id: {id} does not exist");
        //    }

        //    return new CommentsResponseModel
        //    {
        //        Id = comment.Id,
        //        CampgroundId = comment.CampgroundId,
        //        Raiting = comment.Raiting,
        //        Text = comment.Text,
        //        UserId = comment.UserId,
        //        DateCreated = comment.DateCreated
        //    };
        //}

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SaveCommentResponseModel>> AddComment([FromBody] SaveCommentRequestModel request)
        {
            var campground = await _campgroundRepository.GetAsync(request.CampgroundId);

            if (campground is null)
            {
                return NotFound($"Campground with id: {request.CampgroundId} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var comment = new CommentWriteModel
            {
                Id = Guid.NewGuid(),
                CampgroundId = request.CampgroundId,
                Raiting = request.Raiting,
                Text = request.Text,
                UserId = user.Id,
                DateCreated = DateTime.Now
            };

            await _commentRepository.SaveOrUpdateAsync(comment);

            return new SaveCommentResponseModel
            {
                Id = comment.Id,
                CampgroundId = comment.CampgroundId,
                Raiting = comment.Raiting,
                Text = comment.Text,
                UserId = comment.UserId,
                DateCreated = comment.DateCreated
            };
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UpdateCommentResponseModel>> UpdateComment(Guid id, [FromBody] UpdateCommentRequestModel request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var ifExistsComment = await _commentRepository.GetAsync(id);

            if (ifExistsComment is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var commentToUpdate = await _commentRepository.GetAsync(id, user.Id);

            if (commentToUpdate is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to edit information of this comment");
            }

            var comment = new CommentWriteModel
            {
                Id = id,
                CampgroundId = commentToUpdate.CampgroundId,
                Raiting = request.Raiting,
                Text = request.Text,
                UserId = commentToUpdate.UserId,
                DateCreated = commentToUpdate.DateCreated
            };

            await _commentRepository.SaveOrUpdateAsync(comment);

            return new UpdateCommentResponseModel
            {
                Raiting = comment.Raiting,
                Text = comment.Text,
            };
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            var ifExistsComment = await _commentRepository.GetAsync(id);

            if (ifExistsComment is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var commentToDelete = await _commentRepository.GetAsync(id, user.Id);

            if (commentToDelete is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to delete this comment");
            }

            await _commentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
