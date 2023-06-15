using LibaryASP_MVC.Data;
using LibaryASP_MVC.Models.Domain;
using LibaryASP_MVC.Models.ViewModels;
using LibaryASP_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC.Controllers
{
    public class AdminAuthorController : Controller
    {
		private readonly IAuthorRepository authorRepository;

		public AdminAuthorController(IAuthorRepository authorRepository)
        {
			this.authorRepository = authorRepository;
		}

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddAuthorRequest addAuthorRequest)
        {
            //mapping AddAuthorRequest naar Author domain model 
            var author = new Author
            {
                Name = addAuthorRequest.Name
            };

            await authorRepository.AddAsync(author);
           

            return RedirectToAction("List");
        }
        [HttpGet]

        public async Task<IActionResult> List() 
        {
            //use dbcontext om de author te lezen
            var author = await authorRepository.GetAllAsync();
        
            return View(author);    
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var author = await authorRepository.GetAsync(id);
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

            var updatedAuthor = await authorRepository.UpdateAsync(author);
           
            if (updatedAuthor != author) 
            { 
                //show succes note
            }
            else
            {
                //show error note
            }
            return RedirectToAction("Edit", new { id = editAuthorRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditAuthorRequest editAuthorRequest)
        {
            var deleted = await authorRepository.DeleteAsync(editAuthorRequest.Id);

            if(deleted != null) 
            {
                //show scucces
                return RedirectToAction("List");
            }
            //show error noti
            return RedirectToAction("Edit", new { id = editAuthorRequest.Id });
        
        }

      

    }
}
