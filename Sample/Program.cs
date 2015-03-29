using BLL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            SchoolContext db = new SchoolContext();
            StringBuilder sb = new StringBuilder();
            //db.Database.Log = sql => sb.Append(sql);
           // db.Database.Log = Console.WriteLine;

            //执行数据库初始化
            db.Database.Initialize(false);

            Console.WriteLine("查询所有学生");
            foreach (var stu in db.Students)
            {
                stu.Dump();
            }
            Console.WriteLine("查询学生视图");
            foreach (var vstu in db.VStudents)
            {
                vstu.Dump();
            }
            Console.WriteLine();

            Console.WriteLine("存储过程 单结果集返回");
            var retStulike = db.IsExistStuLike(new ObjectParameter("V_NAME", "jz"));
            foreach (var stu in retStulike)
            {
                stu.Dump();
            }
            Console.WriteLine("存储过程 多结果集返回");
            var all_stusFirst = db.GetAllTableData();
            Console.WriteLine("第一个结果集student");
            foreach (var stu in all_stusFirst)
            {
                stu.Dump();
            }
            var all_courseSecond = all_stusFirst.GetNextResult<Course>();
            Console.WriteLine("第二个结果集course");
            foreach (var c in all_courseSecond)
            {
                c.Dump();
            }
            var all_scThird = all_courseSecond.GetNextResult<SC>();
            //Console.WriteLine("第三个结果集sc   //暂不支持");
            //foreach (var sc in all_scThird)
            //{

            //}
        }
    }
    internal static class Helper
    {
        public static Student Dump(this Student stu)
        {
            Console.WriteLine("{0} {1} {2} {3} ", stu.ID, stu.Name, stu.Age, stu.ClassName);
            return stu;
        }
        public static Course Dump(this Course c)
        {
            Console.WriteLine("{0} {1}", c.ID, c.Name);
            return c;
        }
        public static SC Dump(this SC sc)
        {
            Console.WriteLine("{0} {1} {2}", sc.ID, sc.Student.Name, sc.Course.Name);
            return sc;
        }
        public static VStudent Dump(this VStudent vstu)
        {
            Console.WriteLine("{0} {1}", vstu.ID, vstu.Name);
            return vstu;
        }

        public static T Dump<T>(this T o)
        {
            Console.WriteLine(o.ToString());
            return o;
        }

    }
}
