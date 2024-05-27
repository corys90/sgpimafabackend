using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCaja.Domain.Entities;

namespace sgpimafaback.PosCaja.Domain.Services
{
    public class PosCajaServices
    {

        public Sgpimafa2Context _DB;

        public PosCajaServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PoscajaModel> GetAll()
        {
            try
            {
                return _DB.Poscajas.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Poscajas.Find(id);
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

        public PoscajaModel Create(PoscajaModel data)
        {
            try
            {
                var caja = _DB.Poscajas.Where((PoscajaModel rec) => (rec.IdPos == data.IdPos) && (rec.Nombre == data.Nombre));
                if (caja.Any())
                {
                    return null;
                }

                _DB.Poscajas.Add(data);
                _DB.SaveChanges();

                //Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PoscajaModel Update(PoscajaModel data)
        {
            try
            {
                var newData = _DB.Poscajas.Where(
                    (PoscajaModel rec) => (rec.Id == data.Id) && (rec.IdPos == data.IdPos) && rec.Nombre.Equals(data.Nombre)
                );

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información actualizada
                return _DB.Poscajas.Find(data.Id);
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public bool Delete(int id)
        {
            try
            {
                var newData = _DB.Poscajas.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Poscajas.Remove(newData);
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
