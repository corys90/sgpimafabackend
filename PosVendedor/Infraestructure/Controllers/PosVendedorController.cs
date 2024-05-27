
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.Models;
using sgpimafaback.PosClientes.Domain.Entities;
using sgpimafaback.PosVendedor.Domain.Services;

namespace sgpimafaback.PosVendedor.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosVendedorController : ControllerBase
    {

        private readonly PosvendedorServices _Getlist;
        private readonly ILogger<PosVendedorController> _logger;

        public PosVendedorController(PosvendedorServices getList, ILogger<PosVendedorController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosvendedorModel>>> Get()
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
                _logger.LogError($"PosvendedorController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<PosvendedorModel>> GetById(string id)
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
                            Data = new PosvendedorModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosvendedorModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosvendedorController(GetById {id}):   {e.Message}", e);
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
                    Data = new PosvendedorModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PosvendedorModel>> Create([FromBody] PosvendedorModel body)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el campo Id esté vacio
            if (body.Id != 0)
            {
                ErrMsjs.Add("Id:El campo id no debe tener un valor. Este debe ser 0");
            }
            //Valida el campo Tipo Identificación
            if ((body.TipoIdVendedor == null) || (body.TipoIdVendedor < 0))
            {
                ErrMsjs.Add("Tipo Id:El campo Tipo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdVendedor == null) || (body.IdVendedor < 0))
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
                            Data = new PosvendedorModel[] { resultado }
                        };
                        return Created("Creado", response);
                    }
                    else
                    {
                        ErrMsjs.Add("Vendedor: Ya existe un Vendedor con la misma ientificación que intenta crear.");
                        return BadRequest(new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Messages = ErrMsjs,
                            Data = new PosvendedorModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PosvendedorController(Post): {e.Message}", e);
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

        [HttpPut("{id}/{idVendedor}")]
        public async Task<ActionResult<PosvendedorModel>> Update(string id, string idVendedor, [FromBody] PosvendedorModel body)
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

            esNumerico = int.TryParse(idVendedor, out int Idv);

            //Valida el id y que contenga un valor númerico
            if (!esNumerico)
            {
                ErrMsjs.Add("Identificación: El campo identificación del vendedor no existe o no contiene un valor válido");
            }

            // Valida que el id del parámetro sea igual a idVendedor del body
            if (Idv != body.IdVendedor)
            {
                ErrMsjs.Add("Error: El campo identificación del vendedor no coincide con el idvendedor del body");
            }

            //Valida el campo Tipo Identificación
            if ((body.TipoIdVendedor == null) || (body.TipoIdVendedor < 0))
            {
                ErrMsjs.Add("Tipo Id:El campo Tipo Identificación no existe o contiene un valor no válido");
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
                ErrMsjs.Add("Dirección: El campo Dirección no existe o contiene un valor vacio");
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

            if (ErrMsjs.Count <= 0)
            {
                try
                {
                    // Trunca el tamaño de campos si son muy largos
                    body.Nombres = body.Nombres.Length > 32 ? body.Nombres.Substring(0, 32) : body.Nombres;
                    body.Apellidos = body.Apellidos.Length > 32 ? body.Apellidos.Substring(0, 32) : body.Apellidos;
                    body.Direccion = body.Direccion.Length > 256 ? body.Direccion.Substring(0, 256) : body.Direccion;


                    var resultado = _Getlist.Update(body);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.Accepted,
                            Messages = Array.Empty<string>(),
                            Data = new PosvendedorModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PosvendedorModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosvendedorController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PosvendedorModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PosvendedorModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PosvendedorModel>> Delete(string id)
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
                            Data = new PosvendedorModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PosvendedorController(Delete {id}):   {e.Message}", e);
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
                    Data = new PosvendedorModel[] { }
                });
            }
        }

    }
}