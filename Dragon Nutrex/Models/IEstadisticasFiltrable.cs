using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Models
{
    public interface IEstadisticaFiltrable
    {
        void FiltrarPorUsuario(Guid usuarioId);
    }
}
