using Coling.Repositorio.Contratos;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration configuration;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<TokenData> ConstruirToken(string usuariox, string passwordx)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuario",usuariox),
                new Claim("Rol","Admin"),
                new Claim("estado","Activo")
            };
            var SecretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["LLaveSecreta"]??""));
            var cre=new SigningCredentials(SecretKey,SecurityAlgorithms.HmacSha256);
            var expires=DateTime.UtcNow.AddMinutes(5);
            var tokenSeguirdad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expires, signingCredentials: cre);
     
            TokenData respuestaToken=new TokenData();
            respuestaToken.Token= new JwtSecurityTokenHandler().WriteToken(tokenSeguirdad);
            respuestaToken.expira=expires;
            return  respuestaToken;
        }

        //public Task<string> DesencriptarPassword(string password)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<string> EncriptarPassword(string password)
        {
            string Encriptado = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[]bytes=sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));   
                Encriptado =Convert.ToBase64String(bytes);
            }
            return Encriptado;
        }

        public Task<bool> ValidarToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenData> VerificarCredenciales(string usuariox, string passwordx)
        {
            TokenData tokendevolver= new TokenData();
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "Select  count(isUsuario) from Usuario where nombreuser='" + usuariox + "' and password='" + passEncriptado + "'";
            int Existe=conexion.EjecutarEscalar(consulta);
            if (Existe > 0)
            {
                tokendevolver =await ConstruirToken(usuariox, passwordx);
            }
            return tokendevolver;
        }
    }
}
