namespace TrainingCatalog.Application;

public sealed record CreateTrainingRequest(
    string? Title,
    string? Description,
    string? StartDate,
    int DurationHours);

public sealed record Training(
    Guid Id,
    string Title,
    string Description,
    DateOnly StartDate,
    int DurationHours);