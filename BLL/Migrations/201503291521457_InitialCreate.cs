namespace BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCHOOL.COMPLEXTYPE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Inner_ID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("SCHOOL.INNERTYPE", t => t.Inner_ID)
                .Index(t => t.Inner_ID);
            
            CreateTable(
                "SCHOOL.INNERTYPE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        A = c.Decimal(nullable: false, precision: 10, scale: 0),
                        B = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "SCHOOL.COURSE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NAME = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "SCHOOL.SC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Course_ID = c.Decimal(precision: 10, scale: 0),
                        Student_ID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("SCHOOL.COURSE", t => t.Course_ID)
                .ForeignKey("SCHOOL.STUDENT", t => t.Student_ID)
                .Index(t => t.Course_ID)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "SCHOOL.STUDENT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NAME = c.String(maxLength: 100),
                        AGE = c.Decimal(precision: 10, scale: 0),
                        CLASSNAME = c.String(maxLength: 100),
                        BIRTHDAY = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.NAME);
            
            //CreateTable(
            //    "SCHOOL.VSTUDENT",
            //    c => new
            //        {
            //            ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
            //            NAME = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SCHOOL.SC", "Student_ID", "SCHOOL.STUDENT");
            DropForeignKey("SCHOOL.SC", "Course_ID", "SCHOOL.COURSE");
            DropForeignKey("SCHOOL.COMPLEXTYPE", "Inner_ID", "SCHOOL.INNERTYPE");
            DropIndex("SCHOOL.STUDENT", new[] { "NAME" });
            DropIndex("SCHOOL.SC", new[] { "Student_ID" });
            DropIndex("SCHOOL.SC", new[] { "Course_ID" });
            DropIndex("SCHOOL.COMPLEXTYPE", new[] { "Inner_ID" });
            DropTable("SCHOOL.VSTUDENT");
            DropTable("SCHOOL.STUDENT");
            DropTable("SCHOOL.SC");
            DropTable("SCHOOL.COURSE");
            DropTable("SCHOOL.INNERTYPE");
            DropTable("SCHOOL.COMPLEXTYPE");
        }
    }
}
