using Auth.Application.DTOs.RequestModel;
using FluentAssertions;
using ModularMonolith.Template.Application.Tests.Common;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Users.Application.DTOs;

namespace ModularMonolith.Template.Application.Tests.IntegrationTests
{
    [Collection("SharedApiFactory")]
    public class AuthApiIntegrationTests
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
        public async Task Full_Auth_Flow_Should_Succeed()
        {
            // 1. Register            
            registerDto.Email = TestDataGenerator.GenerateRandomEmail();

            HttpResponseMessage? registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerDto);
            registerResponse.EnsureSuccessStatusCode();

            // 2. Login
            LoginDto loginDto = new LoginDto
            {
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            HttpResponseMessage? loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            loginResponse.EnsureSuccessStatusCode();

            string loginJson = await loginResponse.Content.ReadAsStringAsync();
            JsonObject? root = JsonNode.Parse(loginJson)?.AsObject();
            string? refreshToken = root?["data"]?["refreshToken"]?.GetValue<string>();

            // 3. Refresh
            RefreshTokenDto refreshTokenDto = new RefreshTokenDto
            {
                RefreshToken = refreshToken ?? string.Empty
            };

            HttpResponseMessage? refreshResponse = await _client.PostAsJsonAsync("/api/auth/refresh", refreshTokenDto);
            refreshResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
