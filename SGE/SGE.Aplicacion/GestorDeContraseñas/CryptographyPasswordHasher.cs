using System;
using System.Security.Cryptography;
using System.Text;

namespace SGE.Aplicacion;

public class CryptographyPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new AutorizacionException("La contraseña no puede estar vacía.");

        // Convertimos el string de la contraseña a un arreglo de bytes (Input)
        byte[] inputBytes = Encoding.UTF8.GetBytes(password);
        
        // Computamos el hash usando SHA-256 de forma eficiente
        byte[] hashBytes = SHA256.HashData(inputBytes);

        // Convertimos los 32 bytes del hash a un string Hexadecimal (Efecto avalancha/unidireccional)
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            sb.Append(b.ToString("x2")); // Formato hexadecimal de 2 caracteres por byte
        }

        return sb.ToString();
    }
public bool VerifyPassword(string password, string hashedPassword)
{
    // Si alguna de las dos variables es nula o vacía, no coinciden.
    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
    {
        return false; 
    }

    // Volvemos a calcular el hash de la contraseña ingresada
    string computedHash = HashPassword(password);

    // Comparamos de forma segura si los hashes coinciden
    return string.Equals(computedHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
}
}
