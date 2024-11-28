using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerPlate.Entity.Entities.Abstract;
using BoilerPlate.Entity.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // SaveChanges icin CreatedDate ve ModifiedDate i otomatik olarak yerlestiren interceptor
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler uzerinde yapilan degisikliklerin ya da yeni eklenen verinin yakalnmasini saglayan propertydir. Update operasyonlarinda track edilen verileri yakalayip elde etmemizi saglar
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                TimeZoneInfo turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                DateTime istanbulTime = TimeZoneInfo.ConvertTime(DateTime.Now, turkeyTimeZone);
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = istanbulTime;
                    entry.Entity.ModifiedDate = istanbulTime;
                }
                if (entry.State == EntityState.Modified)
                    entry.Entity.ModifiedDate = istanbulTime;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
