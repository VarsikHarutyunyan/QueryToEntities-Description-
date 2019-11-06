using System;
using  System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Queries_LINQ_to_Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Where

            Console.WriteLine("Hello World!");
            using (Context db = new Context())
            {
                var phones = db.Phones.Where(p => p.Company.Name == "Apple");
                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price})");
            }

            using (Context db = new Context())
            {
                var phones = (from phone in db.Phones
                    where phone.Company.Name == "Apple"
                    select phone).ToList();
                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price})");
            }

            #endregion

            #region Functions.Like

            //  %: соответствует любой подстроке, которая может иметь любое количество символов, при этом подстрока может и не содержать ни одного символа
            //  _: соответствует любому одиночному символу
            // []: соответствует одному символу, который указан в квадратных скобках
            //[-]: соответствует одному символу из определенного диапазона
            //[^]: соответствует одному символу, который не указан после символа ^
            using (Context db = new Context())
            {
                var phones = db.Phones.Where(p => EF.Functions.Like(p.Name, "%Galaxy%"));
                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price})");
            }

            using (Context db = new Context())
            {
                var phones = from p in db.Phones
                    where EF.Functions.Like(p.Name, "iPhone [6-8]%")
                    select p;

                foreach (Phone phone in phones)
                    Console.WriteLine($"{phone.Name} ({phone.Price})");
            }

            #endregion

            #region Find

            using (Context db = new Context())
            {

                Phone myphone = db.Phones.Find(3);

                Console.WriteLine(myphone.Name);
            }

            #endregion

            #region First/FirstOrDefault

            using (Context db = new Context())
            {
                Phone myphone = db.Phones.FirstOrDefault(p => p.Id == 3);

                if (myphone != null)
                    Console.WriteLine(myphone.Name);
            }
            #endregion
        }
    }
}
