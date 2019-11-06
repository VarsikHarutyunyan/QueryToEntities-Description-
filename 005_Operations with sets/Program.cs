using System;
using System.Linq;

namespace Queries_LINQ_to_Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Union

            using (Context db = new Context())
            {
                var phones = db.Phones.Where(p => p.Price < 50000)
                    .Union(db.Phones.Where(p => p.Name.Contains("Samsung")));
                foreach (var item in phones)
                    Console.WriteLine(item.Name);
            }

            using (Context db = new Context())
            {
                var result = db.Phones.Select(p => new { Name = p.Name })
                    .Union(db.Companies.Select(c => new { Name = c.Name }));

                foreach (var item in result)
                    Console.WriteLine(item.Name);
            }

            #endregion

            #region Intersect

            using (Context db = new Context())
            {
                var phones = db.Phones.Where(p => p.Price < 50000)
                    .Intersect(db.Phones.Where(p => p.Name.Contains("Samsung")));
                foreach (var item in phones)
                    Console.WriteLine(item.Name);
            }

            #endregion

            #region Except

            using (Context db = new Context())
            {
                var selector1 = db.Phones.Where(p => p.Price < 50000); // Samsung Galaxy S7, iPhone 6S
                var selector2 = db.Phones.Where(p => p.Name.Contains("Samsung")); // Samsung Galaxy S8, Samsung Galaxy S7
                var phones = selector1.Except(selector2); // результат -  iPhone 6S

                foreach (var item in phones)
                    Console.WriteLine(item.Name);
            }

            #endregion
        }
    }
}
