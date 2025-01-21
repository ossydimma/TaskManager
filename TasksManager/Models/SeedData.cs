// using Microsoft.EntityFrameworkCore;
// using TasksManager.Data;

// namespace TasksManager.Models
// {
//     public class SeedData
//     {
//         public static void Initialize(IServiceProvider serviceProvider)
//         {
//             using (var context = new TasksManagerDbContext(
//                 serviceProvider.GetRequiredService<DbContextOptions<TasksManagerDbContext>>()))
//             {
//                 if (context.Users.Any())
//                 {
//                     return;   // DB has been seeded
//                 }

//                 // context.Users.AddRange(
//                 //     new User
//                 //     {

//                 //     }
//                 // )
                   
//             }
//     }
// }