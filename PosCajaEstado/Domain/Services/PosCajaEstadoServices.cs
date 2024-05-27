using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCajaEstado.Domain.Entities;

namespace sgpimafaback.PosCajaEstado.Domain.Services
{
    public class PosCajaEstadoServices
    {

        public Sgpimafa2Context _DB;

        public PosCajaEstadoServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PoscajaestadoModel> GetAll()
        {
            try
            {
                return _DB.Poscajaestados.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaestadoModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Poscajaestados.Find(id);
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

        public PoscajaestadoModel Create(PoscajaestadoModel data)
        {
            try
            {
                _DB.Poscajaestados.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaestadoModel Update(PoscajaestadoModel data)
        {
            try
            {
                var newData = _DB.Poscajaestados.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Poscajaestados.Find(data.Id);
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
                var newData = _DB.Poscajaestados.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Poscajaestados.Remove(newData);
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
