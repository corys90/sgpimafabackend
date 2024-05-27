
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosInventarioProducto.Domain.Entities;
using sgpimafaback.PosInventarioProducto.Domain.Services;

namespace sgpimafaback.PosInventarioProducto.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosinventarioproductoController : ControllerBase
    {

        private readonly PosinventarioproductoServices _Getlist;
        private readonly ILogger<PosinventarioproductoController> _logger;

        public PosinventarioproductoController(PosinventarioproductoServices getList, ILogger<PosinventarioproductoController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosinventarioproductoModel>>> Get()
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
                _logger.LogError($"PosinventarioproductoController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PosinventarioproductoModel>> GetById(string id)
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
                            Data = new PosinventarioproductoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosinventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"ClienteController(GetById {id}):   {e.Message}", e);
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
                    Data = new PosinventarioproductoModel[] { }
                });
            }
        }

        // Recibe el CodigoProducto 
        [HttpGet("getProducto/{id}")]
        public async Task<ActionResult<PosinventarioproductoModel>> GetByProductoId(string id)
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

                    var resultado = _Getlist.GetByProductoId(Idd);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Messages = Array.Empty<string>(),
                            Data = new PosinventarioproductoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosinventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosinventarioproductoController(GetById {id}):   {e.Message}", e);
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
                    Data = new PosinventarioproductoModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PosinventarioproductoModel>> Create([FromBody] PosinventarioproductoModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo pos
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Pos Id:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo codigo prodcuto
            if ((body.IdCodigo == null) || (body.IdCodigo < 0))
            {
                ErrMsjs.Add("Codigo:El campo no existe o contiene un valor no válido");
            }                

            //Valida el campo Tipo
            if (body.TipoProducto == null || (body.TipoProducto < 0))
            {
                ErrMsjs.Add("Tipo:El campo no existe o contiene un valor vacio");
            }     
            
            //Valida el campo Nombre
            if (body.Nombre == null || body.Nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo cantidad
            if (body.Cantidad == null || (body.Cantidad < 0))
            {
                ErrMsjs.Add("Cantidad:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo valor
            if (body.ValorUnitario == null || (body.ValorUnitario < 0))
            {
                ErrMsjs.Add("Valor:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Ciudad
            if (body.FechaCreacion == null || body.FechaCreacion.Equals(""))
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
                            Data = new PosinventarioproductoModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Producto: Ya existe un producto con el mismo código que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PosinventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosinventarioproductoController(Post): {e.Message}", e);
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
                    Data = new PosinventarioproductoModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PosinventarioproductoModel>> Update(string id, [FromBody] PosinventarioproductoModel body)
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

            //Valida el campo pos
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Pos Id:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo codigo prodcuto
            if ((body.IdCodigo == null) || (body.IdCodigo < 0))
            {
                ErrMsjs.Add("Codigo:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo Tipo
            if (body.TipoProducto == null || (body.TipoProducto < 0))
            {
                ErrMsjs.Add("Tipo:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Nombre
            if (body.Nombre == null || body.Nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo cantidad
            if (body.Cantidad == null || (body.Cantidad < 0))
            {
                ErrMsjs.Add("Cantidad:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo valor
            if (body.ValorUnitario == null || (body.ValorUnitario < 0))
            {
                ErrMsjs.Add("Valor:El campo no existe o contiene un valor vacio");
            }

            //Valida el campo Ciudad
            if (body.FechaCreacion == null || body.FechaCreacion.Equals(""))
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
                            Data = new PosinventarioproductoModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosinventarioproductoModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosinventarioproductoController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosinventarioproductoModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosinventarioproductoModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosinventarioproductoModel>> Delete(string id)
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
                            Data = new PosinventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"SedeposController(Delete {id}):   {e.Message}", e);
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
                    Data = new PosinventarioproductoModel[] { }
                });
            }
        }

    }
}