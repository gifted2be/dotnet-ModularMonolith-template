using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolith.Template.Application.Tests.Common
{
    public static class TestDataGenerator
    {
        private static readonly Random _random = new();

        public static string GenerateRandomEmail(string domain = "test.com")
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string user = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[_random.Next(s.Length)]).ToArray());

            return $"{user}@{domain}";
        }
    }
}
