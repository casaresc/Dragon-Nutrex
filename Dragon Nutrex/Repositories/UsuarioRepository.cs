using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private string path = Path.Combine("Data", "usuarios.json");

        public Usuario? GetById(Guid id)
        {
            var usuarios = GetAll();

            return usuarios.FirstOrDefault(u => u.Id == id);
        }

        public void Delete(Guid id)
        {
            var usuarios = GetAll();

            var usuario = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuarios.Remove(usuario);

            SaveAll(usuarios);
        }
        public void Create(Usuario usuario)
        {
            var usuarios = GetAll();

            usuarios.Add(usuario);

            SaveAll(usuarios);
        }

        public List<Usuario> GetAll()
        {
            return FileStorage.Load<Usuario>(path);
        }

        public void SaveAll(List<Usuario> usuarios)
        {
            FileStorage.Save(path, usuarios);
        }

        public void Update(Usuario usuarioActualizado)
        {
            var usuarios = GetAll();

            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuarioActualizado.Id);

            if (usuarioExistente == null)
                throw new Exception("Usuario no encontrado");

            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Peso = usuarioActualizado.Peso;
            usuarioExistente.Altura = usuarioActualizado.Altura;
            usuarioExistente.Objetivo = usuarioActualizado.Objetivo;
            usuarioExistente.NivelActividad = usuarioActualizado.NivelActividad;
            usuarioExistente.TipoDieta = usuarioActualizado.TipoDieta;

            SaveAll(usuarios);
        }
    }
}
