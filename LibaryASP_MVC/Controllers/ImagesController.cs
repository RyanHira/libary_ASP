﻿using LibaryASP_MVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibaryASP_MVC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
        {
			this.imageRepository = imageRepository;
		}
        [HttpPost]
		public async Task<IActionResult> UploadAsync(IFormFile file)
		{
			//call repo
			var imageURL = await imageRepository.UploadAsync(file);

			if (imageURL == null)
			{
				return Problem("Someting went wrong!", null, (int)HttpStatusCode.InternalServerError);
			}

			return new JsonResult(new { link = imageURL });
		}
	}
}