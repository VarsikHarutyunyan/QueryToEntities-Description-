using System;
using System.Linq;

namespace Queries_LINQ_to_Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context db = new Context())
            {
               bool result1 = db.Phones.Any(p => p.Company.Name == "Samsung"); //Any()

               bool result2 = db.Phones.All(p => p.Company.Name == "Huawei"); //All() 
            }
            

            using (Context db = new Context())
            {
                int number1 = db.Phones.Count();
                // найдем кол-во моделей, которые в названии содержат Samsung
                int number2 = db.Phones.Count(p => p.Name.Contains("Samsung"));

                Console.WriteLine(number1);
                Console.WriteLine(number2);
            }

            using (Context db = new Context())
            {
                // минимальная цена
                int minPrice = db.Phones.Min(p => p.Price);
                // максимальная цена
                int maxPrice = db.Phones.Max(p => p.Price);
                // средняя цена на телефоны фирмы Samsung
                double avgPrice = db.Phones.Where(p => p.Company.Name == "Samsung")
                    .Average(p => p.Price);

                Console.WriteLine(minPrice);
                Console.WriteLine(maxPrice);
                Console.WriteLine(avgPrice);
            }

            using (Context db = new Context())
            {
                // суммарная цена всех моделей
                int sum1 = db.Phones.Sum(p => p.Price);
                // суммарная цена всех моделей фирмы Samsung
                int sum2 = db.Phones.Where(p => p.Name.Contains("Samsung"))
                    .Sum(p => p.Price);
                Console.WriteLine(sum1);
                Console.WriteLine(sum2);
            }
        }
    }
}
