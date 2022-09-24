namespace BuildingBlocks.Abstractions.CQRS.Query;

public interface IPageRequest
{
    public IList<string>? Includes { get; init; }
    public IList<FilterModel>? Filters { get; init; }
    public IList<string>? Sorts { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}