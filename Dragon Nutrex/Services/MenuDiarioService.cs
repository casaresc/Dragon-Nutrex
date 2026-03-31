using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;

namespace Dragon_Nutrex.Services
{
    public class MenuDiarioService
    {
        private readonly IRepository<MenuDiario> _menuRepository;
        private readonly MenuDetalleService _detalleService;

        public MenuDiarioService()
        {
            _menuRepository = new MenuDiarioRepository();
            _detalleService = new MenuDetalleService();
        }

        public List<MenuDiario> ObtenerTodos()
        {
            return _menuRepository.GetAll();
        }

        public void CrearMenu(MenuDiario menu, List<MenuDetalle> detalles)
        {
            var menusExistentes = _menuRepository.GetAll();
            bool yaExisteParaEsteUsuario = menusExistentes.Any(m =>
                m.Fecha.Date == menu.Fecha.Date &&
                m.UsuarioId == menu.UsuarioId);

            if (yaExisteParaEsteUsuario)
            {
                throw new InvalidOperationException($"El usuario ya tiene un plan nutricional para la fecha {menu.Fecha.ToShortDateString()}.");
            }

            _menuRepository.Create(menu);

            if (detalles != null && detalles.Count > 0)
            {
                foreach (var detalle in detalles)
                {
                    detalle.MenuId = menu.Id;
                    _detalleService.GuardarDetalle(detalle);
                }
            }
        }

        public void CrearMenu(MenuDiario menu)
        {
            CrearMenu(menu, new List<MenuDetalle>());
        }

        public void EliminarMenu(Guid id)
        {
            _menuRepository.Delete(id);
        }

        public MenuDiario? ObtenerPorId(Guid id)
        {
            return _menuRepository.GetById(id);
        }

        public List<MenuDiario> ObtenerMenus()
        {
            return ObtenerTodos();
        }
        public void ActualizarMenu(MenuDiario menu)
        {
            var existeDuplicado = _menuRepository.GetAll().Any(m =>
                m.Id != menu.Id &&
                m.UsuarioId == menu.UsuarioId &&
                m.Fecha.Date == menu.Fecha.Date);

            if (existeDuplicado)
            {
                throw new InvalidOperationException($"No se puede cambiar a esta fecha porque el usuario ya tiene un plan nutricional para el {menu.Fecha.ToShortDateString()}.");
            }

            _menuRepository.Update(menu);
        }
    }
}