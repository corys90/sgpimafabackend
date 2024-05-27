using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCajaEstado.Domain.Entities;
using sgpimafaback.PosCajaPagoFactura.Domain.Entities;

namespace sgpimafaback.PosCajaPagoFactura.Domain.Services
{
    public class PosCajaPagoFacturaServices
    {

        public Sgpimafa2Context _DB;

        public PosCajaPagoFacturaServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PoscajapagofacturaModel> GetAll()
        {
            try
            {
                return _DB.Poscajapagofacturas.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajapagofacturaModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Poscajapagofacturas.Find(id);
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

        public IEnumerable<PoscajapagofacturaModel> GetByFactura(int idFact)
        {
            try
            {
                var resultado = GetAll().Where(pg => pg.IdFactura == idFact);

                if (resultado != null)
                {
                    return resultado.ToList();
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

        public PoscajapagofacturaModel Create(PoscajapagofacturaModel data)
        {
            try
            {
                _DB.Poscajapagofacturas.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajapagofacturaModel Update(PoscajapagofacturaModel data)
        {
            try
            {
                var newData = _DB.Poscajapagofacturas.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Poscajapagofacturas.Find(data.Id);
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
                var newData = _DB.Poscajapagofacturas.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Poscajapagofacturas.Remove(newData);
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
