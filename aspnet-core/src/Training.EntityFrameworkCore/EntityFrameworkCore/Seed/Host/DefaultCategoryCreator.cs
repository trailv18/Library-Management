using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Entity.Categories;

namespace Training.EntityFrameworkCore.Seed.Host
{
    public class DefaultCategoryCreator
    {
        private readonly TrainingDbContext _context;

        public DefaultCategoryCreator(TrainingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCategory();
        }
        private void CreateCategory()
        {
            IList<Category> defaultCategories = new List<Category>();

            defaultCategories.Add(new Category() { Name = "Sách giáo khoa" });
            defaultCategories.Add(new Category() { Name = "Tiểu thuyết" });
            defaultCategories.Add(new Category() { Name = "Truyện tranh" });
            defaultCategories.Add(new Category() { Name = "Sách bài tập" });
            defaultCategories.Add(new Category() { Name = "Từ điển" });

            foreach (var defaultCategory in defaultCategories)
            {
                var category = _context.Categories
                        .Where(s => s.Name == defaultCategory.Name)
                        .FirstOrDefault<Category>();
                if (category == null)
                {
                    _context.Categories.AddRange(defaultCategory);
                    _context.SaveChanges();
                }
            }

        }
    }
}
