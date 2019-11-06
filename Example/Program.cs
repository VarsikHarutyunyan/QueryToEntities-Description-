using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Queries_LINQ_to_Entities;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region query
            using (Context db = new Context())
            {
                var phone1 = (from phone in db.Phones.Include(p => p.Company)
                              where phone.CompanyId == 5
                              select phone).ToList();

                var phones2 = db.Phones.Include(p => p.Company).Where(p => p.CompanyId == 5);
            }
            #endregion

            #region Like
            using (Context db = new Context())
            {
                var phones3 = db.Phones.Where(p => EF.Functions.Like(p.Name, "%GAL%"));

                var phone4 = (from phone in db.Phones
                              where EF.Functions.Like(phone.Name, "%GAl%")
                              select phone).ToList();
            }
            #endregion

            #region Where
            using (Context db = new Context())
            {
                var phones5 = db.Phones.Where(p => p.Price == 100);

                var phones6 = (from phone in db.Phones.Where(p => p.Price == 100)
                               select phone).ToList();
            }
            #endregion

            #region Find
            using (Context db = new Context())
            {              
                var phones7 = db.Phones.Find(3);
            }
            #endregion

            #region  First/FirstOrDefault
            using (Context db = new Context())
            {
                var phones8 = db.Phones.FirstOrDefault(p => p.Id == 3);
            }
            #endregion

            #region Projection

            using (Context db = new Context())
            {
                var phones9 = db.Phones.Select(p => new Model
                {
                    Name = p.Name,
                    Price = p.Price,
                    Company = p.Company.Name
                });

                var phones10 = db.Phones.Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Company = p.Company
                });
            }

            #endregion

            #region Sorting
            using (Context db = new Context())
            {
                var phones11 = db.Phones.OrderBy(p => p.Name);

                var phones12 = from phone in db.Phones
                               orderby phone.Name
                               select phone;
            }
            #endregion

            #region Join
            using (Context db = new Context())
            {
                //nuyn axyusakum koxqic avelacnum e paymannerin bavararoxner@
                var phones13 = db.Phones.Join(db.Companies, p => p.CompanyId, c => c.Id,
                    (p, c) => new
                    {
                        Name = p.Name,
                        Company = c.Name,
                    });

                var phones14 = from p in db.Phones
                               join c in db.Companies on p.CompanyId equals c.Id
                               select new { Name = p.Name, Company = c.Name, Price = p.Price };
            }
            #endregion

            #region Group by
            using (Context db = new Context())
            {
                var phones15 = from p in db.Phones
                             group p by p.Company.Name;


                var phones16 = db.Phones.GroupBy(p => p.Company.Name);
            
            }
            #endregion

            #region Union

            using (Context db = new Context())
            {
                //vercnum e aj paymanin bavararoxner miavorum 2 paymanin bavararoxner@ u aranc krknutyan
                var phones18 = db.Phones.Where(p => p.Price > 500)
                    .Union(db.Phones.Where(p => p.Name.Contains("ab")));
              
            }
            #endregion

            #region Intersect
            using (Context db = new Context())
            {
               // vercnum e aj paymanin bavararoxner hatum 2 paymanin bavararoxneri het u aranc krknutyan
               var phones19 = db.Phones.Where(p => p.Price > 500)
                    .Intersect(db.Phones.Where(p => p.Name.Contains("ab")));
            }
            #endregion

            #region Except

            using (Context db = new Context())
            {
                // vercnum e aj paymanin bavararoxner hanum 2 paymanin bavararoxner@ ev hatum@,aysinqn mnum e miyay ayn vor@ ka arajini mej
                var selector1 = db.Phones.Where(p => p.Price < 50000); // Samsung Galaxy S7, iPhone 6S
                var selector2 = db.Phones.Where(p => p.Name.Contains("Samsung")); // Samsung Galaxy S8, Samsung Galaxy S7
                var phones = selector1.Except(selector2); // результат -  iPhone 6S

            }

            #endregion

            #region Aggregate operations
            using (Context db = new Context())
            {
                bool result1 = db.Phones.Any(p => p.Company.Name == "Samsung"); //true ete gone mek hat ka aydpisi

                bool result2 = db.Phones.All(p => p.Company.Name == "Huawei"); // true ete bo;or@  aydpisin en

                int number = db.Phones.Count(p => p.Name.Contains("Samsung"));

                int minprice = db.Phones.Min(p => p.Price);

                int maxprice = db.Phones.Max(p=>p.Price);

                double avgPrise = db.Phones.Where(p=>p.Price>100).Average(p => p.Price);

                int sum = db.Phones.Where(p => p.Name == "aa").Sum(p => p.Price);
            }
            #endregion

            #region filters for the model level

            using (Context db = new Context())
            {
                int price = db.Phones.IgnoreQueryFilters().Min(p=>p.Price);
            }
               
            #endregion
        }
    }
}
