using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosCajaArqueo.Domain.Entities;
using sgpimafaback.PosCajaArqueo.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosCajaArqueo.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosCajaArqueoController : ControllerBase
    {

        private readonly PosCajaArqueoServices _Getlist;
        private readonly ILogger<PosCajaArqueoController> _logger;

        public PosCajaArqueoController(PosCajaArqueoServices getList, ILogger<PosCajaArqueoController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        // GET: api/<PosCajaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoscajaarqueoModel>>> Get()
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
                _logger.LogError($"PosCajaArqueoController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }
        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PoscajaarqueoModel>> GetById(string id)
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
                            Data = new PoscajaarqueoModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajaarqueoModel[] { }
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
                    Data = new PoscajaarqueoModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PoscajaarqueoModel>> Create([FromBody] PoscajaarqueoModel body)
        {
            List<string> ErrMsjs = new List<string>();


            //Valida el campo IdCaja esté vacio
            if (body.IdCaja < 0)
            {
                ErrMsjs.Add("Caja:El campo id de Caja debe tener un valor de identificación válido");
            }

            //Valida el campo IdPos esté vacio
            if (body.IdPos < 0)
            {
                ErrMsjs.Add("Pos:El campo idPos debe tener un valor de identificación válido");
            }

            //Valida el campo valor
            if (body.Valor < 0)
            {
                ErrMsjs.Add("Valor:El campo valor debe tener un valor válido");
            }

            //Valida el campo fechaArqueo
            if (body.FechaArqueo.ToString().Equals(""))
            {
                ErrMsjs.Add("Fecha arqueo:El campo fecha arqueo debe tener una fecha válida");
            }

            //Valida el campo estado
            if (body.RevisorId < 0)
            {
                ErrMsjs.Add("Revisor:El campo revisor debe tener un valor válido");
            }

            //Valida el campo estado
            if (body.EstadoArqueo < 0)
            {
                ErrMsjs.Add("Estado: El campo Estado del arqueo debe tener un valor válido");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Asigna el usuario del token al user del body

                    var resultado = _Getlist.Create(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Created,
                            Messages = Array.Empty<string>(),
                            Data = new PoscajaarqueoModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        return new ContentResult
                        {
                            StatusCode = (int?)HttpStatusCode.NotImplemented,
                            Content = "Error interno del servidor (Not Implemented)",
                        };
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaArqueoController(Post): {e.Message}", e);
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
                    Data = new PoscajaarqueoModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PoscajaarqueoModel>> Update(string id, [FromBody] PoscajaarqueoModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int IdTipo);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Caja:El campo Id caja no existe o no contiene un valor válido");
            }

            //Valida el id sea igual al de body
            if (IdTipo != body.Id)
            {
                ErrMsjs.Add("Caja:El campo-parámetro Id caja no coincide con el id del body");
            }

            //Valida el campo IdCaja esté vacio
            if (body.IdCaja < 0)
            {
                ErrMsjs.Add("Caja:El campo id de Caja debe tener un valor de identificación válido");
            }

            //Valida el campo IdPos esté vacio
            if (body.IdPos < 0)
            {
                ErrMsjs.Add("Pos:El campo idPos debe tener un valor de identificación válido");
            }

            //Valida el campo valor
            if (body.Valor < 0)
            {
                ErrMsjs.Add("Valor:El campo valor debe tener un valor válido");
            }

            //Valida el campo fechaArqueo
            if (body.FechaArqueo.ToString().Equals(""))
            {
                ErrMsjs.Add("Fecha arqueo:El campo fecha arqueo debe tener una fecha válida");
            }

            //Valida el campo estado
            if (body.RevisorId < 0)
            {
                ErrMsjs.Add("Revisor:El campo revisor debe tener un valor válido");
            }

            //Valida el campo estado
            if (body.EstadoArqueo < 0)
            {
                ErrMsjs.Add("Estado: El campo Estado del arqueo debe tener un valor válido");
            }


            if (ErrMsjs.Count <= 0)
            {
                try
                {

                    // asigna el usuario del token al user del body
                    //body.User = body.User.Length > 16 ? body.User.Substring(0, 16) : body.User;

                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new PoscajaarqueoModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No encontrado" },
                            Data = new PoscajaarqueoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosCajaArqueoController(Put {id}):   {e.Message}", e);
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PoscajaarqueoModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PoscajaarqueoModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PoscajaarqueoModel>> Delete(string id)
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
                            Data = new PoscajaarqueoModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {

                    _logger.LogError($"PosCajaArqueoController(Delete {id}):   {e.Message}", e);
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
                    Data = new PoscajaarqueoModel[] { }
                });
            }
        }

    }
}
