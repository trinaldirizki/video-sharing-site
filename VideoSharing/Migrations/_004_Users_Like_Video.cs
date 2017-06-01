using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;

namespace VideoSharing.Migrations
{
    [Migration(4)]
    public class _004_Users_Like_Video : Migration
    {
        public override void Down()
        {
            Delete.Table("Users_Like_Video");
        }
        public override void Up()
        {
            Create.Table("Users_Like_Video")
                .WithColumn("like_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("Users","user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);
        }
    }
}