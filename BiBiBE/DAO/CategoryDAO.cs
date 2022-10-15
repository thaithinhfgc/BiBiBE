using BiBiBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiBiBE.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public static async Task<List<Category>> GetCategorys()
        {
            var categories = new List<Category>();

            try
            {
                using (var context = new BiBiContext())
                {
                    categories = await context.Categories.ToListAsync();

                }
                return categories;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task AddCategory(Category m)
        {
            try
            {


                using (var context = new BiBiContext())
                {
                    var p1 = await context.Categories.FirstOrDefaultAsync(c => c.Title.Equals(m.Title));
                    var p2 = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId.Equals(m.CategoryId));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.Categories.Add(m);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }
                    }
                    else
                    {
                        throw new Exception("Categories tName is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateCategory(Category m)
        {
            try
            {
                using (var context = new BiBiContext())
                {


                    context.Entry<Category>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteCategory(int p)
        {
            try
            {
                using (var context = new BiBiContext())
                {
                    var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == p);
                    if (category == null)
                    {
                        throw new Exception("Id is not Exits");
                    }
                    else
                    {
                        context.Categories.Remove(category);
                        await context.SaveChangesAsync();
                    }



                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Category>> SearchByTitle(string search, int page, int pageSize)
        {
            List<Category> searchResult = null;
            if (page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 1000;
            }
            if (search == null)
            {
                IEnumerable<Category> searchValues = await GetCategorys();
                searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                searchResult = searchValues.ToList();
            }
            else
            {
                using (var context = new BiBiContext())
                {
                    IEnumerable<Category> searchValues = await (from film in context.Categories
                                                                where film.Title.ToLower().Contains(search.ToLower())
                                                                select film).ToListAsync();
                    searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                    searchResult = searchValues.ToList();
                }
            }

            return searchResult;
        }
        public async Task<Category> GetProductById(int Id)
        {

            IEnumerable<Category> categories = await GetCategorys();
            Category category = categories.SingleOrDefault(mb => mb.CategoryId == Id);
            return category;
        }

    }
}
