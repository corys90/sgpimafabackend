using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.Models;

namespace sgpimafaback.PosTipoProducto.Domain.Services
{
    public class PostipoproductoServices
    {
        public Sgpimafa2Context _DB;

        public PostipoproductoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PostipoproductoModel> GetAll()
        {
            try
            {
                return _DB.Postipoproductos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoproductoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Postipoproductos.Find(id);
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

        public PostipoproductoModel Create(PostipoproductoModel data)
        {
            try
            {
                var prd = _DB.Postipoproductos.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Postipoproductos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoproductoModel Update(PostipoproductoModel data)
        {
            try
            {
                var newData = _DB.Postipoproductos.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Postipoproductos.Find(data.Id);

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
                var newData = _DB.Postipoproductos.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Postipoproductos.Remove(newData);
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
