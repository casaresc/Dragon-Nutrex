namespace Dragon_Nutrex_Web.Core.Models
{
    public class ResumenRango
    {
        public decimal TotalCalorias { get; set; }

        public decimal TotalCarbohidratos { get; set; }

        public decimal TotalProteinas { get; set; }

        public decimal TotalGrasas { get; set; }

        public decimal PromedioCalorias { get; set; }

        public decimal PromedioCarbohidratos { get; set; }
        public decimal PromedioProteinas { get; set; }

        public decimal PromedioGrasas { get; set; }

        public int DiasConRegistros { get; set; }
    }
}