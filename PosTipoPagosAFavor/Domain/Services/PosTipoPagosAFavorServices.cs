using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosTipoPagosAFavor.Domain.Entities;

namespace sgpimafaback.PosTipoPagosAFavor.Domain.Services
{
    public class PostipopagosafavorServices
    {
        public Sgpimafa2Context _DB;

        public PostipopagosafavorServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PostipopagosafavorModel> GetAll()
        {
            try
            {
                return _DB.Postipopagosafavors.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipopagosafavorModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Postipopagosafavors.Find(id);
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

        public PostipopagosafavorModel Create(PostipopagosafavorModel data)
        {
            try
            {
                var prd = _DB.Postipopagosafavors.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Postipopagosafavors.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipopagosafavorModel Update(PostipopagosafavorModel data)
        {
            try
            {
                var newData = _DB.Postipopagosafavors.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Postipopagosafavors.Find(data.Id);

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
                var newData = _DB.Postipopagosafavors.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Postipopagosafavors.Remove(newData);
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
