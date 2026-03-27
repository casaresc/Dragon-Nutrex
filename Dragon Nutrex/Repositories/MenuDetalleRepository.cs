using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public class MenuDetalleRepository
    {
        private readonly string _filePath =
            Path.Combine("Data", "menu_detalles.json");

        public List<MenuDetalle> ObtenerPorMenu(Guid menuId)
        {
            var detalles = FileStorage
                .Load<MenuDetalle>(_filePath);

            return detalles
                .Where(d =>
                    d.MenuId == menuId &&
                    d.Activo)
                .ToList();
        }

        public void Agregar(MenuDetalle detalle)
        {
            var detalles = FileStorage
                .Load<MenuDetalle>(_filePath);

            detalle.Id = Guid.NewGuid();

            detalles.Add(detalle);

            FileStorage.Save(
                _filePath,
                detalles);
        }

        public void Eliminar(Guid id)
        {
            var detalles = FileStorage
                .Load<MenuDetalle>(_filePath);

            var detalle = detalles
                .FirstOrDefault(d => d.Id == id);

            if (detalle != null)
            {
                detalle.Activo = false;

                FileStorage.Save(
                    _filePath,
                    detalles);
            }
        }
    }
}
