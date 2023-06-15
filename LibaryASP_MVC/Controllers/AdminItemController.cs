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

		public AdminItemController(IAuthorRepository authorRepository, IitemRepository itemRepository)
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
			foreach (var selectedAuthorId in addItemRequest.SelectedAuthors)
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

		[HttpGet]

		public async Task<IActionResult> List()
		{
			//repo call voor data
			var items = await itemRepository.GetAllAsync();

			return View(items);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			//verkijg result repo
			var item = await itemRepository.GetAsync(id);

			var authorsDomain = await authorRepository.GetAllAsync();

			if (item != null)
			{


				//domain naar vieModel
				var model = new EditItemRequest
				{
					Id = item.Id,
					Title = item.Title,
					Content = item.Content,
					Shortdescription = item.Shortdescription,
					FeatureImageUrl = item.FeatureImageUrl,
					UrlHandle = item.UrlHandle,
					Genre = item.Genre,
					Amount = item.Amount,
					ReleaseDate = item.ReleaseDate,
					Location = item.Location,
					Visible = item.Visible,
					Authors = authorsDomain.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedAuthors = item.Authors.Select(x => x.Id.ToString()).ToArray(),
				};

				return View(model);
			}
			//pass data naar view
			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> edit(EditItemRequest editItemRequest)
		{
			//map viewmodel terug domainmodel
			var itemDomainModel = new Item
			{
				Id = editItemRequest.Id,
				Title = editItemRequest.Title,
				Content = editItemRequest.Content,
				Shortdescription = editItemRequest.Shortdescription,
				FeatureImageUrl = editItemRequest.FeatureImageUrl,
				UrlHandle = editItemRequest.UrlHandle,
				Genre = editItemRequest.Genre,
				Amount = editItemRequest.Amount,
				ReleaseDate = editItemRequest.ReleaseDate,
				Location = editItemRequest.Location,
				Visible = editItemRequest.Visible
			};
			//authors ook 
			var selectedAuthor = new List<Author>();
			foreach (var SelectedAuthor in editItemRequest.SelectedAuthors)
			{
				if (Guid.TryParse(SelectedAuthor, out var author))
				{
					var foundAuthor = await authorRepository.GetAsync(author);

					if (foundAuthor != null)
					{
						selectedAuthor.Add(foundAuthor);
					}
				}
			}

			itemDomainModel.Authors = selectedAuthor;
			//submit naar repo omte updaten
			var updatedItem = await itemRepository.UpdateAsync(itemDomainModel);

			if (updatedItem != null)
			{
				//show succes note
				return RedirectToAction("Edit");
			}
			//redirect naar get
			return RedirectToAction("Edit");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(EditItemRequest editItemRequest)
		{
			//comniceer naar repo om te verwijderen
			var deletedItem = await itemRepository.DeleteAsync(editItemRequest.Id);
			if (deletedItem != null)
			{
				//succes note
				return RedirectToAction("List");
			}

			//Error note
			return RedirectToAction("Edit", new { id = editItemRequest.Id });
		}

	}
}
