namespace MassTransit.Play.Subscriber.Domain
{
    using System;

    [Serializable]
    public class AuditEvent
    {
        public virtual long Id { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime EventDate { get; set; }
    }
}