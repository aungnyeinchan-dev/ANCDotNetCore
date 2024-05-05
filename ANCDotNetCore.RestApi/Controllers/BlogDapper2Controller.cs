﻿using ANCDotNetCore.RestApi.Models;
using ANCDotNetCore.Shared;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ANCDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";

            var lst = _dapperService.Query<BlogModel>(query).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        { 
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
			([BlogTitle]
			,[BlogAuthor]
			,[BlogContent])
		VALUES
			(@BlogTitle
			,@BlogAuthor
			,@BlogContent)";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found.");
            }

            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle 
      ,[BlogAuthor] = @BlogAuthor 
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PathBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found.");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions = " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions = " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions = " [BlogContent] = @BlogContent, ";
            }

            if(conditions.Length == 0) 
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found.");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { BlogId = id });

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            var item = _dapperService.QueryFirstOrDafault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }
    }
}
