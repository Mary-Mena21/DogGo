using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        SqlConnection Connection { get; }

        List<Dog> GetAllDogs();
    }
}