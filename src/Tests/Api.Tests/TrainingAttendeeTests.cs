using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TrainingCatalog.Application;

namespace TrainingCatalog.Api.Tests;

public sealed class TrainingAttendeeTests
{
	[Fact]
	public async Task CreatesAttendeeAndReturnsLocation()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var training = await CreateTrainingAsync(client, "2026-10-01");
		var request = new CreateAttendeeRequest(" Ana ", " Silva ", " ana@example.com ");

		var response = await client.PostAsJsonAsync($"/api/trainings/{training.Id}/attendees", request);

		Assert.Equal(HttpStatusCode.Created, response.StatusCode);

		var attendee = await response.Content.ReadFromJsonAsync<Attendee>();
		Assert.NotNull(attendee);
		Assert.NotEqual(Guid.Empty, attendee!.Id);
		Assert.Equal($"/api/trainings/{training.Id}/attendees/{attendee.Id}", response.Headers.Location?.ToString());
		Assert.Equal(training.Id, attendee.TrainingId);
		Assert.Equal("Ana", attendee.FirstName);
		Assert.Equal("Silva", attendee.LastName);
		Assert.Equal("ana@example.com", attendee.Email);
	}

	[Theory]
	[InlineData(null, "Silva", "ana@example.com", "firstName")]
	[InlineData("Ana", null, "ana@example.com", "lastName")]
	[InlineData("Ana", "Silva", null, "email")]
	[InlineData("Ana", "Silva", "invalid", "email")]
	public async Task ReturnsBadRequestForInvalidAttendeeData(
		string? firstName,
		string? lastName,
		string? email,
		string fieldName)
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var training = await CreateTrainingAsync(client, "2026-10-02");
		var request = new CreateAttendeeRequest(firstName, lastName, email);

		var response = await client.PostAsJsonAsync($"/api/trainings/{training.Id}/attendees", request);

		Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		using var error = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
		Assert.True(error.RootElement.GetProperty("errors").TryGetProperty(fieldName, out _));
	}

	[Fact]
	public async Task ReturnsNotFoundWhenCreatingAttendeeForUnknownTraining()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();

		var response = await client.PostAsJsonAsync(
			$"/api/trainings/{Guid.NewGuid()}/attendees",
			new CreateAttendeeRequest("Ana", "Silva", "ana@example.com"));

		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	[Fact]
	public async Task ReturnsEmptyCollectionForTrainingWithoutAttendees()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var training = await CreateTrainingAsync(client, "2026-10-03");

		var response = await client.GetAsync($"/api/trainings/{training.Id}/attendees");

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		var attendees = await response.Content.ReadFromJsonAsync<Attendee[]>();
		Assert.NotNull(attendees);
		Assert.Empty(attendees!);
	}

	[Fact]
	public async Task ListsOnlyAttendeesFromRequestedTrainingInDescendingIdOrder()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var firstTraining = await CreateTrainingAsync(client, "2026-10-04");
		var secondTraining = await CreateTrainingAsync(client, "2026-10-05");

		await CreateAttendeeAsync(client, firstTraining.Id, "ana@example.com");
		await CreateAttendeeAsync(client, firstTraining.Id, "bia@example.com");
		await CreateAttendeeAsync(client, secondTraining.Id, "caio@example.com");

		var response = await client.GetAsync($"/api/trainings/{firstTraining.Id}/attendees");

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		var attendees = await response.Content.ReadFromJsonAsync<Attendee[]>();
		Assert.NotNull(attendees);
		Assert.Equal(2, attendees!.Length);
		Assert.All(attendees, attendee => Assert.Equal(firstTraining.Id, attendee.TrainingId));
		Assert.Equal(attendees.OrderByDescending(attendee => attendee.Id), attendees);
	}

	[Fact]
	public async Task RejectsDuplicateEmailInSameTrainingAfterNormalization()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var training = await CreateTrainingAsync(client, "2026-10-06");

		var firstResponse = await CreateAttendeeAsync(client, training.Id, "ana@example.com");
		var secondResponse = await client.PostAsJsonAsync(
			$"/api/trainings/{training.Id}/attendees",
			new CreateAttendeeRequest("Outra", "Pessoa", "  ANA@EXAMPLE.COM  "));

		Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);
		using var error = JsonDocument.Parse(await secondResponse.Content.ReadAsStringAsync());
		Assert.True(error.RootElement.GetProperty("errors").TryGetProperty("email", out _));

		var listingResponse = await client.GetAsync($"/api/trainings/{training.Id}/attendees");
		var attendees = await listingResponse.Content.ReadFromJsonAsync<Attendee[]>();
		Assert.Single(attendees!);
	}

	[Fact]
	public async Task AllowsSameEmailInDifferentTrainings()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();
		var firstTraining = await CreateTrainingAsync(client, "2026-10-07");
		var secondTraining = await CreateTrainingAsync(client, "2026-10-08");
		var request = new CreateAttendeeRequest("Ana", "Silva", "ana@example.com");

		var firstResponse = await client.PostAsJsonAsync($"/api/trainings/{firstTraining.Id}/attendees", request);
		var secondResponse = await client.PostAsJsonAsync($"/api/trainings/{secondTraining.Id}/attendees", request);

		Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);
		Assert.Equal(HttpStatusCode.Created, secondResponse.StatusCode);
	}

	[Fact]
	public async Task ReturnsNotFoundWhenListingUnknownTraining()
	{
		using var factory = new TrainingCatalogApiFactory();
		using var client = factory.CreateClient();

		var response = await client.GetAsync($"/api/trainings/{Guid.NewGuid()}/attendees");

		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
	}

	private static async Task<Training> CreateTrainingAsync(HttpClient client, string startDate)
	{
		var response = await client.PostAsJsonAsync(
			"/api/trainings",
			new CreateTrainingRequest("Treinamento", "Descrição", startDate, 8));
		response.EnsureSuccessStatusCode();
		return (await response.Content.ReadFromJsonAsync<Training>())!;
	}

	private static Task<HttpResponseMessage> CreateAttendeeAsync(HttpClient client, Guid trainingId, string email) =>
		client.PostAsJsonAsync(
			$"/api/trainings/{trainingId}/attendees",
			new CreateAttendeeRequest("Ana", "Silva", email));
}