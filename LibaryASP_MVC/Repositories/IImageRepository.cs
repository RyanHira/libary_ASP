﻿namespace LibaryASP_MVC.Repositories
{
	public interface IImageRepository
	{
		Task<string> UploadAsync(IFormFile file);
	}
}
