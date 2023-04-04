using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkerRepository : IWalkerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
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

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id,
                             Walker.[Name] AS WalkerName, 
                             Walker.ImageUrl, 
                             Walker.NeighborhoodId,
                             Neighborhood.Id AS NeighborhoodId,
                             Neighborhood.[Name] AS NeighborhoodName
                        FROM Walker INNER JOIN Neighborhood 
                        ON Neighborhood.Id= Walker.NeighborhoodId
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                            //NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = new Neighborhood()
                            {
                                //Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                            },
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                        };

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id,
                             Walker.[Name] As WalkerName, 
                             Walker.ImageUrl, 
                             Neighborhood.[Name] AS NeighborhoodName,
                             Dog.[Name] As DogName
                        FROM Walker 
                        INNER JOIN Neighborhood ON Neighborhood.Id= Walker.NeighborhoodId
                        INNER JoIN Walks ON Walker.Id = Walks.WalkerId
                        INNER Join Dog ON Walks.DogId = Dog.Id
                        WHERE Walker.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("WalkerName")),
                            //NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = new Neighborhood()
                            {
                                //Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                            },
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Dogs = new List<Dog>()
                        };

                        while (reader.Read())
                        {
                            walker.Dogs.Add(new Dog()
                            {
                                Name = reader.GetString(reader.GetOrdinal("DogName"))
                            });
                        }

                        reader.Close();
                        return walker;
                    }

                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
    }
}
