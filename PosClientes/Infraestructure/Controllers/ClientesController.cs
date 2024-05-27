using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosClientes.Domain.Entities;
using sgpimafaback.PosClientes.Domain.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosClientes.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : ControllerBase
    {

        private readonly ClienteServices _Getlist;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ClienteServices getList, ILogger<ClientesController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> Get()
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
                _logger.LogError($"PoscajapagofacturaController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }
        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> GetById(string id)
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
                            Data = new ClienteModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new ClienteModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"ClientesController(GetById {id}):   {e.Message}", e);
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
                    Data = new ClienteModel[] { }
                });
            }
        }

        // Recibe el Tipo documento y el Nro. identificación
        [HttpGet("GetByIdentification/{td}/{id}")]
        public async Task<ActionResult<ClienteModel>> GetByIdentification(string td, string id)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumericoTd = int.TryParse(td, out int tid);

            //Valida el id y que contenga un valor númerico
            if (!esNumericoTd)
            {
                ErrMsjs.Add("Tipo Doc:El campo Tipo documento no existe o no contiene un valor válido");
            }

            bool esNumericoId = int.TryParse(id, out int Idd);
            //Valida el id y que contenga un valor númerico
            if (!esNumericoId)
            {
                ErrMsjs.Add("Id:El campo Id no existe o no contiene un valor válido");
            }

            //Valida el campo Nombre
            if (ErrMsjs.Count <= 0)
            {

                try
                {

                    var resultado = _Getlist.GetByIdentification(tid, Idd);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Messages = Array.Empty<string>(),
                            Data = new ClienteModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new ClienteModel[] { }
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
                    Data = new ClienteModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<ClienteModel>> Create([FromBody] ClienteModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo Tipo Identificación
            if ((body.TipoIdCliente == null) || (body.TipoIdCliente < 0))
            {
                ErrMsjs.Add("Tipo Id:El campo Tipo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdCliente == null) || (body.IdCliente < 0))
            {
                ErrMsjs.Add("Id:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Nombre
            if (body.Nombres == null || body.Nombres.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo Nombre no existe o contiene un valor vacio");
            }

            //Valida el campo Apellido
            if (body.Apellidos == null || body.Apellidos.Equals(""))
            {
                ErrMsjs.Add("Nombre:El campo Apellido no existe o contiene un valor vacio");
            }

            //Valida el campo Apellido
            if (body.Direccion == null || body.Direccion.Equals(""))
            {
                ErrMsjs.Add("Dirección:El campo Dirección no existe o contiene un valor vacio");
            }

            //Valida el campo Dpto
            if (body.Dpto == null || body.Dpto.Equals(""))
            {
                ErrMsjs.Add("Departamento:El campo Dpto no existe o contiene un valor vacio");
            }

            //Valida el campo Ciudad
            if (body.Ciudad == null || body.Ciudad.Equals(""))
            {
                ErrMsjs.Add("Ciudad:El campo Ciudad no existe o contiene un valor vacio");
            }


            //Valida el campo Ciudad
            if ((body.Email != null) && !body.Email.Trim().Equals("") && !Utilities.IsValidEmail(body.Email))
            {
                ErrMsjs.Add("Email:El campo Email no contiene un email válido");
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
                            Data = new ClienteModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Cliente: Ya existe un Cliente con la misma identificación que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new ClienteModel[] { }
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
                    Data = new ClienteModel[] { }
                });
            }
        }

        [HttpPut("{id}/{idCliente}")]
        public async Task<ActionResult<ClienteModel>> Update(string id, string idCliente, [FromBody] ClienteModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id: El campo Id no existe o no contiene un valor válido");
            }

            // Valida que el id del parámetro sea igual a id del body
            if (Idd != body.Id)
            {
                ErrMsjs.Add("Error: El campo id del parámetro no coincide con el id del body");
            }

            esNumerico = int.TryParse(idCliente, out int IdC);
            //Valida el idCliente y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Id: El campo Id no existe o no contiene un valor válido");
            }

            //Valida el campo Identificación
            if (IdC != body.IdCliente)
            {
                ErrMsjs.Add("Error: El campo idCliente del parámetro no coincide con el idCliente del body");
            }

            //Valida el campo Tipo Identificación
            if ((body.TipoIdCliente == null) || (body.TipoIdCliente < 0))
            {
                ErrMsjs.Add("Tipo Id: El campo Tipo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Nombre
            if (body.Nombres == null || body.Nombres.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo Nombre no existe o contiene un valor vacio");
            }

            //Valida el campo Apellido
            if (body.Apellidos == null || body.Apellidos.Equals(""))
            {
                ErrMsjs.Add("Nombre: El campo Apellido no existe o contiene un valor vacio");
            }

            //Valida el campo Apellido
            if (body.Direccion == null || body.Direccion.Equals(""))
            {
                ErrMsjs.Add("Dirección:El campo Dirección no existe o contiene un valor vacio");
            }

            //Valida el campo Dpto
            if (body.Dpto == null || body.Dpto.Equals(""))
            {
                ErrMsjs.Add("Departamento: El campo Dpto no existe o contiene un valor vacio");
            }

            //Valida el campo Ciudad
            if (body.Ciudad == null || body.Ciudad.Equals(""))
            {
                ErrMsjs.Add("Ciudad: El campo Ciudad no existe o contiene un valor vacio");
            }

            //Valida el campo Ciudad
            if ((body.Email != null) && !body.Email.Trim().Equals("") && !Utilities.IsValidEmail(body.Email))
            {
                ErrMsjs.Add("Email: El campo Email no contiene un email válido");
            }

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Trunca el tamaño de campos si son muy largos
                    body.Nombres = body.Nombres.Length > 32 ? body.Nombres.Substring(0, 32) : body.Nombres;
                    body.Direccion = body.Direccion.Length > 256 ? body.Direccion.Substring(0, 256) : body.Direccion;


                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new ClienteModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new ClienteModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"ClienteController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new ClienteModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new ClienteModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteModel>> Delete(string id)
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
                            Data = new ClienteModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"ClientesController(Delete {id}):   {e.Message}", e);
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
                    Data = new ClienteModel[] { }
                });
            }
        }
    }
}
