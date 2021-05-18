using System.Collections.Generic;
using System.Linq;
using Training.Entity.Authors;

namespace Training.EntityFrameworkCore.Seed.Host
{
    public class DefaultAuthorCreator
    {
        private readonly TrainingDbContext _context;

        public DefaultAuthorCreator(TrainingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateAuthor();
        }
        private void CreateAuthor()
        {
            IList<Author> defaultAuthors = new List<Author>();

            defaultAuthors.Add(new Author() { Name = "Nguyễn Nhật Ánh", Phone = "0123456789", Address = "VN", YearOfBirth = 1955 });
            defaultAuthors.Add(new Author() { Name = "Trang Hạ", Phone = "0123456789", Address = "VN", YearOfBirth = 1975 });
            defaultAuthors.Add(new Author() { Name = "Nguyễn Phong Việt", Phone = "0123456789", Address = "VN", YearOfBirth = 1980 });
            defaultAuthors.Add(new Author() { Name = "Anh Khang", Phone = "0123456789", Address = "VN", YearOfBirth = 1987 });
            defaultAuthors.Add(new Author() { Name = "Hamlet Trương", Phone = "0123456789", Address = "VN", YearOfBirth = 1988 });

            foreach (var defaultAuthor in defaultAuthors)
            {
                var author = _context.Authors
                        .Where(s => s.Name == defaultAuthor.Name)
                        .FirstOrDefault<Author>();

                if (author == null)
                {
                    _context.Authors.AddRange(defaultAuthor);
                    _context.SaveChanges();
                }
            }

        }
    }
}
