
using sgpimafaback.Context;
using System.Net;

namespace sgpimafaback.UtiliatriesApi.Domain.Services
{
    public class utilitariesapiServices
    {
        public Sgpimafa2Context _DB;

        public utilitariesapiServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public DtoTipos GetAll()
        {
            try
            {
                var sedes = _DB.Sedepos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var unidadMedida = _DB.Posunidadesmedida.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoProducto = _DB.Postipoproductos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoPagosAFavor = _DB.Postipopagosafavors.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoIdCliente = _DB.Postipoidclientes.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoEstadoPosCaja = _DB.Postipoestadoposcajas.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoEstadoCaja = _DB.Postipoestadocajas.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoEmbalaje = _DB.Postipoembalajes.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                var tipoPrdCompuesto = _DB.Posproductocompuestos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();

                DtoTipos tipos = new DtoTipos();

                tipos.Sedes = sedes;
                tipos.UnidadMedida = unidadMedida;
                tipos.TipoProducto = tipoProducto;
                tipos.TipoPagosAFavor = tipoPagosAFavor;
                tipos.TipoIdCliente = tipoIdCliente;
                tipos.TipoEstadoPosCaja = tipoEstadoPosCaja;
                tipos.TipoEstadoCaja = tipoEstadoCaja;
                tipos.TipoEmbalaje = tipoEmbalaje;
                tipos.TipoPrdCompuesto = tipoPrdCompuesto;


                return tipos;
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public Object GetById(string tipo)
        {
            try
            {
                switch (tipo.ToLower())
                {
                    case "sedes": return _DB.Sedepos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "unidadmedida": return _DB.Posunidadesmedida.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoproducto": return _DB.Postipoproductos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipopagosafavor": return _DB.Postipopagosafavors.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoidcliente": return _DB.Postipoidclientes.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoestadoposcaja": return _DB.Postipoestadoposcajas.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoestadocaja": return _DB.Postipoestadocajas.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoembalaje": return _DB.Postipoembalajes.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();
                    case "tipoprdcompuesto": return _DB.Posproductocompuestos.Select(field => new { field.Id, field.Nombre, field.Estado }).ToArray();

                }

                return null;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

    }

    public class DtoTipos
    {
        public Object Sedes;
        public Object UnidadMedida;
        public Object TipoProducto;
        public Object TipoPagosAFavor;
        public Object TipoIdCliente;
        public Object TipoEstadoPosCaja;
        public Object TipoEstadoCaja;
        public Object TipoEmbalaje;
        public Object TipoPrdCompuesto;

    }
}
