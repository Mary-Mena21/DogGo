using DogGo.Models;
using Microsoft.Data.SqlClient;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        SqlConnection Connection { get; }

        List<Owner> GetOwners();
        Owner GetOwner(int id);
        //Owner Delete(int id);
        void DeleteOwner(int ownerId);
        void UpdateOwner(Owner owner);
        void AddOwner(Owner owner);
        Owner GetOwnerByEmail(string email);
        Owner GetOwnerById(int id);
        void Delete(int id);
    }
}