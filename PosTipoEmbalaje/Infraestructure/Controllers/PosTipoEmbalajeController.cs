
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.Models;
using sgpimafaback.PosProductoCompuesto.Domain.Entities;
using sgpimafaback.PosTipoEmbalajeServices.Domain.Services;


namespace sgpimafaback.PosTipoEmbalajeController.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosTipoEmbalajeController : ControllerBase
    {

        private readonly PostipoembalajeServices _Getlist;
        private readonly ILogger<PosTipoEmbalajeController> _logger;

        public PosTipoEmbalajeController(PostipoembalajeServices getList, ILogger<PosTipoEmbalajeController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostipoembalajeModel>>> Get()
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
                _logger.LogError($"PosTipoEstadoPosCajaController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PostipoembalajeModel>> GetById(string id)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor n�merico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor v�lido");
            }

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
                            Data = new PostipoembalajeModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PostipoembalajeModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosTipoEmbalajeController(GetById {id}):   {e.Message}", e);
                    return new ContentResult
                    {
                        StatusCode = (int?)HttpStatusCode.InternalServerError,
                        Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                    };
                }
            }
            else
            {
                return NotFound(new
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Messages = new string[] { "No econtrado" },
                    Data = new PostipoembalajeModel[] { }
                });
            }
        }

        // Crea un tipo de producto con informaci�n recibida en el body
        [HttpPost]
        public async Task<ActionResult<PostipoembalajeModel>> Create([FromBody] PostipoembalajeModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id est� vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }

            //Valida el campo Nombre
            if (body.Nombre == null || body.Nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo Nombre no existe o contiene un valor vacio");
            }

            //Valida el campo User
            if (body.Descripcion == null || body.Descripcion.Trim().Equals(""))
            {
                ErrMsjs.Add("Descripci�n: El campo no existe o contiene un valor vacio");
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
                            Data = new PostipoembalajeModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Nombre: Ya existe un embalaje con el mismo nombre que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PostipoembalajeModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosTipoEmbalajeController(Post): {e.Message}", e);
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
                    Data = new PostipoembalajeModel[] { }
                });
            }
        }

        [HttpPut("{id}/{nombre}")]
        public async Task<ActionResult<PostipoembalajeModel>> Update(string id, string nombre, [FromBody] PostipoembalajeModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int IdTipo);

            //Valida el id y que contenga un valor n�merico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor v�lido");
            }

            // Valida que el id del par�metro sea igual a id del body
            if (IdTipo != body.Id)
            {
                ErrMsjs.Add("Error:El campo id del par�metro no coincide con el id del body");
            }

            //Valida el campo Nombre
            if (nombre.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo Nombre no existe o contiene un valor vacio");
            }

            // Valida que el id del par�metro sea igual a id del body
            if (!nombre.Equals(body.Nombre))
            {
                ErrMsjs.Add("Error:El campo nombre del par�metro no coincide con el nombre del body");
            }

            //Valida el campo Nombre
            if (body.Nombre == null)
            {
                ErrMsjs.Add("Nombre:El campo Nombre no existe o contiene un valor vacio");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Trunca el tama�o de campos si son muy largos
                    body.Nombre = body.Nombre.Length > 32 ? body.Nombre.Substring(0, 32) : body.Nombre;
                    body.Descripcion = body.Descripcion.Length > 256 ? body.Descripcion.Substring(0, 256) : body.Descripcion;

                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new PostipoembalajeModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PostipoembalajeModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosTipoEmbalajeController(Put {id}, {nombre}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PostipoembalajeModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PostipoembalajeModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostipoembalajeModel>> Delete(string id)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int IdTipo);

            //Valida el id y que contenga un valor n�merico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor v�lido");
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
                            Data = new PostipoembalajeModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"\nPosTipoEstadoPosCajaController(Delete {id}:   {e.Message}", e);
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
                    Data = new PostipoembalajeModel[] { }
                });
            }
        }

    }
}