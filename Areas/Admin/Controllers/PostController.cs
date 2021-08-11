using KKTraders.Models;
using KKTraders.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace KKTraders.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        public PostController(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public  async Task<IActionResult> AllPost()
        {
            var allPost =  await _postRepository.GetAllPost();
            return View(allPost);
        }
        
        public async Task<IActionResult> DeletePost(int id)
        { 
            PostModel model = new PostModel();
            model=  await _postRepository.DeletePost(id);
            if(id>0)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(AllPost));
            }
            
            
        }
        public async Task<IActionResult> AddNewPost(bool isSubmitted = false)
        {
            ViewBag.isSubmitted = isSubmitted;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewPost(PostModel model)
        {
            if(ModelState.IsValid)
            {
                string message = await _postRepository.AddNewPost(model);
                ViewBag.Message = message;
                return RedirectToAction(nameof(AddNewPost),new { isSubmitted=true});
            }
            ViewBag.isSubmitted=false;
            return View();
        }
        
        public async Task<IActionResult> EditPost(int id)
        {
            var post=await  _postRepository.GetPostById(id);

            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> EditPost(PostModel model)
        {
            var post = await _postRepository.EditPost(model);

            return RedirectToAction(nameof(AllPost));
        }
    }

}



