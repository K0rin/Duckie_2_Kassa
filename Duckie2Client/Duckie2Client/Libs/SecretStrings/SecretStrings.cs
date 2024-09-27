using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Duckie2Client.Libs.SecretStrings;

/// <summary>
/// Contains functionality for working with password hashes.
/// </summary>
public static class SecretStrings
{
    /// <summary>
    /// Returns the hash of the string.
    /// <example>
    /// Usage example for SecureString type:
    /// <code>
    /// var securePassword = new SecureString();
    /// foreach (var c in "YourSecurePassword") securePassword.AppendChar(c);
    /// securePassword.MakeReadOnly();
    /// using (var sha256 = SHA256.Create())
    /// {
    ///     var hashedPassword = SecretStrings.GetHash(securePassword, sha256);
    /// } 
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="input">The string whose hash is to be retrieved.</param>
    /// <param name="hashAlgorithm">The hashing algorithm that will be used to hash the string.</param>
    /// <typeparam name="T">string, SecureString</typeparam>
    public static string GetHash<T>(T input, HashAlgorithm hashAlgorithm)
    {
        // todo: Check for an empty input value.

        var builder = new StringBuilder();
        byte[] bytes;

        switch (input)
        {
            case string inputString:
            {
                bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                break;
            }
            case SecureString secureString:
            {
                var unmanagedString = IntPtr.Zero;
                try
                {
                    // Convert SecureString into a flat string.
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                    var password = Marshal.PtrToStringUni(unmanagedString);
                    bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password!));
                }
                finally
                {
                    // Free unmanaged memory.
                    if (unmanagedString != IntPtr.Zero) Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                }

                break;
            }
            default:
                var t = input?.GetType().Name;
                throw new Exception($"No implementation for type {t}");
        }

        // Convert bytes into a string.
        foreach (var b in bytes) builder.Append(b.ToString("x2"));

        return builder.ToString();
    }

    /// <summary>
    /// Checks if the hash of the string belongs to the requested string.
    /// <example>
    /// Usage example for SecureString type:
    /// <code>
    /// var securePassword = new SecureString();
    /// foreach (var c in "YourSecurePassword") securePassword.AppendChar(c);
    /// securePassword.MakeReadOnly();
    /// using (var sha256 = SHA256.Create())
    /// {
    ///     var hashedPassword = SecretStrings.GetHash(securePassword, sha256);
    ///     var isEquals = SecretStrings.VerifyHash("YourSecurePassword", hashedPassword, sha256);
    /// } 
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="value">Unencrypted string.</param>
    /// <param name="savedHash">String hash.</param>
    /// <param name="hashAlgorithm">The hashing algorithm used to create the hash.</param>
    /// <returns>True - the string matches its hash. False - the string does not match its hash.</returns>
    public static bool VerifyHash<T>(T value, string savedHash, HashAlgorithm hashAlgorithm)
    {
        var enteredHash = GetHash(value, hashAlgorithm);
        var result = enteredHash.Equals(savedHash, StringComparison.OrdinalIgnoreCase);

        return result;
    }
}