using LibaryASP_MVC.Data;
using LibaryASP_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

		public async Task<Item?> DeleteAsync(Guid id)
		{
			var existingItem = await libaryDbContext.Items.FindAsync(id);
			if (existingItem != null)
			{
				libaryDbContext.Items.Remove(existingItem);
				await libaryDbContext.SaveChangesAsync();
				return existingItem;
			}

			return null;
		}

		public async Task<IEnumerable<Item>> GetAllAsync()
		{
			return await libaryDbContext.Items.Include(x => x.Authors).ToListAsync();

		}

		public async Task<Item?> GetAsync(Guid id)
		{
			return await libaryDbContext.Items.Include(x => x.Authors).FirstOrDefaultAsync(x => x.Id == id);
		}

        public async Task<Item?> GetByUrlHandleAsync(string urlHandle)
        {
            return await libaryDbContext.Items.Include(x => x.Authors).FirstOrDefaultAsync(x =>x.UrlHandle == urlHandle);

        }

        public async Task<Item?> UpdateAsync(Item item)
		{
			var existingItem = await libaryDbContext.Items.Include(x => x.Authors).
				FirstOrDefaultAsync(x => x.Id == item.Id);
			if (existingItem != null)
			{
				existingItem.Id = item.Id;
				existingItem.Title = item.Title;
				existingItem.Content = item.Content;
				existingItem.Shortdescription = item.Shortdescription;
				existingItem.FeatureImageUrl = item.FeatureImageUrl;
				existingItem.UrlHandle = item.UrlHandle;
				existingItem.Genre = item.Genre;
				existingItem.Amount = item.Amount;
				existingItem.ReleaseDate = item.ReleaseDate;
				existingItem.Location = item.Location;
				existingItem.Visible = item.Visible;
				existingItem.Authors = item.Authors;

				await libaryDbContext.SaveChangesAsync();
				return existingItem;

			}
			return null;
		}
	}
}
