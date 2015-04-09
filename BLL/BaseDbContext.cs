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
    public class BaseDbContext : DbContext
    {
        public string Schema { get; set; }
        /// <summary>
        /// if not exists then The target context 'BLL.ASPDBContext' is not constructible. Add a default constructor or provide an implementation of IDbContextFactory.
        /// </summary>
        public BaseDbContext(string schema, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Schema = schema.ToUpper();
#if oracle
            Database.SetInitializer(new Initializer.SchoolContextInitializer_Oracle());
#endif
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //一律使用架构名
#if !mysql
            //mysql 不需要设置 应该是连接字符串中已经有了
            modelBuilder.HasDefaultSchema(Schema);
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
        protected IObjectContextAdapter ObjectContextAdapter
        {
            get
            {
                return (IObjectContextAdapter)this;
            }
        }

    }
}
