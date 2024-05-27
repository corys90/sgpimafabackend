using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosClientes.Domain.Entities;

namespace sgpimafaback.PosClientes.Domain.Services
{
    public class ClienteServices
    {

        public Sgpimafa2Context _DB;

        public ClienteServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            try
            {
                return _DB.Clientes.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public ClienteModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Clientes.Find(id);
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

        public ClienteModel GetByIdentification(int td, int id)
        {
            try
            {
                //var resultado = _DB.Clientes.Find(id);
                var resultado = _DB.Clientes.Where((ClienteModel rec) => (rec.TipoIdCliente == td) && (rec.IdCliente == id));
                if (resultado.Any())
                {
                    return resultado.ToList()[0];
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

        public ClienteModel Create(ClienteModel data)
        {
            try
            {
                _DB.Clientes.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public ClienteModel Update(ClienteModel data)
        {
            try
            {
                var newData = _DB.Clientes.Where(rec => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return _DB.Clientes.Find(data.Id);
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
                var newData = _DB.Clientes.Find(id);
                if (newData != null)
                {
                    var ntask = _DB.Clientes.Remove(newData);
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
