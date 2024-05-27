using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosFacturaDetalle.Domain.Entities;


namespace sgpimafaback.PosFacturaDetalle.Domain.Services
{
    public class PosFacturaDetalleServices
    {
        public Sgpimafa2Context _DB;

        public PosFacturaDetalleServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosfacturadetalleModel> GetAll()
        {
            try
            {
                return _DB.Posfacturadetalles.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosfacturadetalleModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posfacturadetalles.Find(id);
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

        public IEnumerable<PosfacturadetalleModel> GetByFactura(int id)
        {
            try
            {
                var resultado = _DB.Posfacturadetalles.ToList().Where(rec => rec.IdFactura == id);
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

        public PosfacturadetalleModel Create(PosfacturadetalleModel data)
        {
            try
            {

                _DB.Posfacturadetalles.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosfacturadetalleModel Update(PosfacturadetalleModel data)
        {
            try
            {
                var newData = _DB.Posfacturadetalles.Where((PosfacturadetalleModel rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posfacturadetalles.Find(data.Id);

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
                var newData = _DB.Posfacturadetalles.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posfacturadetalles.Remove(newData);
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
