using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibaryASP_MVC.Models.ViewModels
{
	public class EditItemRequest
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string Shortdescription { get; set; }
		public string FeatureImageUrl { get; set; }
		public string UrlHandle { get; set; }
		public string Genre { get; set; }
		public int Amount { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Location { get; set; }
		public bool Visible { get; set; }

		//display author
		public IEnumerable<SelectListItem> Authors { get; set; }
		//collect Author
		public string[] SelectedAuthors { get; set; } = Array.Empty<string>();
	}
}
