using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.Models;

namespace sgpimafaback.PosVendedor.Domain.Services
{
    public class PosvendedorServices
    {
        public Sgpimafa2Context _DB;

        public PosvendedorServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosvendedorModel> GetAll()
        {
            try
            {
                return _DB.Posvendedors.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosvendedorModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posvendedors.Find(id);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosvendedorModel Create(PosvendedorModel data)
        {
            try
            {
                var prd = _DB.Posvendedors.Where((PosvendedorModel rec) => (rec.TipoIdVendedor == data.TipoIdVendedor) && (rec.IdVendedor == data.IdVendedor));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Posvendedors.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosvendedorModel Update(PosvendedorModel data)
        {
            try
            {
                var newData = _DB.Posvendedors.Where((PosvendedorModel rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posvendedors.Find(data.Id);

                // Retorna el objeto con la información de actualizada
                return oData;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: Interno del servidor o BD ({e.Message})");

            }
        }

        public bool Delete(int id)
        {
            try
            {
                var newData = _DB.Posvendedors.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posvendedors.Remove(newData);
                    _DB.SaveChanges();

                    // Para efectos de auditoria, el user que realiza la operación sale del token del JWT 

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error: Interno del servidor o BD ({e.Message})");

            }
        }

    }
}
