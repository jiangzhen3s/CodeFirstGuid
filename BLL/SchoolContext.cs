//#define sqlserver
#define oracle
//#define mysql
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
#if mysql
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
#endif
    public class SchoolContext : DbContext
    {
        /// <summary>
        /// if not exists then The target context 'BLL.ASPDBContext' is not constructible. Add a default constructor or provide an implementation of IDbContextFactory.
        /// </summary>
        public SchoolContext()
#if sqlserver
            : base("name=SchoolSqlserver")
#endif
#if oracle
            : base("name=SchoolOracle")
#endif
#if mysql
            : base("name=SchoolMySql")
#endif
        {
            Database.SetInitializer(new Initializer.SchoolContextInitializer_Oracle());
            // Database.SetInitializer<SchoolContext>(null);
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<SC> SCs { get; set; }

        public virtual DbSet<VStudent> VStudents { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //以存储过程 函数支持
            modelBuilder.Conventions.Add(new FunctionsConvention<SchoolContext>("SCHOOL"));
            modelBuilder.Conventions.Add(new FunctionsConvention("SCHOOL", typeof(Functions)));

            //直接跑这行不能开启 2015-03-29
            //目前code first 不能生成视图，视图需要通过原生sql创建
            //因此migration的时候取消视图的创建（默认会简表的）
            //  modelBuilder.Ignore<VStudent>();

            //一律使用架构名
#if !mysql
            //mysql 不需要设置 应该是连接字符串中已经有了
            modelBuilder.HasDefaultSchema("SCHOOL");
#endif
#if oracle
            //所有列明大写
            modelBuilder.Properties()
                .Configure(config => config.HasColumnName(config.ClrPropertyInfo.Name.ToUpper()));
            //所有表名大写
            modelBuilder.Types()
                .Configure(config => config.ToTable(config.ClrType.Name.ToUpper()));
#endif
#if sqlserver || mysql
            modelBuilder.Properties()
                .Configure(config => config.HasColumnName(config.ClrPropertyInfo.Name));
            modelBuilder.Types()
                .Configure(config => config.ToTable(config.ClrType.Name));
#endif
            base.OnModelCreating(modelBuilder);
        }
        private IObjectContextAdapter ObjectContextAdapter
        {
            get
            {
                return (IObjectContextAdapter)this;
            }
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
