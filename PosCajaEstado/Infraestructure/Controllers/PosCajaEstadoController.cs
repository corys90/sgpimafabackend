using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosCajaEstado.Domain.Entities;
using sgpimafaback.PosCajaEstado.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosCajaEstado.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosCajaEstadoController : ControllerBase
    {

        private readonly PosCajaEstadoServices _Getlist;
        private readonly ILogger<PosCajaEstadoController> _logger;

        public PosCajaEstadoController(PosCajaEstadoServices getList, ILogger<PosCajaEstadoController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        // GET: api/<PosCajaEstadoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoscajaestadoModel>>> Get()
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
                _logger.LogError($"PosCajaEstadoController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }
        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoscajaestadoModel>> GetById(string id)
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
                            Data = new PoscajaestadoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajaestadoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaEstadoController(GetById {id}):   {e.Message}", e);
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
                    Data = new PoscajaestadoModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PoscajaestadoModel>> Create([FromBody] PoscajaestadoModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo id caja
            if ((body.IdCaja == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Caja:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Pos:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Valorestado
            if (body.ValorEstado == null || body.ValorEstado <= 0)
            {
                ErrMsjs.Add("ValorEstado:El campo ValorEstado no existe o contiene un valor no válido");
            }

            //Valida el campo estado
            if (body.Estado == null || body.Estado <= 0)
            {
                ErrMsjs.Add("Estado:El campo Estado no existe o contiene un valor no válido");
            }

            //Valida el campo Fecha
            if (body.FechaOperacion == null || body.FechaOperacion.Equals(""))
            {
                ErrMsjs.Add("Fecha:El campo fecha no existe o contiene un valor no válido");
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
                        Data = new PoscajaestadoModel[] { resultado }
                    };
                    return Created("Creado", response);
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaEstadoController(Post): {e.Message}", e);
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
                    Data = new PoscajaestadoModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PoscajaestadoModel>> Update(string id, [FromBody] PoscajaestadoModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor válido");
            }

            //Valida el campo Tipo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Caja:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Pos:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Valorestado
            if (body.ValorEstado == null || body.ValorEstado <= 0)
            {
                ErrMsjs.Add("ValorEstado:El campo ValorEstado no existe o contiene un valor no válido");
            }

            //Valida el campo estado
            if (body.Estado == null || body.Estado <= 0)
            {
                ErrMsjs.Add("Estado:El campo Estado no existe o contiene un valor no válido");
            }

            //Valida el campo Fecha
            if (body.FechaOperacion == null || body.FechaOperacion.Equals(""))
            {
                ErrMsjs.Add("Fecha:El campo fecha no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.Id != Idd))
            {
                ErrMsjs.Add("Error:El campo id del parámetro no coincide con el Id del Body");
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
                            Data = new PoscajaestadoModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajaestadoModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosCajaEstadoController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PoscajaestadoModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PoscajaestadoModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PoscajaestadoModel>> Delete(string id)
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
                            Data = new PoscajaestadoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosCajaEstadoController(Delete {id}):   {e.Message}", e);
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
                    Data = new PoscajaestadoModel[] { }
                });
            }
        }
    }
}
