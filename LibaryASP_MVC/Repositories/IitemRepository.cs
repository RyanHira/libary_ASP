using LibaryASP_MVC.Models.Domain;
using Microsoft.AspNetCore.Http.Features;

namespace LibaryASP_MVC.Repositories
{
	public interface IitemRepository
	{
		Task<IEnumerable<Item>> GetAllAsync();

		Task<Item?> GetAsync(Guid id);

		Task<Item> AddAsync(Item item);

		Task<Item?> UpdateAsync(Item item);

		Task<Item?> DeleteAsync(Guid id);
	}
}
