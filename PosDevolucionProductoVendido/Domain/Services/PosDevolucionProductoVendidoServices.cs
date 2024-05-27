using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Entities;

namespace sgpimafaback.PosDevolucionProductoVendido.Domain.Services
{
    public class PosDevolucionProductoVendidoServices
    {

        public Sgpimafa2Context _DB;

        public PosDevolucionProductoVendidoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosdevolucionproductovendidoModel> GetAll()
        {
            try
            {
                return _DB.Posdevolucionproductovendidos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosdevolucionproductovendidoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posdevolucionproductovendidos.Find(id);
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


        public PosdevolucionproductovendidoModel Create(PosdevolucionproductovendidoModel data)
        {
            try
            {
                _DB.Posdevolucionproductovendidos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosdevolucionproductovendidoModel Update(PosdevolucionproductovendidoModel data)
        {
            try
            {
                var newData = _DB.Posdevolucionproductovendidos.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Posdevolucionproductovendidos.Find(data.Id);
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
                var newData = _DB.Posdevolucionproductovendidos.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Posdevolucionproductovendidos.Remove(newData);
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
