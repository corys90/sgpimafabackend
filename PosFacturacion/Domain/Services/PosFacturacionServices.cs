using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosFacturacion.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Entities;

namespace sgpimafaback.PosFacturacionServices.Domain.Services
{
    public class PosfacturacionServices
    {

        public Sgpimafa2Context _DB;

        public PosfacturacionServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<header> GetAll()
        {
            try
            {
                return _DB.Posfacturas.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public header GetById(int id)
        {
            try
            {
                var resultado = _DB.Posfacturas.Find(id);
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

        public IEnumerable<header> GetByClienteId(int id)
        {
            try
            {
                IEnumerable<header> resultado = _DB.Posfacturas.ToList().Where(reg => reg.Nit == id);
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

        public header GetByFactura(int id)
        {
            try
            {
                var resultado = _DB.Posfacturas.Find(id);
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


        public header Create(DtoPosfactura data)
        {
            try
            {
                //Proceso que crea o guarda la cabecera de la factura
                _DB.Posfacturas.Add(data.factHeader);
                _DB.SaveChanges();

                // el id generado es el mismo para el idFcatura
                data.factHeader.IdFactura = data.factHeader.Id;
                // actualizo la cabecera con el id an al idFactura
                header resp = Update(data.factHeader);

                // Proceso que guarda el detalle de la factura
                foreach (var item in data.lista)
                {
                    item.IdFactura = data.factHeader.IdFactura;
                    _DB.Posfacturadetalles.Add(item);
                    _DB.SaveChanges();

                    //Proceso que actualiza el stock de cada producto
                    PosinventarioproductoModel prd = _DB.Posinventarioproductos.Where(reg => reg.IdCodigo == item.CodigoProducto).First();
                    prd.Cantidad = prd.Cantidad - item.Cantidad;
                    _DB.SaveChanges();

                }
                // Responde con la cabecera actualizada
                return resp;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }

        }

        public header Update(header data)
        {
            try
            {
                var newData = _DB.Posfacturas.Where((header rec) => (rec.Id == data.Id));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posfacturas.Find(data.Id);

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
                var newData = _DB.Posfacturas.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posfacturas.Remove(newData);
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
