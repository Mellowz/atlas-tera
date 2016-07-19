using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Dialect;

namespace GameServer.Utility.Helpers
{
    public class NHibernateHelper
    {
        public static ISessionFactory CreateMssqlSessionFactory()
        {
            return Fluently.Configure()
              .Database(
                MsSqlConfiguration
                .MsSql2012
                .ConnectionString("Server=(local);initial catalog=ATLAS_DB;Integrated Security=True")
                .Dialect<MsSql2012Dialect>()
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<GameServer>())
              .BuildSessionFactory();
        }
    }
}
