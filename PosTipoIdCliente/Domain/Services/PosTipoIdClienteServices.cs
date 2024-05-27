using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosTipoIdCliente.Domain.Entities;


namespace sgpimafaback.PosTipoIdCliente.Domain.Services
{
    public class PostipoidclienteServices
    {
        public Sgpimafa2Context _DB;

        public PostipoidclienteServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PostipoidclienteModel> GetAll()
        {
            try
            {
                return _DB.Postipoidclientes.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoidclienteModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Postipoidclientes.Find(id);
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

        public PostipoidclienteModel Create(PostipoidclienteModel data)
        {
            try
            {
                var prd = _DB.Postipoidclientes.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Postipoidclientes.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PostipoidclienteModel Update(PostipoidclienteModel data)
        {
            try
            {
                var newData = _DB.Postipoidclientes.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Postipoidclientes.Find(data.Id);

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
                var newData = _DB.Postipoidclientes.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Postipoidclientes.Remove(newData);
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
