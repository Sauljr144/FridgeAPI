using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FridgeAPI.Services;
using FridgeAPI.Services.Context;
using FridgeAPI.Models;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly DataContext _context;

    public AuthController(JwtService jwtService, DataContext context)
    {
        _jwtService = jwtService;
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // Generate a random salt
        byte[] salt = GenerateSalt();

        // Concatenate salt with password and hash the result
        byte[] hashedPassword = HashPassword(Encoding.UTF8.GetBytes(request.Password), salt);

        // Convert byte arrays to base64 strings for storage
        string saltString = Convert.ToBase64String(salt);
        string hashedPasswordString = Convert.ToBase64String(hashedPassword);

        // Store username, salt, and hashed password in the database
        var user = new UserModel
        {
            Username = request.Username,
            Salt = saltString,
            HashedPassword = hashedPasswordString
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null)
        {
            return Unauthorized(); // User not found
        }

        // Convert stored salt and hashed password from base64 strings to byte arrays
        byte[] salt = Convert.FromBase64String(user.Salt);
        byte[] storedHashedPassword = Convert.FromBase64String(user.HashedPassword);

        // Concatenate salt with provided password and hash the result
        byte[] hashedPassword = HashPassword(Encoding.UTF8.GetBytes(request.Password), salt);

        // Compare the stored hashed password with the computed hashed password
        if (!hashedPassword.SequenceEqual(storedHashedPassword))
        {
            return Unauthorized(); // Invalid password
        }

        // Authentication successful
        var token = _jwtService.GenerateToken(request.Username);
        return Ok(new { token });
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private byte[] HashPassword(byte[] password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] combinedBytes = password.Concat(salt).ToArray();
            return sha256.ComputeHash(combinedBytes);
        }
    }
}

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
