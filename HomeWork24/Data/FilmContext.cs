using HomeWork24.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork24.Data
{
    public class FilmContext : DbContext
    {

        public FilmContext(DbContextOptions<FilmContext> options) : base(options)
        {

        }

        public DbSet<Film> Films { get; set; }
    }
}
