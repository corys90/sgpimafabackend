
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.InventarioProducto.Domain.Entities;
using sgpimafaback.InventarioProducto.Domain.Services;
using sgpimafaback.SedePos.Domain.Entities;
using sgpimafaback.SedePos.Domain.Services;

namespace sgpimafaback.InventarioProducto.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class inventarioproductoController : ControllerBase
    {

        private readonly inventarioproductoServices _Getlist;
        private readonly SedeposServices _GetlistSedes;
        private readonly ILogger<inventarioproductoController> _logger;

        public inventarioproductoController(inventarioproductoServices getList, SedeposServices sedeServices, ILogger<inventarioproductoController> logger)
        {
            _logger = logger;
            _Getlist = getList;
            _GetlistSedes = sedeServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<inventarioproductoModel>>> Get()
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
                _logger.LogError($"inventarioproductoController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: aquí Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<inventarioproductoModel>> GetById(string id)
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
                            Data = new inventarioproductoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new inventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"inventarioproductoController(GetById {id}):   {e.Message}", e);
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
                    Data = new inventarioproductoModel[] { }
                });
            }
        }

        // Recibe el CodigoProducto 
        [HttpGet("getProducto/{id}")]
        public async Task<ActionResult<inventarioproductoModel>> GetByProductoId(string id)
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
                            Data = new inventarioproductoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new inventarioproductoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"inventarioproductoController(GetById {id}):   {e.Message}", e);
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
                    Data = new inventarioproductoModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<inventarioproductoModel>> Create([FromBody] inventarioproductoModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
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
                    //var sedes = _GetlistSedes.GetAll();
                    //_logger.LogInformation($"\n No. pos: {sedes.ToList().Count()}");

                    //foreach (SedeposModel sede in sedes)
                    //{
                    //    _logger.LogInformation($"\nSedes:   {Utilities.GetPropiedades(sede)}\n");
                    //}

                    var resultado = _Getlist.Create(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Created,
                            Messages = Array.Empty<string>(),
                            Data = new inventarioproductoModel[] { resultado }
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
                            Data = new inventarioproductoModel[] { }
                        });
                    }

                }
                catch (Exception e)
                {
                    _logger.LogError($"inventarioproductoController(Post): {e.Message}", e);
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
                    Data = new inventarioproductoModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<inventarioproductoModel>> Update(string id, [FromBody] inventarioproductoModel body)
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
                            Data = new inventarioproductoModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new inventarioproductoModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"inventarioproductoController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new inventarioproductoModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new inventarioproductoModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<inventarioproductoModel>> Delete(string id)
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
                            Data = new inventarioproductoModel[] { }
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
                    Data = new inventarioproductoModel[] { }
                });
            }
        }

    }
}