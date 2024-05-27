using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.Models;
using sgpimafaback.PosTipoEstadoCaja.Domain.Entities;


namespace sgpimafaback.PosTipoEstadoCaja.Domain.Services
{
    public class PostipoestadoscajaServices
    {
        public Sgpimafa2Context _DB;

        public PostipoestadoscajaServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PostipoestadocajaModel> GetAll()
        {
            try
            {
                return _DB.Postipoestadocajas.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoestadocajaModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Postipoestadocajas.Find(id);
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

        public PostipoestadocajaModel Create(PostipoestadocajaModel data)
        {
            try
            {

                var prd = _DB.Postipoestadocajas.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Postipoestadocajas.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoestadocajaModel Update(int id, string nombre, PostipoestadocajaModel data)
        {
            try
            {
                var newData = _DB.Postipoestadocajas.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Postipoestadocajas.Find(data.Id);

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
                var newData = _DB.Postipoestadocajas.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Postipoestadocajas.Remove(newData);
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
