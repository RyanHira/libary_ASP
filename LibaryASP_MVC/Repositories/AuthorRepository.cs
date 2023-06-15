using LibaryASP_MVC.Data;
using LibaryASP_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly LibaryDbContext libaryDbContext;

		public AuthorRepository(LibaryDbContext libaryDbContext)
        {
			this.libaryDbContext = libaryDbContext;
		}
        public async Task<Author> AddAsync(Author author)
		{
			await libaryDbContext.Authors.AddAsync(author);
			await libaryDbContext.SaveChangesAsync();
			return author;
		}

		public async Task<Author?> DeleteAsync(Guid id)
		{
			var existingAuthor = await libaryDbContext.Authors.FindAsync(id);

			if (existingAuthor != null) 
			{
				libaryDbContext.Authors.Remove(existingAuthor);
				await libaryDbContext.SaveChangesAsync();
				return existingAuthor;
			}
			return null;
		}

		public async Task<IEnumerable<Author>> GetAllAsync()
		{
			return await libaryDbContext.Authors.ToListAsync();
		}

		public async Task<Author?> GetAsync(Guid id)
		{
			return await libaryDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Author?> UpdateAsync(Author author)
		{
			var existingAuthor = await libaryDbContext.Authors.FindAsync(author.Id);

			if (existingAuthor != null) 
			{
				existingAuthor.Name = author.Name;

				//save 
				await libaryDbContext.SaveChangesAsync();

				return existingAuthor;
			}
			return null;
		}
	}
}
