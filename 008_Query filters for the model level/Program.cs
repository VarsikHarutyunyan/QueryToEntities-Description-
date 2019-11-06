using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Queries_LINQ_to_Entities;

namespace _008_Query_filters_for_the_model_level
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Context db = new Context())
            {
                Role adminRole = new Role { Name = "admin" };
                Role userRole = new Role { Name = "user" };
                User user1 = new User { Name = "Tom", Age = 17, Role = userRole };
                User user2 = new User { Name = "Bob", Age = 18, Role = userRole };
                User user3 = new User { Name = "Alice", Age = 19, Role = adminRole };
                User user4 = new User { Name = "Sam", Age = 20, Role = adminRole };
                db.Roles.AddRange(userRole, adminRole);
                db.Users.AddRange(user1, user2, user3, user4);
                db.SaveChanges();
            }

            using (Context db = new Context() { RoleId = 1 })
            {
                var users = db.Users.Include(u => u.Role).ToList();
                foreach (User user in users)
                    Console.WriteLine($"Name: {user.Name}  Age: {user.Age}  Role: {user.Role?.Name}");
            }

            using (Context db = new Context() { RoleId = 1 })
            {
                int minAge = db.Users.Min(x => x.Age);
                Console.WriteLine(minAge);  // 19
            }

            using (Context db = new Context() { RoleId = 1 })
            {
                int minAge = db.Users.IgnoreQueryFilters().Min(x => x.Age);
                Console.WriteLine(minAge);  // 17
            }
        }
    }
}
