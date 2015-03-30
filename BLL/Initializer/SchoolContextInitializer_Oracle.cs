using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Initializer
{
    internal class SchoolContextInitializer_Oracle : SchoolContextInitiallizer
    {
        public override void InitializeDatabase(SchoolContext context)
        {
            //虽然是DropCreateDatabaseAlways，但是视图没有被删除
            context.Database.ExecuteSqlCommand(
                            @"
declare 
      num   number; 
begin 
      select count(1) into num from User_views where View_NAME = 'VSTUDENT' ; 
      if   num=1   then 
          execute immediate 'drop view VSTUDENT'; 
      end   if; 
end; "
                            );

            base.InitializeDatabase(context);

            context.Database.ExecuteSqlCommand(
                @"drop table VSTUDENT"
            );
            context.Database.ExecuteSqlCommand(
                @"create or replace view VStudent
                    as  
                    select ID,NAME from STUDENT");

            int ret = context.Database.ExecuteSqlCommand(
                @"
                create or replace procedure IsExistStuLike
                ( v_name in varchar , cv_1 out sys_refcursor )
                is
                begin 
                  open  cv_1 for
                 select * from STUDENT where NAME like v_name;
                  return ;
                end;
                "
                );

            context.Database.ExecuteSqlCommand(
                @"
                create or replace procedure GetAllTableData
                (
                stu out sys_refcursor,
                c out sys_refcursor,
                sc out sys_refcursor
                )
                is
                begin
                    open stu for select * from student ;
                    open c for select * from course;
                    --open sc for select * from sc;
  --open sc for select sc.* ,stu.name Student_Name,stu.age Student_Age,stu.classname Student_ClassName,c.* from sc inner join student stu on sc.""Student_ID""=stu.id
--inner join course c on sc.""Course_ID""= c.id;
                end;
                ");

            context.Database.ExecuteSqlCommand(
                @"
create or replace function DateTimeToString
(V_Time date)
return varchar
as
begin
return to_char(V_Time, 'dd/mm/yyyy hh24:mi:ss');
end;
");
        }
    }

}
