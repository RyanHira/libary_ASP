using LibaryASP_MVC.Models.Domain;
using LibaryASP_MVC.Models.ViewModels;
using LibaryASP_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LibaryASP_MVC.Controllers
{
	public class AdminItemController : Controller
	{
		private readonly IAuthorRepository authorRepository;
		private readonly IitemRepository itemRepository;

		public AdminItemController(IAuthorRepository authorRepository , IitemRepository itemRepository)
        {
			this.authorRepository = authorRepository;
			this.itemRepository = itemRepository;
		}

		public ItemRepository ItemRepository { get; }

		[HttpGet]  
		public async Task<IActionResult> Add() 
		{
			//get Author van repositorie
			var author = await authorRepository.GetAllAsync();

			var model = new AddItemRequest
			{
				Authors = author.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddItemRequest addItemRequest)
		{
			var item = new Item
			{
				Title = addItemRequest.Title,
				Content = addItemRequest.Content,
				Shortdescription = addItemRequest.Shortdescription,
				FeatureImageUrl = addItemRequest.FeatureImageUrl,
				UrlHandle = addItemRequest.UrlHandle,
				Genre = addItemRequest.Genre,
				Amount = addItemRequest.Amount,
				ReleaseDate = addItemRequest.ReleaseDate,
				Location = addItemRequest.Location,
				Visible = addItemRequest.Visible,
			};

			//map author van selected authors
			var selectedAuthors = new List<Author>();
			foreach(var selectedAuthorId in addItemRequest.selectedAuthors)
			{
				var selectedAuthorIdAsGuid = Guid.Parse(selectedAuthorId);
				var existingAuthor = await authorRepository.GetAsync(selectedAuthorIdAsGuid);

				if (existingAuthor != null) 
				{
					selectedAuthors.Add(existingAuthor);
				}
			}
			//mapping authors 
			item.Authors = selectedAuthors;

			await itemRepository.AddAsync(item);

			return RedirectToAction("Add");
		}

	}
}
