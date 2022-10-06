namespace BuildingBlocks.Abstractions.Domain;

/// <summary>
/// Super type of all Store.Services.Identity types with generic id.
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IIdentity<out TId>
{
    /// <summary>
    /// Gets the generic identifier.
    /// </summary>
    public TId Value { get; }
}