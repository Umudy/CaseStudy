using System.Linq;

using TemplateCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TemplateCore.DataAccess.Concrete.Contexts
{
    public class TemplateCoreContext : DbContext
    {
        public TemplateCoreContext(DbContextOptions options) : base(options)
        {
        }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
