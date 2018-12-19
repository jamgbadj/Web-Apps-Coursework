using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        //create new instance of blog database context
        private readonly MyBlogContext _context;

        public BlogPostsController(MyBlogContext context)
        {
            _context = context;
        }
        
        //default action for this controller
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPost.ToListAsync());
        }
        
        //view details of blog posts
        public async Task<IActionResult> DetailedBlogPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostID == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }
        
        //GET version of blog post creation method
        public IActionResult CreateBlogPost()
        {
            return View();
        }
        

        //POST overload of CreateBlogPost method, specific properties binded to prevent overposting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBlogPost([Bind("BlogPostID,Title,Body")] BlogPost blogPost)
        {
            //check if the model is valid
            if (ModelState.IsValid)
            {
                //save the blog post to the database
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }
        
        public async Task<IActionResult> EditBlogPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlogPost(int id, [Bind("BlogPostID,Title,Body")] BlogPost blogPost)
        {
            if (id != blogPost.BlogPostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.BlogPostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }
        
        public async Task<IActionResult> DeleteBlogPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.BlogPostID == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlogPostConfirmed(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPost.Any(e => e.BlogPostID == id);
        }
    }
}
