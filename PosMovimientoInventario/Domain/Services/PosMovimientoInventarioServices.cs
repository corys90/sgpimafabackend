
using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.InventarioProducto.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Services;
using sgpimafaback.PosMovimientoInventario.Domain.Entities;


namespace sgpimafaback.PosMovimientoInventarioServices.Domain.Services
{
    public class posmovimientoinventarioServices
    {
        public Sgpimafa2Context _DB;
        private readonly PosinventarioproductoServices _GetInvPrd;

        public posmovimientoinventarioServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosmovimientoinventarioModel> GetAll()
        {
            try
            {
                return _DB.Posmovimientoinventarios.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosmovimientoinventarioModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posmovimientoinventarios.Find(id);
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

        public PosmovimientoinventarioModel Create(PosmovimientoinventarioModel data)
        {
            try
            {
                // añade el registro de ingreso de inventario
                _DB.Posmovimientoinventarios.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosmovimientoinventarioModel Update(PosmovimientoinventarioModel data)
        {
            try
            {
                var newData = _DB.Posmovimientoinventarios.Where((PosmovimientoinventarioModel rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posmovimientoinventarios.Find(data.Id);

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
                var newData = _DB.Posmovimientoinventarios.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posmovimientoinventarios.Remove(newData);
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
