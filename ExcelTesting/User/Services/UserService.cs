using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTesting
{
    public class UserService : IUserService
    {
        readonly ContextDb _repo;
        public UserService(ContextDb repo) 
        {
            _repo = repo;
        }


        public async Task<bool> ExcelCopy(IFormFile filePath)
        {
            if (filePath == null)
            {
                throw new Exception("Chose a file axper jan");
                return false;
            }

            try { 
                List<User> users = new List<User>();

                using (var reader = new StreamReader(filePath.OpenReadStream()))
                {
                    List<string> listA = new List<string>();
                    List<string> listB = new List<string>();
                    List<string> listD = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listA.Add(values[0]);
                        listB.Add(values[1]);
                        listD.Add(values[2]);
                    }
                    if (reader.EndOfStream)
                    {
                        
                        for (int i = 1; i < listA.Count; i++)
                        {
                            var b = listB[i];
                            var d = listD[i];

                            var newUser = new User { Name = b, LastName = d };

                            users.Add(newUser);
                        }

                        _repo.AddRange(users);
                        _repo.SaveChanges();

                        return true;
                    }
                }
               
            }
            catch(Exception exo)
            {
                Console.WriteLine(exo.InnerException.Message);
            }
            return false;
        }

    }
}
