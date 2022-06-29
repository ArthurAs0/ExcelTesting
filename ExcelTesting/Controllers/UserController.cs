using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcelTesting
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : Controller
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpPost]
        public Task<bool> ExcelCopy(IFormFile file)
        {
            return _service.ExcelCopy(file); 
        }


        [HttpPost]
        [Route("DbFill")]
        public async Task<bool> DbFill(string name, string lastName)
        {
            return await _service.DbFill(name, lastName);
        }

        [HttpPost]
        [Route("Search")]
        public async Task<List<User>> SearchDb([FromQuery]string searchText)
        {
            return await _service.SearchDb(searchText);
        }



    }
}
