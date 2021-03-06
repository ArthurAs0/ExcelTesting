using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTesting
{
    public interface IUserService
    {

        Task<bool> ExcelCopy(IFormFile filePath);
        Task<bool> DbFill(string name, string lastName);
        Task<List<User>> SearchDb([FromQuery] string searchText);

    }
}
