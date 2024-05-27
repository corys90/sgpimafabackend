
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.SedePos.Domain.Entities;
using sgpimafaback.SedePos.Domain.Services;

namespace sgpimafaback.SedePos.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SedePosController : ControllerBase
    {

        private readonly SedeposServices _Getlist;
        private readonly ILogger<SedePosController> _logger;

        public SedePosController(SedeposServices getList, ILogger<SedePosController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SedeposModel>>> Get()
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
                _logger.LogError($"SedeposController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<SedeposModel>> GetById(string id)
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
                            Data = new SedeposModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new SedeposModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosTipoEstadoPosCajaController(GetById {id}):   {e.Message}", e);
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
                    Data = new SedeposModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<SedeposModel>> Create([FromBody] SedeposModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }

            //Valida el campo Nombre
            if (body.Nombre == null || body.Nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo Nombre no existe o contiene un valor vacio");
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
                            Data = new SedeposModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Nombre: Ya existe una sede POS con el mismo nombre que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new SedeposModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"SedeposController(Post): {e.Message}", e);
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
                    Data = new SedeposModel[] { }
                });
            }
        }


        [HttpPut("{id}/{nombre}")]
        public async Task<ActionResult<SedeposModel>> Update(string id, string nombre, [FromBody] SedeposModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int IdTipo);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id: El campo Id no existe o no contiene un valor válido");
            }

            // Valida que el id del parámetro sea igual a id del body
            if (IdTipo != body.Id)
            {
                ErrMsjs.Add("Error: El campo id del parámetro no coincide con el id del body");
            }

            //Valida el campo Nombre
            if (nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo Nombre no existe o contiene un valor vacio");
            }

            // Valida que el id del parámetro sea igual a id del body
            if (!nombre.Equals(body.Nombre))
            {
                ErrMsjs.Add("Error: El campo nombre del parámetro no coincide con el nombre del body");
            }

            //Valida el campo Nombre
            if (body.Nombre == null)
            {
                ErrMsjs.Add("Nombre: El campo Nombre no existe o contiene un valor vacio");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Trunca el tamaño de campos si son muy largos
                    body.Nombre = body.Nombre.Length > 32 ? body.Nombre.Substring(0, 32) : body.Nombre;
                    body.Descripcion = body.Descripcion.Length > 256 ? body.Descripcion.Substring(0, 256) : body.Descripcion;


                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new SedeposModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new SedeposModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"SedeposController(Put {id}, {nombre}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new SedeposModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new SedeposModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SedeposModel>> Delete(string id)
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
                            Data = new SedeposModel[] { }
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
                    Data = new SedeposModel[] { }
                });
            }
        }

    }
}