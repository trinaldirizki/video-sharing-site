using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;
using System.Data;

namespace VideoSharing.Migrations
{
    [Migration(2)]
    public class _002_Video_and_Comment : Migration
    {
        public override void Down()
        {
            Delete.Table("Videos_Comments");
            Delete.Table("Comments");
            Delete.Table("Videos");
        }

        public override void Up()
        {
            Create.Table("Videos")
                .WithColumn("video_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("video_title").AsString(128)
                .WithColumn("video_path").AsString()
                .WithColumn("video_load_date").AsDate()
                .WithColumn("like_count").AsInt32()
                .WithColumn("dislike_count").AsInt32()
                .WithColumn("view_count").AsInt32();
            Create.Table("Comments")
                .WithColumn("comment_id").AsInt32().PrimaryKey().Identity()
                .WithColumn("content").AsString(128)
                .WithColumn("comment_date").AsDate()
                .WithColumn("like_count").AsInt32()
                .WithColumn("disllike_count").AsInt32();
            Create.Table("Videos_Comments")
                .WithColumn("video_id").AsInt32().ForeignKey("Videos", "video_id").OnDelete(Rule.Cascade)
                .WithColumn("comment_id").AsInt32().ForeignKey("Comments", "comment_id").OnDelete(Rule.Cascade);
        }


    }
}