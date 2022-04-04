using System;
using System.Linq;

namespace IntexProject.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext context { get; set; }

        public EFCrashRepository (CrashDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Crash> mytable => context.mytable;
    }
}
