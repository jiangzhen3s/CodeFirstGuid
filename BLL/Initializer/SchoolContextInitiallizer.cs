using BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Initializer
{
    internal class SchoolContextInitiallizer : DropCreateDatabaseAlways<SchoolContext>
    {
        protected override void Seed(SchoolContext ctx)
        {
            var fali = new Student
            {
                Name = "ALFKI",
                Age = 10,
                Birthday = DateTime.Now
            };
            var jz = new Student
            {
                Name = "jz",
                Age = 20,
                Birthday = DateTime.Now.AddDays(1)
            };

            ctx.Students.Add(fali);
            ctx.Students.Add(jz);

            var csharp = new Course
            {
                Name = "C#"
            };
            var fsharp = new Course
            {
                Name = "F#"
            };
            ctx.Courses.Add(csharp);
            ctx.Courses.Add(fsharp);

            ctx.SaveChanges();

            ctx.SCs.Add(new SC
            {
                Student = jz,
                Course = csharp
            });
            ctx.SCs.Add(new SC
            {
                Student = jz,
                Course = fsharp
            });
            ctx.SCs.Add(new SC
            {
                Student = fali,
                Course = csharp
            });
        }

    }
}
