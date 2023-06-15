using LibaryASP_MVC.Data;
using LibaryASP_MVC.Models.Domain;

namespace LibaryASP_MVC.Repositories
{
	public class ItemRepository : IitemRepository
	{
		private readonly LibaryDbContext libaryDbContext;

		public ItemRepository(LibaryDbContext libaryDbContext)
        {
			this.libaryDbContext = libaryDbContext;
		}
        public async Task<Item> AddAsync(Item item)
		{
			await libaryDbContext.AddAsync(item);
			await libaryDbContext.SaveChangesAsync();
			return item;
		}

		public Task<Item?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Item>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Item?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Item?> UpdateAsync(Item item)
		{
			throw new NotImplementedException();
		}
	}
}
