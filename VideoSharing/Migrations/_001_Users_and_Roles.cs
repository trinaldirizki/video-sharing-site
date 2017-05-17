using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FluentMigrator;

namespace VideoSharing.Migrations
{
    [Migration(1)]
    public class _001_Users_and_Roles : Migration
    {
        public override void Down()
        {
            Delete.Table("role_users");
            Delete.Table("roles");
            Delete.Table("users");
        }
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("user_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(25)
                .WithColumn("surname").AsString(25)
                .WithColumn("nick_name").AsString(30)
                .WithColumn("email").AsString(256)
                .WithColumn("password").AsString(128);

            Create.Table("Roles")
                .WithColumn("roles_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("roles_string").AsString(20);

            Create.Table("Role_Users")
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("Roles", "roles_id").OnDelete(Rule.Cascade);
            }
    }
}