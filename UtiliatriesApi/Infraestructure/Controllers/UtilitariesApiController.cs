
using System.Net;
using Microsoft.AspNetCore.Mvc;
using sgpimafaback.UtiliatriesApi.Domain.Entities;
using sgpimafaback.UtiliatriesApi.Domain.Services;


namespace sgpimafaback.UtiliatriesApi.Infraestructure.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UtiliatriesApiController : ControllerBase
    {

        private readonly utilitariesapiServices _Getlist;
        private readonly ILogger<UtiliatriesApiController> _logger;

        public UtiliatriesApiController(utilitariesapiServices getList, ILogger<UtiliatriesApiController> logger)
        {
            _logger = logger;
            _Getlist = getList;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoTipos>>> Get()
        {

            try
            {
                var response = Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    Messages = Array.Empty<string>(),
                    Data = new {
                        sedes = _Getlist.GetAll().Sedes,
                        unidadMedida = _Getlist.GetAll().UnidadMedida,
                        tipoProducto = _Getlist.GetAll().TipoProducto,
                        tipoPagosAFavor = _Getlist.GetAll().TipoPagosAFavor,
                        tipoIdCliente = _Getlist.GetAll().TipoIdCliente,
                        tipoEstadoPosCaja = _Getlist.GetAll().TipoEstadoPosCaja,
                        tipoEstadoCaja = _Getlist.GetAll().TipoEstadoCaja,
                        tipoEmbalaje = _Getlist.GetAll().TipoEmbalaje,
                        tipoPrdCompuesto = _Getlist.GetAll().TipoPrdCompuesto
                    },
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"UtilitariesApiController(Get):   {e.Message}", e);
                return new ContentResult
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError,
                    Content = "Error: Interno del servidor o BD. Contacte al administrador del sistema",
                };
            }

        }

        // Recibe el Id 
        [HttpGet("{tipo}")]
        public async Task<ActionResult<DtoUtilitariesModel>> GetById(string tipo)
        {
            List<string> ErrMsjs = new List<string>();

            //Valida el id y que contenga un valor númerico
            if (tipo.Trim().Equals(""))
            {
                ErrMsjs.Add("tipo:El campo tipo no existe o no contiene un valor válido");
            }

            //Valida el campo Nombre
            if (ErrMsjs.Count <= 0)
            {

                try
                {

                    var resultado =  _Getlist.GetById(tipo);
                    if (resultado != null)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Messages = Array.Empty<string >(),
                            Data = resultado
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Messages = new string[] { "Dato no econtrado" },
                            Data = new DtoUtilitariesModel[] { }
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"UtilitariesApiController(GetById {tipo}):   {e.Message}", e);
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
                    Data = new DtoUtilitariesModel[] { }
                });
            }
        }

    }
}