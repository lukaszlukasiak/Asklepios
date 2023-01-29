using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Asklepios.Data.ServiceClasses
{
    public static class EFDebugging
    {
        public static void ListChanges(DbContext context)
        {
            var changes = from e in context.ChangeTracker.Entries()
                          where e.State != EntityState.Unchanged
                          select e;


            foreach (var change in changes)
            {
                if (change.State == EntityState.Added)
                {
                    // Log Added
                }
                else if (change.State == EntityState.Modified)
                {
                    // Log Modified
                    
                    //var item = change.Cast<IEntity>().Entity;
                    var originalValues = change.OriginalValues;
                    var currentValues = change.CurrentValues;

                    foreach (var propertyName in originalValues.Properties)
                    {
                        var original = originalValues[propertyName.Name];
                        var current = currentValues[propertyName.Name];

                        if (!Equals(original, current))
                        {
                            Console.WriteLine("Inne wartosci w entity: " + change.Entity.ToString());
                            Console.WriteLine("Id encji: " + originalValues["Id"]);
                            Console.WriteLine("Wlasciwosc: "+    propertyName.Name);
                            Console.WriteLine("Orginalna: " + original);
                            Console.WriteLine("obecna: " + current);
                            // log propertyName: original --> current
                        }
                    }
                }
                else if (change.State == EntityState.Deleted)
                {
                    // log deleted
                }
            }


        }
    }
}
