
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosMovimientoInventario.Domain.Entities;
using sgpimafaback.PosMovimientoInventarioServices.Domain.Services;


namespace sgpimafaback.PosMovimientoInventario.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosMovimientoInventarioController : ControllerBase
    {

        private readonly posmovimientoinventarioServices _Getlist;
        private readonly ILogger<PosMovimientoInventarioController> _logger;

        public PosMovimientoInventarioController(posmovimientoinventarioServices getList, ILogger<PosMovimientoInventarioController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosmovimientoinventarioModel>>> Get()
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
                _logger.LogError($"PosmovimientoInventariosController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PosmovimientoinventarioModel>> GetById(string id)
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
                            Data = new PosmovimientoinventarioModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosmovimientoinventarioModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"posmovimientoinventarioController(GetById {id}):   {e.Message}", e);
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
                    Data = new PosmovimientoinventarioModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PosmovimientoinventarioModel>> Create([FromBody] PosmovimientoinventarioModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo Tipo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("IdPos:El campo Tipo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdCodigo == null) || (body.IdCodigo < 0))
            {
                ErrMsjs.Add("IdCodigo:El campo código producto no existe o contiene un valor no válido");
            }

            //Valida el campo cantidad
            if (body.Cantidad == null || body.Cantidad <= 0)
            {
                ErrMsjs.Add("Cantidad:El campo cantidad no existe o contiene un su valor no es válido");
            }

            //Valida el campo Apellido
            if (body.FechaMovimiento == null || body.FechaMovimiento.Equals(""))
            {
                ErrMsjs.Add("fechaMovimiento:El campo fechaMovimiento no existe o contiene un valor no válido");
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
                            Data = new PosmovimientoinventarioModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Error: Intenta crear una movimiento con la misma ientificación de uno ya existente");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PosmovimientoinventarioModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"posmovimientoinventarioController(Post): {e.Message}", e);
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
                    Data = new PosmovimientoinventarioModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PosmovimientoinventarioModel>> Update(string id, [FromBody] PosmovimientoinventarioModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id: El campo Id no existe o no contiene un valor válido");
            }

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo Tipo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("IdPos:El campo Tipo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdCodigo == null) || (body.IdCodigo < 0))
            {
                ErrMsjs.Add("IdCodigo:El campo código producto no existe o contiene un valor no válido");
            }

            //Valida el campo cantidad
            if (body.Cantidad == null || body.Cantidad <= 0)
            {
                ErrMsjs.Add("Cantidad:El campo cantidad no existe o contiene un su valor no es válido");
            }

            //Valida el campo Apellido
            if (body.FechaMovimiento == null || body.FechaMovimiento.Equals(""))
            {
                ErrMsjs.Add("fechaMovimiento:El campo fechaMovimiento no existe o contiene un valor no válido");
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
                            Data = new PosmovimientoinventarioModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosmovimientoinventarioModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"posmovimientoinventarioController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosmovimientoinventarioModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosmovimientoinventarioModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosmovimientoinventarioModel>> Delete(string id)
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
                            Data = new PosmovimientoinventarioModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"posmovimientoinventarioController(Delete {id}):   {e.Message}", e);
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
                    Data = new PosmovimientoinventarioModel[] { }
                });
            }
        }

    }
}