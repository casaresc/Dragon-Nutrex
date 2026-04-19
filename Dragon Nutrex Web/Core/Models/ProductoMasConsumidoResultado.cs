namespace Dragon_Nutrex_Web.Core.Models
{
    public class ProductoMasConsumidoResultado
    {
        public Guid ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal TotalPorciones { get; set; }
        public int TotalRegistros { get; set; }
    }
}