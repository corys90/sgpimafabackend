using Microsoft.AspNetCore.Mvc;
using sgpimafaback.Context;
using sgpimafaback.PosCaja.Domain.Entities;
using sgpimafaback.PosCaja.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosCaja.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosCajaController : ControllerBase
    {

        private readonly PosCajaServices _Getlist;
        private readonly ILogger<PosCajaController> _logger;


        public PosCajaController(PosCajaServices getList, ILogger<PosCajaController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }


        // GET: api/<PosCajaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoscajaModel>>> Get()
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
                _logger.LogError($"PosCajaController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }
        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoscajaModel>> GetById(string id)
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
                            Data = new PoscajaModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaController(GetById {id}):   {e.Message}", e);
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
                    Data = new PoscajaModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PoscajaModel>> Create([FromBody] PoscajaModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id caja esté vacio
            if (body.Id > 0)
            {
                ErrMsjs.Add("Caja: El campo id de Caja debe tener un valor de identificación válido o debe ser 0");
            }

            //Valida el campo Id esté vacio
            if (body.IdPos < 0)
            {
                ErrMsjs.Add("Pos: El campo idPos debe tener un valor de identificación válido");
            }

            //Valida el campo estado
            if (body.Estado < 0)
            {
                ErrMsjs.Add("Estado: El campo estado debe tener un valor válido");
            }

            //Valida el campo Nombre
            if (body.Nombre == null || body.Nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo Nombre no existe o contiene un valor vacio");
            }

            //Valida el campo debe ser sacado del token

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
                            Data = new PoscajaModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Caja: Ya existe una caja en el mismo POS y con igual nombre que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PoscajaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaController(Post): {e.Message}", e);
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
                    Data = new PoscajaModel[] { }
                });
            }
        }

        [HttpPut("{id}/{idpos}/{nombre}")]
        public async Task<ActionResult<PoscajaModel>> Update(string id, string idpos, string nombre, [FromBody] PoscajaModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Caja: El campo Id caja no existe o no contiene un valor válido");
            }

            //Valida el id sea igual al de body
            if (Idd != body.Id)
            {
                ErrMsjs.Add("Caja: El campo-parámetro Id caja no coincide con el id del body");
            }

            esNumerico = int.TryParse(idpos, out int IdPos);
            //Valida el idPos y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Pos: El campo IdPos no existe o no contiene un valor válido");
            }

            //Valida el id sea igual al de body
            if (IdPos != body.IdPos)
            {
                ErrMsjs.Add("Pos: El campo-parámetro IdPos no coincide con el idPos del body");
            }

            //Valida el campo Nombre
            if (nombre == null || nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo-parámetro Nombre no existe o contiene un valor vacio");
            }
            //Valida el campo Nombre
            if (!nombre.Equals(body.Nombre))
            {
                ErrMsjs.Add("Nombre: El campo-parámetro Nombre no coincide con el campo nombre del body");
            }


            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Trunca el tamaño de campos si son muy largos
                    body.Nombre = body.Nombre.Length > 32 ? body.Nombre.Substring(0, 32) : body.Nombre;
                    body.Descripcion = body.Descripcion.Length > 1024 ? body.Descripcion.Substring(0, 1024) : body.Descripcion;


                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new PoscajaModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No encontrado" },
                            Data = new PoscajaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosCajaController(Put {id}, {idpos}, {nombre}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PoscajaModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PoscajaModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PoscajaModel>> Delete(string id)
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
                            Data = new PoscajaModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosCajaController(Delete {id}):   {e.Message}", e);
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
                    Data = new PoscajaModel[] { }
                });
            }
        }

    }
}
