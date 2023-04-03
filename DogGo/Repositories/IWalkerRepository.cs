using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        SqlConnection Connection { get; }

        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
    }
}