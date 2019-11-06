using System;
using System.Linq;

namespace Queries_LINQ_to_Entities
{
    class Program
    {
        static void Main(string[] args)
        {

             #region Projection

            using (Context db = new Context())
            {
                var phones = db.Phones.Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Company = p.Company.Name
                });
                foreach (var phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price}) - {phone.Company}");
            }

            using (Context db = new Context())
            {
                var phones = db.Phones.Select(p => new Model
                {
                    Name = p.Name,
                    Price = p.Price,
                    Company = p.Company.Name
                });
                foreach (Model phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price}) - {phone.Company}");
            }

            #endregion

            #region Sorting

            using (Context db = new Context())
            {
                var phones = db.Phones.OrderBy(p => p.Name);
                foreach (var phone in phones)
                    Console.WriteLine($"{phone.Id}.{phone.Name} ({phone.Price})");
            }

            using (Context db = new Context())
            {
                var phones = from p in db.Phones
                    orderby p.Name
                    select p;
                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Id}.{phone.Name} ({phone.Price})");
            }

            using (Context db = new Context())
            {

                var phones = db.Phones.OrderByDescending(p => p.Name);

                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Id}.{phone.Name} ({phone.Price})");
            }

            using (Context db = new Context())
            {
                var phones = db.Phones.OrderBy(p => p.Price).ThenBy(p => p.Company.Name);

                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Id}.{phone.Name} ({phone.Price})");
            }
            #endregion

        }
    }
}
