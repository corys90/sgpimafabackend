using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCajaArqueo.Domain.Entities;

namespace sgpimafaback.PosCajaArqueo.Domain.Services
{
    public class PosCajaArqueoServices
    {

        public Sgpimafa2Context _DB;

        public PosCajaArqueoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PoscajaarqueoModel> GetAll()
        {
            try
            {
                return _DB.Poscajaarqueos.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaarqueoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Poscajaarqueos.Find(id);
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

        public PoscajaarqueoModel Create(PoscajaarqueoModel data)
        {
            try
            {
                _DB.Poscajaarqueos.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaarqueoModel Update(PoscajaarqueoModel data)
        {
            try
            {
                var newData = _DB.Poscajaarqueos.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Poscajaarqueos.Find(data.Id);
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
                var newData = _DB.Poscajaarqueos.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Poscajaarqueos.Remove(newData);
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
