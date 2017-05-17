using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;

namespace VideoSharing.Migrations
{
    [Migration(3)]
    public class _003_Users_Video_and_Commnet_Relation : Migration
    {
        public override void Down()
        {
            Delete.Table("Users_Comments");
            Delete.Table("Users_Videos");
        }

        public override void Up()
        {
            Create.Table("Users_Videos")
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade);
            Create.Table("Users_Comments")
                .WithColumn("user_id").AsInt32().ForeignKey("Users", "user_id").OnDelete(Rule.Cascade)
                .WithColumn("comment_id").AsInt32().ForeignKey("Comments", "comment_id").OnDelete(Rule.Cascade);
        }
    }
}