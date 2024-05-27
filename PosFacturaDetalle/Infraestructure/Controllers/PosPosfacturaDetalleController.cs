
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Entities;
using sgpimafaback.PosFacturaDetalle.Domain.Entities;
using sgpimafaback.PosFacturaDetalle.Domain.Services;


namespace sgpimafaback.PosFacturaDetalle.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosPosfacturaDetalleController : ControllerBase
    {

        private readonly PosFacturaDetalleServices _Getlist;
        private readonly ILogger<PosPosfacturaDetalleController> _logger;

        public PosPosfacturaDetalleController(PosFacturaDetalleServices getList, ILogger<PosPosfacturaDetalleController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosfacturadetalleModel>>> Get()
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
                _logger.LogError($"PosfacturadetalleController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id del detalle
        [HttpGet("{id}")]
        public async Task<ActionResult<PosfacturadetalleModel>> GetById(string id)
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
                            Data = new PosfacturadetalleModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosfacturadetalleModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturadetalleController(GetById {id}):   {e.Message}", e);
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

        // Recibe nro de la factura
        [HttpGet("GetByFactura/{id}")]
        public async Task<ActionResult<PosfacturadetalleModel>> GetByFactura(string id)
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

                    var resultado = _Getlist.GetByFactura(Idd);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Messages = Array.Empty<string>(),
                            Data = resultado
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosfacturadetalleModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturadetalleController(GetById {id}):   {e.Message}", e);
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
        public async Task<ActionResult<PosfacturadetalleModel>> Create([FromBody] PosfacturadetalleModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }

            //Valida el campo Id esté vacio
            if ((body.IdPos == null) || (body.IdPos <= 0))
            {
                ErrMsjs.Add("Pos:El campo id no existe o el valor no es válido");
            }

            //Valida el campo factura
            if ((body.IdFactura == null) || (body.IdFactura <= 0))
            {
                ErrMsjs.Add("Factura:El campo no existe o el valor no es válido");
            }

            //Valida lalista de Productos
            if (body.CodigoProducto == null || (body.CodigoProducto <= 0))
            {
                ErrMsjs.Add("Lista de productos:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Id esté vacio
            if ((body.Cantidad == null) || (body.Cantidad <= 0))
            {
                ErrMsjs.Add("Cantidad:El campo no existe o el valor no es válido");
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
                            Data = new PosfacturadetalleModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PosfacturadetalleModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturadetalleController(Post): {e.Message}", e);
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
                    Data = new PosfacturadetalleModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PosfacturadetalleModel>> Update(string id, [FromBody] PosfacturadetalleModel body)
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
            if ((body.IdPos == null) || (body.IdPos <= 0))
            {
                ErrMsjs.Add("Pos:El campo id no existe o el valor no es válido");
            }

            //Valida el campo factura
            if ((body.IdFactura == null) || (body.IdFactura <= 0))
            {
                ErrMsjs.Add("Factura:El campo no existe o el valor no es válido");
            }

            //Valida lalista de Productos
            if (body.CodigoProducto == null || (body.CodigoProducto <= 0))
            {
                ErrMsjs.Add("Lista de productos:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Id esté vacio
            if ((body.Cantidad == null) || (body.Cantidad <= 0))
            {
                ErrMsjs.Add("Cantidad:El campo no existe o el valor no es válido");
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
                            Data = new PosfacturadetalleModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosfacturadetalleModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosfacturadetalleController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosfacturadetalleModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosfacturadetalleModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosfacturadetalleModel>> Delete(string id)
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
                            Data = new PosfacturadetalleModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosfacturadetalleController(Delete {id}):   {e.Message}", e);
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
                    Data = new PosfacturadetalleModel[] { }
                });
            }
        }

    }
}