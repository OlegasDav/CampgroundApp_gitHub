using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using RestApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Authorize]
    [EmailVerification]
    [ApiController]
    [Route("images")]
    public class ImageController : ControllerBase
    {
        private readonly ICampgroundRepository _campgroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public ImageController(ICampgroundRepository campgroundRepository, IUserRepository userRepository, IImageRepository imageRepository)
        {
            _campgroundRepository = campgroundRepository;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<ActionResult<SaveImageResponseModel>> AddImage([FromBody] SaveImageRequestModel request)
        {
            var campground = await _campgroundRepository.GetAsync(request.CampgroundId);

            if (campground is null)
            {
                return NotFound($"Campground with id: {request.CampgroundId} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var campgroundToAddImage = await _campgroundRepository.GetAsync(request.CampgroundId, user.Id);

            if (campgroundToAddImage is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to add the image to this campground");
            }

            var image = new ImageWriteModel
            {
                Id = Guid.NewGuid(),
                CampgroundId = request.CampgroundId,
                Url = request.Url

            };

            await _imageRepository.SaveOrUpdateAsync(image);

            return new SaveImageResponseModel
            {
                Id = image.Id,
                CampgroundId = image.CampgroundId,
                Url = image.Url
            };
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UpdateImageResponseModel>> UpdateImage(Guid id, [FromBody] UpdateImageRequestModel request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var ifExistsImage = await _imageRepository.GetAsync(id);

            if (ifExistsImage is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var campground = await _campgroundRepository.GetAsync(ifExistsImage.CampgroundId, user.Id);

            if (campground is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to edit image of this campground");
            }

            var image = new ImageWriteModel
            {
                Id = id,
                CampgroundId = campground.Id,
                Url = request.Url
            };

            await _imageRepository.SaveOrUpdateAsync(image);

            return new UpdateImageResponseModel
            {
                Url = image.Url
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var ifExistsImage = await _imageRepository.GetAsync(id);

            if (ifExistsImage is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var campground = await _campgroundRepository.GetAsync(ifExistsImage.CampgroundId, user.Id);

            if (campground is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to delete image of this campground");
            }

            await _imageRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
