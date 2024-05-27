using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Entities;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosDevolucionProductoVendido.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosDevolucionProductoVendidoController : ControllerBase
    {

        private readonly PosDevolucionProductoVendidoServices _Getlist;
        private readonly ILogger<PosDevolucionProductoVendidoController> _logger;

        public PosDevolucionProductoVendidoController(PosDevolucionProductoVendidoServices getList, ILogger<PosDevolucionProductoVendidoController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosdevolucionproductovendidoModel>>> Get()
        {
            try
            {
                var response = Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    Messages = Array.Empty<string>(),
                    Data = _Getlist.GetAll(),
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"PosdevolucionproductovendidoController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }
        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PosdevolucionproductovendidoModel>> GetById(string id)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor válido");
            }

            //Valida el campo Nombre
            if (ErrMsjs.Count <= 0)
            {

                try
                {

                    var resultado = _Getlist.GetById(Idd);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Messages = Array.Empty<string>(),
                            Data = new PosdevolucionproductovendidoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosdevolucionproductovendidoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosdevolucionproductovendidoController(GetById {id}):   {e.Message}", e);
                    return new ContentResult
                    {
                        StatusCode = (int?)HttpStatusCode.InternalServerError,
                        Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                    };
                }

            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosdevolucionproductovendidoModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PosdevolucionproductovendidoModel>> Create([FromBody] PosdevolucionproductovendidoModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }

            //Valida el campo Id esté vacio
            if ((body.IdPos == null) || (body.IdPos == 0))
            {
                ErrMsjs.Add("Pos:El campo id no existe o el valor no es válido");
            }

            //Valida el campo factura
            if ((body.IdFactura == null) || (body.IdFactura == 0))
            {
                ErrMsjs.Add("Factura:El campo no existe o el valor no es válido");
            }

            //Valida el campo Nit
            if ((body.Nit == null) || (body.Nit <= 0))
            {
                ErrMsjs.Add("Nit :El campo Nit no existe o contiene un valor no válido");
            }

            //Valida el campo Razón Social
            if ((body.RazonSocial == null) || (body.RazonSocial.Trim().Equals("")))
            {
                ErrMsjs.Add("RazonSocial:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Direccion
            if ((body.Direccion == null) || (body.Direccion.Trim().Equals("")))
            {
                ErrMsjs.Add("Dirección:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Producto
            if (body.CodigoProducto == null || body.CodigoProducto <= 0)
            {
                ErrMsjs.Add("Producto Id:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Motivo
            if (body.Motivo == null || body.Motivo.Equals(""))
            {
                ErrMsjs.Add("Motivo:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo fecha devolución
            if (body.FechaDevolucion == null || body.FechaDevolucion.Equals(""))
            {
                ErrMsjs.Add("Fecha:El campo no existe o contiene un valor vacio");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    var resultado = _Getlist.Create(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Created,
                            Messages = Array.Empty<string>(),
                            Data = new PosdevolucionproductovendidoModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Producto Devuelto: Ya existe un Cliente con la misma ientificación que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PosdevolucionproductovendidoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosdevolucionproductovendidoController(Post): {e.Message}", e);
                    return new ContentResult
                    {
                        StatusCode = (int?)HttpStatusCode.InternalServerError,
                        Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                    };
                }

            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosdevolucionproductovendidoModel[] { }
                });
            }
        }

        [HttpPut("{id}/{idCliente}")]
        public async Task<ActionResult<PosdevolucionproductovendidoModel>> Update(string id, string idCliente, [FromBody] PosdevolucionproductovendidoModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor válido");
            }

            // Valida que el id del parámetro sea igual a id del body
            if (Idd != body.Id)
            {
                ErrMsjs.Add("Error:El campo id del parámetro no coincide con el id del body");
            }

            //Valida el campo Id esté vacio
            if ((body.IdPos == null) || (body.IdPos == 0))
            {
                ErrMsjs.Add("Pos:El campo id no existe o el valor no es válido");
            }

            //Valida el campo factura
            if ((body.IdFactura == null) || (body.IdFactura == 0))
            {
                ErrMsjs.Add("Factura:El campo no existe o el valor no es válido");
            }

            //Valida el campo Nit
            if ((body.Nit == null) || (body.Nit <= 0))
            {
                ErrMsjs.Add("Nit :El campo Nit no existe o contiene un valor no válido");
            }

            //Valida el campo Razón Social
            if ((body.RazonSocial == null) || (body.RazonSocial.Trim().Equals("")))
            {
                ErrMsjs.Add("RazonSocial:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Direccion
            if ((body.Direccion == null) || (body.Direccion.Trim().Equals("")))
            {
                ErrMsjs.Add("Dirección:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Producto
            if (body.CodigoProducto == null || body.CodigoProducto <= 0)
            {
                ErrMsjs.Add("Producto Id:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Motivo
            if (body.Motivo == null || body.Motivo.Equals(""))
            {
                ErrMsjs.Add("Motivo:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo fecha devolución
            if (body.FechaDevolucion == null || body.FechaDevolucion.Equals(""))
            {
                ErrMsjs.Add("Fecha:El campo no existe o contiene un valor vacio");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {

                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new PosdevolucionproductovendidoModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosdevolucionproductovendidoModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosdevolucionproductovendidoController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosdevolucionproductovendidoModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosdevolucionproductovendidoModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosdevolucionproductovendidoModel>> Delete(string id)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int IdTipo);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor válido");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    var resultado = _Getlist.Delete(IdTipo);
                    if (resultado)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No encontrado" },
                            Data = new PosdevolucionproductovendidoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosdevolucionproductovendidoController(Delete {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return new ContentResult
                    {
                        StatusCode = (int?)HttpStatusCode.InternalServerError,
                        Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                    };
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosdevolucionproductovendidoModel[] { }
                });
            }
        }
    }
}
