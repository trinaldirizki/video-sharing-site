using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;

namespace VideoSharing.Migrations
{
    [Migration(4)]
    public class _004_Users_Like_and_Dislike : Migration
    {
        public override void Down()
        {
            Delete.Table("Users_Dislike_Comment");
            Delete.Table("Users_Like_Comment");
            Delete.Table("Users_Dislike_Video");
            Delete.Table("Users_Like_Video");
        }
        public override void Up()
        {
            Create.Table("Users_Like_Video")
                .WithColumn("like_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("Users","user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);
            Create.Table("Users_Dislike_Video")
                .WithColumn("dislike_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);
            Create.Table("Users_Like_Comment")
                .WithColumn("like_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);
            Create.Table("Users_Dislike_Comment")
                .WithColumn("dislike_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);

        }
    }
}