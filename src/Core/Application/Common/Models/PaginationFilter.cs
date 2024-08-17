namespace OpsManagerAPI.Application.Common.Models;

public class PaginationFilter // : BaseFilter
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; } = 20;
}