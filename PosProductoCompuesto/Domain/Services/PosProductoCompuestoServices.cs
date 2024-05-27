using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosProductoCompuesto.Domain.Entities;


namespace sgpimafaback.PosProductoCompuestoServices.Domain.Services
{
    public class posproductocompuestoServices
    {
        public Sgpimafa2Context _DB;

        public posproductocompuestoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosproductocompuestoModel> GetAll()
        {
            try
            {
                return _DB.Posproductocompuestos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosproductocompuestoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posproductocompuestos.Find(id);
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

        public PosproductocompuestoModel Create(PosproductocompuestoModel data)
        {
            try
            {
                var prd = _DB.Posproductocompuestos.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Posproductocompuestos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosproductocompuestoModel Update(PosproductocompuestoModel data)
        {
            try
            {
                var newData = _DB.Posproductocompuestos.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var dataUpdate = _DB.Posproductocompuestos.Find(data.Id);

                // Retorna el objeto con la información de actualizada
                return dataUpdate;
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
                var newData = _DB.Posproductocompuestos.Find(id);
                if (newData != null)
                {

                    var ntask = _DB.Posproductocompuestos.Remove(newData);
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
