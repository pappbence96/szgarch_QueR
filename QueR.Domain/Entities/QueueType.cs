namespace QueR.Domain.Entities
{
    public class QueueType : BaseEntity
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}