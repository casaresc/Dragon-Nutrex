using Dragon_Nutrex.Data;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public class UsuarioRepository
    {
        private string path = "Data/usuarios.json";

        public List<Usuario> GetAll()
        {
            return FileStorage.Load<Usuario>(path);
        }

        public void SaveAll(List<Usuario> usuarios)
        {
            FileStorage.Save(path, usuarios);
        }
    }
}
