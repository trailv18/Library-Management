using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Training.Authorization.Roles;
using Training.Authorization.Users;
using Training.MultiTenancy;
using Training.Entity.Categories;
using Training.Entity.Authors;
using Training.Entity.Publishers;
using Training.Entity.Provinces;
using Training.Entity.Districts;
using Training.Entity.Books;
using Training.Entity.Libraries;
using Training.Entity.BookLibraries;
using Training.Entity.BorrowBooks;
using Training.Entity.BorrowBookDetails;
using Training.Entity.FavoriteBooks;

namespace Training.EntityFrameworkCore
{
    public class TrainingDbContext : AbpZeroDbContext<Tenant, Role, User, TrainingDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<BookLibrary> BookLibraries { get; set; }
        public DbSet<BorrowBook> BorrowBooks { get; set; }
        public DbSet<BorrowBookDetail> BorrowBookDetails { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
            : base(options)
        {
        }
    }
}
