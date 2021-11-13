using Books_Api.Models;
using Books_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books_Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        ApiServices apiServices;
        public BooksController()
        {
            apiServices = new ApiServices();
        }

        [Route("Books")]
        [HttpGet]
        public async Task<ActionResult> Books()
        {
            ResultModel result = new ResultModel();
            List<Books> books = new List<Books>();
            result = await apiServices.Get<Books>("v1/Books");

            if (result.Success)
            {
                books = (List<Books>)result.Data;
                return Ok(books);
            }

            if (result.Messages == "NotFound")
            {
                return NotFound();
            }

            return BadRequest();
        }

        [Route("Books/{id}")]
        [HttpGet]
        public async Task<ActionResult> Books(int id)
        {
            ResultModel result = new ResultModel();
            Books book = new Books();
            result = await apiServices.Get<Books>("v1/Books",id);

            if (result.Success)
            {
                book = (Books)result.Data;
                return Ok(book);
            }

            if (result.Messages == "NotFound")
            {
                return NotFound();
            }

            return BadRequest();
        }
    }
}
