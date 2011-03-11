namespace MassTransit.Play.Subscriber.Domain
{
    using FluentNHibernate.Mapping;

    public class AuditEventMap : ClassMap<AuditEvent>
    {
        public AuditEventMap()
        {
            Schema("dbo");
            Table("AuditLog");
            Id(x => x.Id).GeneratedBy.HiLo("IdentifierSeeds", "NextKey", "20", "TableName='AuditLog'");
            Map(x => x.Description)
                .Not.Nullable();
            Map(x => x.EventDate)
                .CustomType("DateTime2")
                .Not.Nullable();
        }
    }
}