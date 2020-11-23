namespace QueR.Domain.Services
{
    public interface IUserAccessor
    {
        int UserId { get; }
        int? CompanyId { get; }
        int? WorksiteId { get; }
    }
}