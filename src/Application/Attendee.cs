namespace TrainingCatalog.Application;

public sealed record CreateAttendeeRequest(
	string? FirstName,
	string? LastName,
	string? Email);

public sealed record Attendee(
	Guid Id,
	Guid TrainingId,
	string FirstName,
	string LastName,
	string Email);