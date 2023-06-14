using LibaryASP_MVC.Data;
using LibaryASP_MVC.Models.Domain;
using LibaryASP_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC.Controllers
{
    public class AdminAuthorController : Controller
    {
        private readonly LibaryDbContext libaryDbContext;

        public AdminAuthorController(LibaryDbContext libaryDbContext)
        {
            this.libaryDbContext = libaryDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddAuthorRequest addAuthorRequest)
        {
            //mapping AddAuthorRequest naar Author domain model 
            var author = new Author
            {
                Name = addAuthorRequest.Name
            };

            await libaryDbContext.Authors.AddAsync(author);
            await libaryDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }
        [HttpGet]

        public async Task<IActionResult> List() 
        {
            //use dbcontext om de author te lezen
            var author = await libaryDbContext.Authors.ToListAsync();

            return View(author);    
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            //1st method
            //var author = libaryDbContext.Authors.Find(id);
            
            //2dn method
            var author = await libaryDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author != null) 
            {
                var editAuthorRequest = new EditAuthorRequest
                {
                    Id = author.Id,
                    Name = author.Name
                };
                return View(editAuthorRequest);
            }


            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAuthorRequest editAuthorRequest) 
        {
            var author = new Author
            {
                Id = editAuthorRequest.Id,
                Name = editAuthorRequest.Name
            };

            var existingAuthor = await libaryDbContext.Authors.FindAsync(author.Id);

            if (existingAuthor != null) 
            {
                existingAuthor.Name = author.Name;
                //save 
                await libaryDbContext.SaveChangesAsync();
                //show succes note
                return RedirectToAction("Edit", new { id = editAuthorRequest.Id });
            }
            //show error noti
            return RedirectToAction("Edit", new { id = editAuthorRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditAuthorRequest editAuthorRequest)
        {
            var author = await libaryDbContext.Authors.FindAsync(editAuthorRequest.Id);

            if (author != null) 
            {
                libaryDbContext.Authors.Remove(author);
                await libaryDbContext.SaveChangesAsync();

                //succes noti
                return RedirectToAction("List");
            }
            //show error noti
            return RedirectToAction("Edit", new { id = editAuthorRequest.Id });
        
        }

      

    }
}
