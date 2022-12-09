﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Data;

namespace ShoeStore

{
    ///<summary>
    ///Это класс, реализующий создание бирер токена по моей логике
    ///</summary>
    public class JwtAuthenticationManager
    {
        private readonly string? key;

        ///<summary>
        ///Конструктор, принимающий ключ для создания уникального токена
        ///</summary>
        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        ///<summary>
        ///Создает и возвращает бирер токен
        ///</summary>
        public string? Authenticate(string? userName, ApplicationDbContext _db)
        {
            if (_db.Users != null && !_db.Users.Any(u=>u.Name == userName))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}