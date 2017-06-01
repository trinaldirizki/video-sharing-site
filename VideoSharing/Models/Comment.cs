using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharing.Models
{
    public class Comment
    {
        public virtual int comment_id { get; set; }
        public virtual string content { get; set; }
        public virtual DateTime comment_date { get; set; }
        public virtual int like_count { get; set; }
        public virtual int dislike_count { get; set; }
        public virtual Video videos { get; set; }
    }

    public class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Table("comments");

            Id(x => x.comment_id, x => x.Generator(Generators.Identity));
            Property(x => x.content, x => x.NotNullable(true));
            Property(x => x.comment_date, x => x.NotNullable(true));
            Property(x => x.like_count, x => x.NotNullable(false));
            Property(x => x.dislike_count, x => x.NotNullable(false));
            ManyToOne(x => x.videos, x =>
            {
                x.Column("video_id");
            });
        }
    }
}