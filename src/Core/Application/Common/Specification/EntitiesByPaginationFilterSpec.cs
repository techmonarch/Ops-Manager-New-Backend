namespace OpsManagerAPI.Application.Common.Specification;
public class ApplicationUserByPaginationFilterSpec<T> : Specification<T>
{
    public ApplicationUserByPaginationFilterSpec(PaginationFilter filter)
        => Query.PaginateBy(filter);
}

public class EntitiesByPaginationFilterSpec<T> : Specification<T>
    where T : AuditableEntity
{
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        => Query.PaginateBy(filter).OrderByDescending(x => x.CreatedOn);
}