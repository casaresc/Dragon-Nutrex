using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Common.DataConfig;
using Dragon_Nutrex_Web.Utils.FileStorage;

namespace Dragon_Nutrex_Web.Infrastructure.Repositories
{
    public class ConsumoDiarioRepository : IRepository<ConsumoDiario>
    {
        private readonly string _filePath = DataConfig.GetStoragePath("consumo_diario.json");
        public List<ConsumoDiario> GetAll()
        {
            return FileStorage.Load<ConsumoDiario>(_filePath)
                              .Where(r => r.Activo)
                              .ToList();
        }

        public ConsumoDiario? GetById(Guid id)
        {
            return GetAll().FirstOrDefault(r => r.Id == id);
        }

        public void Create(ConsumoDiario entity)
        {
            var registros = FileStorage.Load<ConsumoDiario>(_filePath);

            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            entity.Activo = true;

            registros.Add(entity);
            FileStorage.Save(_filePath, registros);
        }

        public void Update(ConsumoDiario entity)
        {
            var registros = FileStorage.Load<ConsumoDiario>(_filePath);
            var index = registros.FindIndex(r => r.Id == entity.Id);
            if (index != -1)
            {
                registros[index] = entity;
                FileStorage.Save(_filePath, registros);
            }
        }

        public void Delete(Guid id)
        {
            var registros = FileStorage.Load<ConsumoDiario>(_filePath);
            var registro = registros.FirstOrDefault(r => r.Id == id);
            if (registro != null)
            {
                registro.Activo = false;
                FileStorage.Save(_filePath, registros);
            }
        }

        public List<ConsumoDiario> GetByRange(DateTime startDate, DateTime endDate)
        {
            return GetAll()
                   .Where(r => r.Fecha.Date >= startDate.Date && r.Fecha.Date <= endDate.Date)
                   .ToList();
        }
        public List<ConsumoDiario> GetByDate(DateTime date)
        {
            return GetAll()
                   .Where(r => r.Fecha.Date == date.Date)
                   .ToList();
        }

    }
}