using FluentNHibernate.Mapping;

namespace GameServer.Model.Mappings.Players
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerMap : ClassMap<Player.Player>
    {
        /// <summary>
        /// 
        /// </summary>
        public PlayerMap()
        {
            Table("Players");
            Id(x => x.Id);
            Map(x => x.AccountId);
            Map(x => x.Name)
              .Length(32)
              .Not.Nullable();
            Map(x => x.Race)
              .CustomType<int>();
            Map(x => x.Gender)
              .CustomType<int>();
            Map(x => x.Class)
              .CustomType<int>();
            Map(x => x.MapId);
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Z);
            Map(x => x.Heading);
            Map(x => x.Data);
            Map(x => x.Detail);
            Map(x => x.Detail2);
        }
    }
}
