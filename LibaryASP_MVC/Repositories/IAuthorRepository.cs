using LibaryASP_MVC.Models.Domain;

namespace LibaryASP_MVC.Repositories
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAllAsync();

		Task<Author?> GetAsync(Guid id);

		Task<Author>AddAsync(Author author);

		//kan null zijn 
		Task<Author?> UpdateAsync(Author author);

		Task<Author?> DeleteAsync(Guid id);
	}
}
