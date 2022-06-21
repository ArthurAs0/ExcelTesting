using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
