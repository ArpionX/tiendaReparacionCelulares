using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IAuthServices
    {
        Task<Usuario?> Login(string nameUser, string password);
        Task<Usuario?> GetById(int id);
        Task<int> Insert(Usuario user);
        Task<int> Update(Usuario user);
        Task<int> Delete(int id);
    }
    public class AuthServices: IAuthServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> Login(string nameUser, string password)
        {
            Usuario? user;
            string errorCredentials = "Usuario y / o contraseña incorrectos";
            try
            {
                user = await _usuarioRepository.Login(nameUser);

                bool verificatePassword = VerificatePassword(password, user?.ContrasenaHash ?? string.Empty);
                if (user is  null || !verificatePassword) throw new Exception(errorCredentials);

                user.UltimoLogin = DateTime.Now;
                await _usuarioRepository.Update(user); // El método Update del repositorio ahora guarda directamente.

            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }
        public async Task<Usuario?> GetById(int id)
        {
            Usuario? usuario = null;
            try
            {
                usuario = await _usuarioRepository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return usuario;
        }

        public async Task<int> Insert(Usuario usuario)
        {
            int result = 0;
            try
            {
                Usuario? user = await _usuarioRepository.Login(usuario.NombreUsuario);

                if (user is not null) throw new Exception($"{usuario.NombreUsuario} ya se encuentra registrado");

                usuario.ContrasenaHash = EncryptedPassword(usuario.ContrasenaHash);

                await _usuarioRepository.Insert(usuario);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<int> Update(Usuario usuario)
        {
            int result = 0;
            try
            {
                Usuario? existingUser = await _usuarioRepository.Login(usuario.NombreUsuario);
                if (existingUser is not null && existingUser.IdUsuario != usuario.IdUsuario)
                {
                    throw new Exception($"El usuario '{usuario.NombreUsuario}' ya se encuentra registrado por otro ID.");
                }
                Usuario? originalUser = await _usuarioRepository.GetById(usuario.IdUsuario);
                if (originalUser != null && !string.IsNullOrEmpty(usuario.ContrasenaHash) &&
                    !BCrypt.Net.BCrypt.Verify(usuario.ContrasenaHash, originalUser.ContrasenaHash)) // Si la nueva contraseña no es el hash actual
                {
                    usuario.ContrasenaHash = EncryptedPassword(usuario.ContrasenaHash);
                }
                else if (originalUser != null)
                {
                    // Si la contraseña no se cambió, mantenemos el hash existente.
                    usuario.ContrasenaHash = originalUser.ContrasenaHash;
                }
                await _usuarioRepository.Update(usuario);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public async Task<int> Delete(int id)
        {
            int result = 0;
            try
            {
                Usuario? usuario = await _usuarioRepository.GetById(id);

                if (usuario is null) throw new Exception("Usuario no registrado");

                await _usuarioRepository.Delete(usuario);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private string EncryptedPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool VerificatePassword(string plainPassword, string hashPassord)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashPassord);    
        }
    }

}
