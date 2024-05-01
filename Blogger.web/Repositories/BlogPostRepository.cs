using Blogger.web.Data;
using Blogger.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Blogger.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
             await bloggieDbContext.AddAsync(blogPost);
             await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog=  await bloggieDbContext.BlogPosts.FindAsync(id);
        
            if(existingBlog != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            return bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstAsync(x => x.Id == id);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingTag = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
        
            if (existingTag != null)
            {
                existingTag.Id = blogPost.Id;
                existingTag.Heading = blogPost.Heading;
                existingTag.PageTitle = blogPost.PageTitle;
                existingTag.Content = blogPost.Content;
                existingTag.ShortDescription = blogPost.ShortDescription;
                existingTag.Author = blogPost.Author;
                existingTag.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingTag.UrlHandle = blogPost.UrlHandle;
                existingTag.visible = blogPost.visible;
                existingTag.PublishedDate = blogPost.PublishedDate;
                existingTag.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
