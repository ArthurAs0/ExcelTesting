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

        public async Task<bool> DbFill(string name,string lastName)
        {

            List<User> listUsers = new List<User>();

            for (int i = 1; i < 100000; i++)
            {
                listUsers.Add(new User
                {
                    
                    Name = name,
                    LastName = lastName+i,
                });



            }

            _repo.AddRange(listUsers);
            await _repo.SaveChangesAsync();

            return true;
        }


        public async Task<bool> ExcelCopy(IFormFile filePath)
        {
            if (filePath == null)
                throw new Exception("Choose a file axper jan)");

            try { 

                List<User> users = new List<User>();

                using (var reader = new StreamReader(filePath.OpenReadStream()))
                {
                    
                    List<User> listExcel = new List<User>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listExcel.Add(new User 
                        {
                          Name = values[1],
                          LastName = values[2],
                        });                                
                                                
                    }

                    if (reader.EndOfStream)
                    {
                        
                        for (int i = 1; i < listExcel.Count; i++) 
                        {
                            var name = listExcel.Select(x => x.Name).ToArray();
                            var lastName = listExcel.Select(x => x.LastName).ToArray();

                            var newUser = new User { Name = name[i] , LastName = lastName[i]};

                            users.Add(newUser);
                        }

                        _repo.AddRange(users);
                       await _repo.SaveChangesAsync();

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
