using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCajaPagosAFavor.Domain.Entities;

namespace sgpimafaback.PosCajaPagoFactura.Domain.Services
{
    public class PosCajaPagosAFavorServices
    {

        public Sgpimafa2Context _DB;

        public PosCajaPagosAFavorServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PoscajapagosafavorModel> GetAll()
        {
            try
            {
                return _DB.Poscajapagosafavors.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajapagosafavorModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Poscajapagosafavors.Find(id);
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

        public IEnumerable<PoscajapagosafavorModel> GetByFactura(int idFact)
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

        public PoscajapagosafavorModel Create(PoscajapagosafavorModel data)
        {
            try
            {
                _DB.Poscajapagosafavors.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajapagosafavorModel Update(PoscajapagosafavorModel data)
        {
            try
            {
                var newData = _DB.Poscajapagosafavors.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Poscajapagosafavors.Find(data.Id);
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
                var newData = _DB.Poscajapagosafavors.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Poscajapagosafavors.Remove(newData);
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
