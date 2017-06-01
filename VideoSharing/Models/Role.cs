using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharing.Models
{
    public class Role
    {
        public virtual int roles_id { get; set; }
        public virtual string Name { get; set; }
    }


    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.roles_id, x => x.Generator(Generators.Identity));
            Property(x => x.Name, x => {
                x.Column("roles_string");
                x.NotNullable(true);

            });
        }
    }
}