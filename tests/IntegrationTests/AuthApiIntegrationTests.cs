using Auth.Application.DTOs.RequestModel;
using FluentAssertions;
using ModularMonolith.Template.Application.Tests.Common;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Users.Application.DTOs;

namespace ModularMonolith.Template.Application.Tests.IntegrationTests
{
    public class AuthApiIntegrationTests : IClassFixture<ModularApiFactory>
    {
        private readonly ModularApiFactory _factory;
        private readonly HttpClient _client;
        
        public AuthApiIntegrationTests(ModularApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        internal readonly UserDto testUser = TestUsers.TestUser;

        internal readonly RegisterDto registerDto = TestUsers.RegisterDto;

        [Fact]
        public async Task POST_Register()
        {
            registerDto.Email = TestDataGenerator.GenerateRandomEmail();
            HttpResponseMessage? response = await _client.PostAsJsonAsync("/api/auth/register", registerDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_Login()
        {
            LoginDto loginDto = new LoginDto
            {
                Email = testUser.Email,
                Password = testUser.Password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_Refresh()
        {
            LoginDto loginDto = new LoginDto
            {
                Email = testUser.Email,
                Password = testUser.Password
            };

            HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            loginResponse.EnsureSuccessStatusCode();

            string json = await loginResponse.Content.ReadAsStringAsync();
            JsonObject? root = JsonNode.Parse(json)?.AsObject();
            string? token = root?["data"]?["token"]?.GetValue<string>();
            string? refreshToken = root?["data"]?["refreshToken"]?.GetValue<string>();

            RefreshTokenDto refreshTokenDto = new RefreshTokenDto { 
                RefreshToken = refreshToken ?? string.Empty,
            };

            HttpResponseMessage refreshResponse = await _client.PostAsJsonAsync("/api/auth/refresh", refreshTokenDto);

            refreshResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
