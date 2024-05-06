using ANCDotNetCore.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ANCDotNetCore.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ANCDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Cotroller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            /*AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            parameters[0] = new AdoDotNetParameter("@BlogId", id);
            var item = _adoDotNetService.Query<BlogModel>(query, parameters);*/

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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
            return Ok(message);
            //return StatusCode(500, message);
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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", blog.BlogId),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return StatusCode(500, message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found");
            }

            blog.BlogId = id;

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", blog.BlogId),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return StatusCode(500, message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data is found");
            }

            blog.BlogId = id;

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", blog.BlogId)
                );

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return StatusCode(500, message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            return item;
        }
    }
}
