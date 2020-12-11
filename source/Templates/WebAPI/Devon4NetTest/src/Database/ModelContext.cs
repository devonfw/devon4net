using Microsoft.EntityFrameworkCore;
using Devon4Net.Domain.Context;

namespace Devon4Net.Domain.Entities
{
    public class ModelContext : Devon4NetBaseContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

    }
}
