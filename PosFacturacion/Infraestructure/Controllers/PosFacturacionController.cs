using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosFacturacion.Domain.Entities;
using sgpimafaback.PosFacturacionServices.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosFacturacionController.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosFacturacionController : ControllerBase
    {


        private readonly PosfacturacionServices _Getlist;
        private readonly ILogger<PosFacturacionController> _logger;

        public PosFacturacionController(PosfacturacionServices getList, ILogger<PosFacturacionController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosfacturaModel>>> Get()
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
                _logger.LogError($"PosfacturaController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PosfacturaModel>> GetById(string id)
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
                            Data = new header[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No encontrado" },
                            Data = new PosfacturaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturaController(GetById {id}):   {e.Message}", e);
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
                    Data = new PosfacturaModel[] { }
                });
            }
        }

        [HttpGet("GetByClienteId/{id}")]
        public async Task<ActionResult<header>> GetByClienteId(string id)
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

                    var resultado = _Getlist.GetByClienteId(Idd);
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
                            Data = new header[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturaController(GetByClienteId {id}):   {e.Message}", e);
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
                    Data = new PosfacturaModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<header>> Create([FromBody] DtoPosfactura body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.factHeader.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }

            //Valida el campo Id esté vacio
            if ((body.factHeader.IdPos == null) || (body.factHeader.IdPos == 0))
            {
                ErrMsjs.Add("Pos:El campo id no existe o el valor no es válido");
            }

            //Valida el campo factura
            if ((body.factHeader.IdFactura == null) || (body.factHeader.IdFactura != 0))
            {
                ErrMsjs.Add("Factura:El campo no existe o el valor no es válido");
            }

            //Valida el campo Nit
            if ((body.factHeader.Nit == null) || (body.factHeader.Nit <= 0))
            {
                ErrMsjs.Add("Nit :El campo Nit no existe o contiene un valor no válido");
            }

            //Valida el campo Razón Social
            if ((body.factHeader.RazonSocial == null) || (body.factHeader.RazonSocial.Trim().Equals("")))
            {
                ErrMsjs.Add("RazonSocial:El campo no existe o contiene un valor no válido");
            }

            //Valida el tipo cliente
            if ((body.factHeader.TipoCliente == null) || (body.factHeader.TipoCliente <= 0))
            {
                ErrMsjs.Add("Tipo Cliente :El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Direccion
            if ((body.factHeader.Direccion == null) || body.factHeader.Direccion.Trim().Equals(""))
            {
                ErrMsjs.Add("Dirección:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Dirección
            if (body.factHeader.IdVendedor == null || (body.factHeader.IdVendedor <= 0))
            {
                ErrMsjs.Add("Vendedor Id:El campo no existe o contiene un valor vacio");
            }


            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    var resultado = _Getlist.Create(body);

                    var response = new
                    {
                        StatusCode = HttpStatusCode.Created,
                        Messages = Array.Empty<string>(),
                        Data = new header[] { resultado }
                    };
                    return Created("Creado", response);

                }
                catch (Exception e)
                {
                    _logger.LogError($"PosfacturaController(Post): {e.Message}", e);
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
                    Data = new PosfacturaModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<header>> Update(string id, [FromBody] header body)
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

            //Valida el tipo cliente
            if ((body.TipoCliente == null) || (body.TipoCliente <= 0))
            {
                ErrMsjs.Add("Tipo Cliente :El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Direccion
            if ((body.Direccion == null) || body.Direccion.Trim().Equals(""))
            {
                ErrMsjs.Add("Dirección:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Dirección
            if (body.IdVendedor == null || (body.IdVendedor <= 0))
            {
                ErrMsjs.Add("Vendedor Id:El campo no existe o contiene un valor vacio");
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
                            Data = new header[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosfacturaModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosfacturaController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosfacturaModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosfacturaModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosfacturaModel>> Delete(string id)
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
                            Data = new PosfacturaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosfacturaController(Delete {id}):   {e.Message}", e);
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
                    Data = new PosfacturaModel[] { }
                });
            }
        }
    }
}
