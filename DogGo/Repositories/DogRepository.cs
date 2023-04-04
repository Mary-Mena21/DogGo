using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private IConfiguration _config;
        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        /*----------------------------------GetAllDogs()---------------------------------------*/
        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"SELECT 
                                           [Id]
                                          ,[Name]
                                          ,[OwnerId]
                                          ,[Breed]
                                          ,[Notes]
                                          ,[ImageUrl]
                                      FROM [DogWalkerMVC].[dbo].[Dog]";
                    SqlDataReader reader= cmd.ExecuteReader();
                    var Dogs = new List<Dog>();
                    while (reader.Read())
                    {
                        Dog dog = new Dog()
                        {
                            //Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    Dogs.Add(dog);
                    }
                reader.Close();
                return Dogs;
                }
            }
        }




































    }
}
