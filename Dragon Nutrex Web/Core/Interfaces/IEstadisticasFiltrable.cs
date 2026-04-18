using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex_Web.Core.Interfaces
{
    public interface IEstadisticaFiltrable
    {
        void FiltrarPorUsuario(Guid usuarioId);
    }
}
