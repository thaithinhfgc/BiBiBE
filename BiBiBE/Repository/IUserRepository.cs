using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BiBiBE.Models;

namespace BiBiBE.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetMembers();
        Task<User> LoginMember(String email, String password);
        Task<User> GetProfile(int AccountID);
        Task ChangePassword(int AccountID, string password);

        Task DeleteMember(int m);

        Task UpdateMember(User m);
        Task AddMember(User m);
        Task<List<User>> SearchByEmail(string search, int page, int pageSize);
    }
}
