using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.InventarioProducto.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Services;
using sgpimafaback.PosMovimientoInventario.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;


namespace sgpimafaback.PosMovimientoInventarioServices.Domain.Services
{
    public class posmovimientoinventarioServices
    {
        public Sgpimafa2Context _DB;

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

                // obtiene el producto del inventario general
                inventarioproductoModel newPrd = _DB.inventarioproductos.First(reg => (reg.IdCodigo == data.IdCodigo));

                // obtiene el producto del inventario del POS
                PosinventarioproductoModel prd = _DB.Posinventarioproductos.FirstOrDefault(reg => (reg.IdCodigo == data.IdCodigo) && (reg.IdPos == data.IdPos));
                if (prd != null)
                {
                    prd.Cantidad = prd.Cantidad + data.Cantidad;
                    _DB.SaveChanges();
                }
                else
                {
                    /*static void EntityFrameworkMySQLExampleUpdateRecord()
                    {
                        using (var dbContext = new MyDbContext())
                        {
                            // var record6 = dbContext.TestModels.Where(record => record.ID == 4).FirstOrDefault();
                            var record6 = new TestModel { ID = 4, Value = "EF Core !" };
                            dbContext.Attach(record6);
                            dbContext.Entry(record6).Property(r => r.Value).IsModified = true;
                            dbContext.SaveChanges();
                        }
                    }*/

                    PosinventarioproductoModel rvPrd = new PosinventarioproductoModel();
                    rvPrd = (PosinventarioproductoModel) newPrd;
                    //rvPrd.Cantidad = data.Cantidad;
                    _DB.Posinventarioproductos.Add(rvPrd);
                    _DB.SaveChanges();

                }

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
