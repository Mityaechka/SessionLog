namespace SessionLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayType = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        IsPresented = c.Boolean(nullable: false),
                        Mark = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.DayId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "StudentId", "dbo.Students");
            DropForeignKey("dbo.SubjectStudents", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Results", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Days", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Results", "DayId", "dbo.Days");
            DropIndex("dbo.SubjectStudents", new[] { "StudentId" });
            DropIndex("dbo.SubjectStudents", new[] { "SubjectId" });
            DropIndex("dbo.Results", new[] { "SubjectId" });
            DropIndex("dbo.Results", new[] { "StudentId" });
            DropIndex("dbo.Results", new[] { "DayId" });
            DropIndex("dbo.Days", new[] { "Subject_Id" });
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectStudents");
            DropTable("dbo.Students");
            DropTable("dbo.Results");
            DropTable("dbo.Days");
        }
    }
}
