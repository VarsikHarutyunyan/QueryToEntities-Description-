using System;
using System.Linq;

namespace Queries_LINQ_to_Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Join

            using (Context db = new Context())
            {
                var phones = db.Phones.Join(db.Companies, // второй набор
                    p => p.CompanyId, // свойство-селектор объекта из первого набора
                    c => c.Id, // свойство-селектор объекта из второго набора
                    (p, c) => new // результат
                    {
                        Name = p.Name,
                        Company = c.Name,
                        Price = p.Price
                    });
                foreach (var p in phones)
                    Console.WriteLine("{0} ({1}) - {2}", p.Name, p.Company, p.Price);
            }

            using (Context db = new Context())
            {
                var phones = from p in db.Phones
                             join c in db.Companies on p.CompanyId equals c.Id
                             select new { Name = p.Name, Company = c.Name, Price = p.Price };

                foreach (var p in phones)
                    Console.WriteLine("{0} ({1}) - {2}", p.Name, p.Company, p.Price);
            }


            using (Context db = new Context())
            {
                var result = from phone in db.Phones
                             join company in db.Companies on phone.CompanyId equals company.Id
                             join country in db.Countries on company.CountryId equals country.Id
                             select new
                             {
                                 Name = phone.Name,
                                 Company = company.Name,
                                 Price = phone.Price,
                                 Country = country.Name
                             };

                foreach (var p in result)
                    Console.WriteLine("{0} ({1}) - {2} - {3}", p.Name, p.Company, p.Price, p.Country);
            }
            #endregion

            #region Group by


            using (Context db = new Context())
            {
                var groups = from p in db.Phones
                             group p by p.Company.Name;

                foreach (var group in groups)
                {
                    Console.WriteLine(group.Key);
                    foreach (var phone in group)
                        Console.WriteLine($"{phone.Name} - {phone.Price}");
                }
            }


            using (Context db = new Context())
            {
                var groups = db.Phones.GroupBy(p => p.Company.Name);

                foreach (var group in groups)
                {
                    Console.WriteLine(group.Key);
                    foreach (var phone in group)
                        Console.WriteLine($"{phone.Name} - {phone.Price}");
                }
            }

            using (Context db = new Context())
            {
                var groups = from p in db.Phones
                             group p by p.Company.Name into g
                             select new { Name = g.Key, Count = g.Count() };
                // альтернативный способ
                //var groups = db.Phones.GroupBy(p=>p.Company.Name)
                //                  .Select(g => new { Name = g.Key, Count = g.Count()});
                foreach (var c in groups)
                    Console.WriteLine($"Производитель: {c.Name} Кол-во моделей: {c.Count}");
            }
            #endregion
        }
    }
}
