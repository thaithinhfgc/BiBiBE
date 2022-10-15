using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BiBiBE.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BiBiBE.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public static async Task<List<User>> GetMembers()
        {
            var members = new List<User>();

            try
            {
                using (var context = new BiBiContext())
                {
                    members = await context.Users.ToListAsync();

                }
                return members;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<User> Login(string email, string password)
        {

            IEnumerable<User> members = await GetMembers();
            User member = members.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
            return member;
        }
        public static async Task AddAccount(User m)
        {
            try
            {


                using (var context = new BiBiContext())
                {
                    var p1 = await context.Users.FirstOrDefaultAsync(c => c.Email.Equals(m.Email));
                    var p2 = await context.Users.FirstOrDefaultAsync(c => c.UserId.Equals(m.UserId));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.Users.Add(m);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }
                    }
                    else
                    {
                        throw new Exception("Email is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateAccount(User m)
        {
            try
            {
                using (var context = new BiBiContext())
                {


                    context.Entry<User>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteAccount(int p)
        {
            try
            {
                using (var context = new BiBiContext())
                {
                    var member = await context.Users.FirstOrDefaultAsync(c => c.UserId == p);
                    if (member == null)
                    {
                        throw new Exception("Id is not Exits");
                    }
                    else
                    {
                        context.Users.Remove(member);
                        await context.SaveChangesAsync();
                    }



                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<User>> SearchByEmail(string search, int page, int pageSize)
        {
            List<User> searchResult = null;
            if (page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 1000;
            }
            if (search == null)
            {
                IEnumerable<User> searchValues = await GetMembers();
                searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                searchResult = searchValues.ToList();
            }
            else
            {
                using (var context = new BiBiContext())
                {
                    IEnumerable<User> searchValues = await (from member in context.Users
                                                            where member.Email.ToLower().Contains(search.ToLower())
                                                            select member).ToListAsync();

                    searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                    searchResult = searchValues.ToList();
                }
            }

            return searchResult;
        }
        public async Task<User> GetProfile(int AccountID)
        {

            IEnumerable<User> members = await GetMembers();
            User member = members.SingleOrDefault(mb => mb.UserId == AccountID);
            return member;
        }
        public async Task ChangePassword(int AccountID, string password)
        {
            try
            {
                var user = new User() { UserId = AccountID, Password = password };
                using (var db = new BiBiContext())
                {
                    db.Users.Attach(user);
                    db.Entry(user).Property(x => x.Password).IsModified = true;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
