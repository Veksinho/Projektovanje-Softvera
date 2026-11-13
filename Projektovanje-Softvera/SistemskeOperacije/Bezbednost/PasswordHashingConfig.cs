using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace SistemskeOperacije.Bezbednost
{
    public static class PasswordHashingConfig
    {
        private const string Key = "PasswordHashing:WorkFactor";
        private const int MinWorkFactor = 4;
        private const int MaxWorkFactor = 31;

        private static readonly Lazy<int> workFactor = new(() =>
        {
            string? value = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build()[Key];

            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"Nedostaje podešavanje '{Key}'.");

            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int wfNumber)
                || wfNumber < MinWorkFactor || wfNumber > MaxWorkFactor)
                throw new InvalidOperationException(
                    $"Podešavanje '{Key}' mora biti ceo broj od {MinWorkFactor} do {MaxWorkFactor}.");

            return wfNumber;
        });

        public static int WorkFactor => workFactor.Value;
    }
}
