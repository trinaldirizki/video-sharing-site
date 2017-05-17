using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSharing.Models;

namespace VideoSharing
{
    
    public static class Database
    {
        private const string SessionKey = "VideoSharing.Database.SessionKey";

        private static ISessionFactory _sessionFactory;
        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            config.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            _sessionFactory = config.BuildSessionFactory();
        }
        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }
        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;

            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}