using KKTraders.Data;
using KKTraders.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Repository
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<PostModel>> GetAllPost()
        {
            return  await _context.Posts.Select(x => new PostModel()
            {
                Id=x.Id,
                Name=x.Name,
                salary=x.salary,
                JobDescription=x.JobDescription
            }).ToListAsync();
        }
        public async Task<PostModel> DeletePost(int id)
        {
            PostModel pm = new PostModel();
                var postData = await _context.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (postData != null)
                {
                    _context.Posts.Remove(postData);
                    await _context.SaveChangesAsync();
                    pm = new PostModel()
                    {
                        Id = postData.Id,
                        Name = postData.Name,
                        salary = postData.salary,
                        JobDescription = postData.JobDescription
                    };
                }
            return pm;
        }
        public async Task<string> AddNewPost(PostModel model)
        {
            var post = new Post()
            {
                Name = model.Name,
                salary = model.salary,
                JobDescription = model.JobDescription
            };
            await  _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return "success";
        }

        public async Task<PostModel> GetPostById(int id)
        {
             return  await _context.Posts.Where(x => x.Id == id).Select(x => new PostModel()
            {
                Name = x.Name,
                salary = x.salary,
                JobDescription = x.JobDescription,


            }).FirstOrDefaultAsync();
        }
        public async Task<string> EditPost( PostModel model)
        {
            var post= _context.Posts.Where(x => x.Id == model.Id).FirstOrDefault();
            post.Name = model.Name;
            post.salary = model.salary;
            post.JobDescription = model.JobDescription;
            _context.Posts.Update(post);
            _context.SaveChanges();
            return " Success";

            
        }
    }
}
