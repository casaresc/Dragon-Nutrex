using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public class UsuarioRepository
    {
        private string path = Path.Combine("Data", "usuarios.json");

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
