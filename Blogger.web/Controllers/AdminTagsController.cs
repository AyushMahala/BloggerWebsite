using Blogger.web.Data;
using Blogger.web.Models.Domain;
using Blogger.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.web.Controllers
{

    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
            
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
            
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //use dbcontext to read the text
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }
    }
}
