using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using CodeFirstStoreFunctions;

namespace BLL
{
    public class SchoolContext : BaseDbContext
    {
        public SchoolContext()
            : base("School", "name=SchoolConnectionString")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<SC> SCs { get; set; }

        public virtual DbSet<VStudent> VStudents { get; set; }

        public virtual DbSet<BLL.Model.ComplexType> ComplexTypeTest { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //以存储过程 函数支持
            modelBuilder.Conventions.Add(new FunctionsConvention<SchoolContext>("SCHOOL"));
            modelBuilder.Conventions.Add(new FunctionsConvention("SCHOOL", typeof(Functions)));

            base.OnModelCreating(modelBuilder);
        }

        [DbFunction("SchoolContext", "ISEXISTSTULIKE")]
        [DbFunctionDetails(ResultTypes = new Type[] { typeof(Student) })]
        public virtual ObjectResult<Student> IsExistStuLike([ParameterType(typeof(string))]ObjectParameter name)
        {
            return ((IObjectContextAdapter)this).ObjectContext.
                ExecuteFunction<Student>("ISEXISTSTULIKE", name);
        }

        [DbFunction("SchoolContext", "GETALLTABLEDATA")]
        [DbFunctionDetails(ResultTypes = new[] { typeof(Student), typeof(Course)
           // , typeof(SC)
        })]
        public virtual ObjectResult<Student> GetAllTableData()
        {
            return ObjectContextAdapter.ObjectContext.ExecuteFunction<Student>("GETALLTABLEDATA");
        }


    }
    public class Functions
    {
        [DbFunction("CodeFirstDatabaseSchema", "DATETIMETOSTRING")]
        public static string DateTimeToString(DateTime time)
        {
            throw new NotSupportedException();
        }
    }
}
