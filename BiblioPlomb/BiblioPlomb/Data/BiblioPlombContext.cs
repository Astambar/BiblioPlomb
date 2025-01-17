using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Models;

namespace BiblioPlomb.Data
{
    public class BiblioPlombContext : DbContext
    {
        public BiblioPlombContext (DbContextOptions<BiblioPlombContext> options)
            : base(options)
        {
        }

        public DbSet<BiblioPlomb.Models.Emprunt> Emprunt { get; set; } = default!;
    }
}
