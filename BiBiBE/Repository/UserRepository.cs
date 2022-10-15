using BiBiBE.DAO;
using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> GetMembers() => UserDAO.GetMembers();
        public Task<User> LoginMember(string email, string password) => UserDAO.Instance.Login(email, password);
        public Task<User> GetProfile(int AccountID) => UserDAO.Instance.GetProfile(AccountID);
        public Task DeleteMember(int m) => UserDAO.DeleteAccount(m);
        public Task ChangePassword(int AccountID, string password) => UserDAO.Instance.ChangePassword(AccountID, password);

        public Task AddMember(User m) => UserDAO.AddAccount(m);
        public Task UpdateMember(User m) => UserDAO.UpdateAccount(m);
        public Task<List<User>> SearchByEmail(string search, int page, int pageSize) => UserDAO.Instance.SearchByEmail(search, page, pageSize);
    }
}
