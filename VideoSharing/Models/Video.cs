using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharing.Models
{
    public class Video
    {
        public virtual int video_id { get; set; }
        public virtual string video_title { get; set; }
        public virtual string video_path { get; set; }
        public virtual User user { get; set; }
        public virtual DateTime video_load_date { get; set; }
        public virtual int like_count { get; set; }
        public virtual int view_count { get; set; }
    }

    public class VideoMap : ClassMapping<Video>
    {
        public VideoMap()
        {
            Table("videos");

            Id(x => x.video_id, x => x.Generator(Generators.Identity));
            ManyToOne(x => x.user, x =>
            {
                x.Column("user_id");
                x.NotNullable(true);

            });
            Property(x => x.video_title, x => x.NotNullable(true));
            Property(x => x.video_path, x => x.NotNullable(true));
            Property(x => x.video_load_date, x => x.NotNullable(true));
            Property(x => x.like_count, x => x.NotNullable(false));
            Property(x => x.view_count, x => x.NotNullable(false));
            
        }
    }
}