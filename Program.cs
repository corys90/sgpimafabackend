
using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.PosCaja.Domain.Services;
using sgpimafaback.PosCajaArqueo.Domain.Services;
using sgpimafaback.PosCajaEstado.Domain.Services;
using sgpimafaback.PosCajaPagoFactura.Domain.Services;
using sgpimafaback.PosClientes.Domain.Services;
using sgpimafaback.PosDevolucionProductoVendido.Domain.Services;
using sgpimafaback.PosFacturacionServices.Domain.Services;
using sgpimafaback.PosFacturaDetalle.Domain.Services;
using sgpimafaback.PosMovimientoInventarioServices.Domain.Services;
using sgpimafaback.PosProductoCompuestoServices.Domain.Services;
using sgpimafaback.PosTipoEmbalajeServices.Domain.Services;
using sgpimafaback.PosInventarioProducto.Domain.Services;
using sgpimafaback.PosTipoEstadoCaja.Domain.Services;
using sgpimafaback.PosTipoEstadoPosCaja.Domain.Services;
using sgpimafaback.PosTipoIdCliente.Domain.Services;
using sgpimafaback.PosTipoPagosAFavor.Domain.Services;
using sgpimafaback.PosTipoProducto.Domain.Services;
using sgpimafaback.PosVendedor.Domain.Services;
using sgpimafaback.SedePos.Domain.Services;
using sgpimafaback.InventarioProducto.Domain.Services;


namespace sgpimafaback
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            var DbContextConnection = builder.Configuration.GetConnectionString("Connection");
            if (DbContextConnection is not null)
                builder.Services.AddDbContext<Sgpimafa2Context>(options => options.UseMySQL(DbContextConnection));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Services del proyecto
            builder.Services.AddScoped<PosCajaServices>();            
            builder.Services.AddScoped<PosCajaArqueoServices>();
            builder.Services.AddScoped<PosCajaEstadoServices>();
            builder.Services.AddScoped<PosCajaPagoFacturaServices>();
            builder.Services.AddScoped<PosCajaPagosAFavorServices>();
            builder.Services.AddScoped<ClienteServices>();
            builder.Services.AddScoped<PosDevolucionProductoVendidoServices>();
            builder.Services.AddScoped<PosfacturacionServices>();
            builder.Services.AddScoped<PosFacturaDetalleServices>();
            builder.Services.AddScoped<PosinventarioproductoServices>();
            builder.Services.AddScoped<inventarioproductoServices>();
            builder.Services.AddScoped<posmovimientoinventarioServices>();
            builder.Services.AddScoped<posproductocompuestoServices>();
            builder.Services.AddScoped<PostipoembalajeServices>();
            builder.Services.AddScoped<PostipoestadoscajaServices>();
            builder.Services.AddScoped<PostipoestadosposcajaServices>();
            builder.Services.AddScoped<PostipoidclienteServices>();
            builder.Services.AddScoped<PostipopagosafavorServices>();
            builder.Services.AddScoped<PostipoproductoServices>();
            builder.Services.AddScoped<PosunidadmedidaServices>();
            builder.Services.AddScoped<PosvendedorServices>();
            builder.Services.AddScoped<SedeposServices>();


            //Configuracion CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyCorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
