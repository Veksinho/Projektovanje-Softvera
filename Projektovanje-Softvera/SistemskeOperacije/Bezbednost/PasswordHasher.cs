using BCrypt.Net;
using System.Security.Cryptography;

namespace SistemskeOperacije.Bezbednost
{
    public static class PasswordHasher
    {
        private static readonly Lazy<string> dummyHash = new(() =>
            BCrypt.Net.BCrypt.HashPassword(
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)),
                PasswordHashingConfig.WorkFactor));

        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Šifra za heširanje nije prosleđena.", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password, PasswordHashingConfig.WorkFactor);
        }

        public static bool Verify(string? password, string? hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                BCrypt.Net.BCrypt.Verify(string.IsNullOrEmpty(password) ? "-" : password, dummyHash.Value);
                return false;
            }

            if (string.IsNullOrEmpty(password))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch (SaltParseException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
