﻿using Blogger.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.web.Data
{
    public class BloggieDbContext: DbContext
    {
        public BloggieDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }


    }
}
