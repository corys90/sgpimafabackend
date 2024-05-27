using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.InventarioProducto.Domain.Entities;

namespace sgpimafaback.InventarioProducto.Domain.Services
{
    public class inventarioproductoServices
    {
        public Sgpimafa2Context _DB;

        public inventarioproductoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<inventarioproductoModel> GetAll()
        {
            try
            {
                return _DB.inventarioproductos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public inventarioproductoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.inventarioproductos.Find(id);
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

        public inventarioproductoModel GetByProductoId(int id)
        {
            try
            {
                var resultado = _DB.inventarioproductos.Where((inventarioproductoModel rec) => rec.IdCodigo == id);
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

        public inventarioproductoModel Create(inventarioproductoModel data)
        {
            try
            {
                var prd = _DB.inventarioproductos.Where((inventarioproductoModel rec) => (rec.IdCodigo == data.IdCodigo));
                if (prd.Any())
                {
                    return null;
                }

                _DB.inventarioproductos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public inventarioproductoModel Update(inventarioproductoModel data)
        {
            try
            {
                var newData = _DB.inventarioproductos.Where((inventarioproductoModel rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.inventarioproductos.Find(data.Id);

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
                var newData = _DB.inventarioproductos.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.inventarioproductos.Remove(newData);
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
