using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosTipoEstadoPosCaja.Domain.Entities;

namespace sgpimafaback.PosTipoEstadoPosCaja.Domain.Services
{
    public class PostipoestadosposcajaServices
    {
        public Sgpimafa2Context _DB;

        public PostipoestadosposcajaServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PostipoestadoposcajaModel> GetAll()
        {
            try
            {
                return _DB.Postipoestadoposcajas.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoestadoposcajaModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Postipoestadoposcajas.Find(id);
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

        public PostipoestadoposcajaModel Create(PostipoestadoposcajaModel data)
        {
            try
            {
                var prd = _DB.Postipoestadoposcajas.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (prd.Count() > 0)
                {
                    return null;
                }

                _DB.Postipoestadoposcajas.Add(data);
                var ntask = _DB.SaveChanges();

                var dataUpdate = _DB.Postipoestadoposcajas.Find(data.Id);

                // Retorna el objeto con la información de actualizada
                return dataUpdate;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoestadoposcajaModel Update(PostipoestadoposcajaModel data)
        {
            try
            {
                var newData = _DB.Postipoestadoposcajas.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Postipoestadoposcajas.Find(data.Id);

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
                var newData = _DB.Postipoestadoposcajas.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Postipoestadoposcajas.Remove(newData);
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
