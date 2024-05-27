using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosInventarioProducto.Domain.Entities;

namespace sgpimafaback.PosInventarioProducto.Domain.Services
{
    public class PosinventarioproductoServices
    {
        public Sgpimafa2Context _DB;

        public PosinventarioproductoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosinventarioproductoModel> GetAll()
        {
            try
            {
                return _DB.Posinventarioproductos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosinventarioproductoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posinventarioproductos.Find(id);
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

        public PosinventarioproductoModel GetByProductoId(int id)
        {
            try
            {
                var resultado = _DB.Posinventarioproductos.Where((PosinventarioproductoModel rec) => rec.IdCodigo == id);
                if (resultado.Count() >= 0)
                {
                    return resultado.FirstOrDefault();
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

        public PosinventarioproductoModel Create(PosinventarioproductoModel data)
        {
            try
            {
                var prd = _DB.Posinventarioproductos.Where((PosinventarioproductoModel rec) => (rec.IdCodigo == data.IdCodigo));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Posinventarioproductos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosinventarioproductoModel Update(PosinventarioproductoModel data)
        {
            try
            {
                var newData = _DB.Posinventarioproductos.Where((PosinventarioproductoModel rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posinventarioproductos.Find(data.Id);

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
                var newData = _DB.Posinventarioproductos.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posinventarioproductos.Remove(newData);
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
