using Microsoft.AspNetCore.Mvc;
using sgpimafaback.PosCajaPagoFactura.Domain.Entities;
using sgpimafaback.PosCajaPagoFactura.Domain.Services;
using sgpimafaback.PosCajaPagosAFavor.Domain.Entities;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgpimafaback.PosCajaPagosAFavor.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PosCajaPagosAFavorController : ControllerBase
    {

        private readonly PosCajaPagosAFavorServices _Getlist;
        private readonly ILogger<PosCajaPagosAFavorController> _logger;

        public PosCajaPagosAFavorController(PosCajaPagosAFavorServices getList, ILogger<PosCajaPagosAFavorController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoscajapagosafavorModel>>> Get()
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
        public async Task<ActionResult<PoscajapagosafavorModel>> GetById(string id)
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
                            Data = new PoscajapagosafavorModel[] { resultado }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajapagosafavorModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"PoscajapagofacturaController(GetById {id}):   {e.Message}", e);
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
                    Data = new PoscajapagosafavorModel[] { }
                });
            }
        }

        // Crea un tipo de producto con información recibida en el body
        [HttpPost]
        public async Task<ActionResult<PoscajapagosafavorModel>> Create([FromBody] PoscajapagosafavorModel body)
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
                ErrMsjs.Add("Pos:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdCaja == null) || (body.IdCaja < 0))
            {
                ErrMsjs.Add("Caja:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo id caja
            if ((body.IdFactura == null) || (body.IdFactura < 0))
            {
                ErrMsjs.Add("Factura:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdPago == null) || (body.IdPago < 0))
            {
                ErrMsjs.Add("Pago:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Devuelto
            if (body.SaldoAfavor == null || body.SaldoAfavor <= 0)
            {
                ErrMsjs.Add("Saldo:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo estado
            if (body.Estado == null || body.Estado <= 0)
            {
                ErrMsjs.Add("Estado:El campo no existe o contiene un valor no válido");
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
                        Data = new PoscajapagosafavorModel[] { resultado }
                    };
                    return Created("Creado", response);
                }
                catch (Exception e)
                {
                    _logger.LogError($"PoscajapagosafavorController(Post): {e.Message}", e);
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
                    Data = new PoscajapagosafavorModel[] { }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PoscajapagosafavorModel>> Update(string id, [FromBody] PoscajapagosafavorModel body)
        {
            List<string> ErrMsjs = new List<string>();
            bool esNumerico = int.TryParse(id, out int Idd);


            //Valida el campo Tipo Identificación
            if ((body.IdPos == null) || (body.IdPos < 0))
            {
                ErrMsjs.Add("Pos:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdCaja == null) || (body.IdCaja < 0))
            {
                ErrMsjs.Add("Caja:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo id caja
            if ((body.IdFactura == null) || (body.IdFactura < 0))
            {
                ErrMsjs.Add("Factura:El campo Id caja no existe o contiene un valor no válido");
            }

            //Valida el campo Identificación
            if ((body.IdPago == null) || (body.IdPago < 0))
            {
                ErrMsjs.Add("Pago:El campo Identificación no existe o contiene un valor no válido");
            }

            //Valida el campo Devuelto
            if (body.SaldoAfavor == null || body.SaldoAfavor <= 0)
            {
                ErrMsjs.Add("Saldo:El campo no existe o contiene un valor no válido");
            }

            //Valida el campo estado
            if (body.Estado == null || body.Estado <= 0)
            {
                ErrMsjs.Add("Estado:El campo no existe o contiene un valor no válido");
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
                            Data = new PoscajapagosafavorModel[] { resultado }
                        };
                        return Accepted(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "No econtrado" },
                            Data = new PoscajapagosafavorModel[] { }
                        }); ;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PoscajapagosafavorController(Put {id}):   {e.Message}", e);
                    _logger.LogError("\n ");
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Messages = new string[] { "Error interno del servidor o BD" },
                        Data = new PoscajapagosafavorModel[] { }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Messages = ErrMsjs,
                    Data = new PoscajapagofacturaModel[] { }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PoscajapagosafavorModel>> Delete(string id)
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
                            Data = new PoscajapagosafavorModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("\n ");
                    _logger.LogError($"PoscajapagosafavorController(Delete {id}):   {e.Message}", e);
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
                    Data = new PoscajapagofacturaModel[] { }
                });
            }
        }
    }
}
