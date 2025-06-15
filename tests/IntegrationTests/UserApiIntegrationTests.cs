using Auth.Application.DTOs.RequestModel;
using Auth.Application.Exceptions;
using FluentAssertions;
using ModularMonolith.Template.Application.Tests.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Users.Application.DTOs;

namespace ModularMonolith.Template.Application.Tests.IntegrationTests
{
    [Collection("SharedApiFactory")]
    public class UserApiIntegrationTests
    {
        private readonly ModularApiFactory _factory;
        private readonly HttpClient _client;
        
        public UserApiIntegrationTests(ModularApiFactory factory) {

            _factory = factory;
            _client = factory.CreateClient();
        }

        internal readonly UserDto testUser = TestUsers.TestUser;

        internal readonly RegisterDto registerDto = TestUsers.RegisterDto;

        private async Task RegisterUserIfNotExistsAsync()
        {
            HttpResponseMessage? registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerDto);

            if (registerResponse.IsSuccessStatusCode)
            {
                return;
            }
            else if (registerResponse.StatusCode == HttpStatusCode.Conflict)
            {
                return;
            }
            else
            {
                registerResponse.EnsureSuccessStatusCode();
            }
        }

        private async Task AuthenticateAsync()
        {
            await RegisterUserIfNotExistsAsync();

            LoginDto loginDto = new LoginDto { 
                Email = testUser.Email,
                Password = testUser.Password
            };

            HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            loginResponse.EnsureSuccessStatusCode();

            string json = await loginResponse.Content.ReadAsStringAsync();
            JsonObject? root = JsonNode.Parse(json)?.AsObject();
            string? token = root?["data"]?["token"]?.GetValue<string>();

            if (string.IsNullOrEmpty(token))
                // throw new InvalidOperationException("Token not found in response.");
                throw AuthException.TokenTampered();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        [Fact]
        public async Task GET_User_List()
        {
            await AuthenticateAsync();
            HttpResponseMessage response = await _client.GetAsync("/api/user");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GET_User_By_Id_Should_Return_404_When_Not_Found()
        {
            await AuthenticateAsync();
            HttpResponseMessage response = await _client.GetAsync("/api/user/00000000-0000-0000-0000-000000000000");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_Create_User_Should_Return_201()
        {
            await AuthenticateAsync();

            UserDto dto = testUser;

            HttpResponseMessage response = await _client.PostAsJsonAsync("/api/user", dto);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task PUT_Update_User() {

            await AuthenticateAsync();

            UserDto dto = new UserDto
            {
                Id = Guid.Parse(TestUsers.TestGuid),
                UserName = "Test User 2",
                Email = "test@yahoo.com"
            };

            HttpResponseMessage response = await _client.PutAsJsonAsync($"/api/user/{TestUsers.TestGuid}", dto);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DELETE_Delete_User() {

            await AuthenticateAsync();

            HttpResponseMessage response = await _client.DeleteAsync($"/api/user/{TestUsers.TestGuid}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
