using Microsoft.EntityFrameworkCore;
using sgpimafaback.Context;
using sgpimafaback.Models;
using sgpimafaback.PosUnidadesMedida.Domain.Entities;

namespace sgpimafaback.PosTipoProducto.Domain.Services
{
    public class PosunidadmedidaServices
    {
        public Sgpimafa2Context _DB;

        public PosunidadmedidaServices(Sgpimafa2Context DB)
        {
            _DB = DB;
        }

        public IEnumerable<PosunidadesmedidumModel> GetAll()
        {
            try
            {
                return _DB.Posunidadesmedida.ToList();
            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosunidadesmedidumModel GetById(int id)
        {
            try
            {
                var resultado = _DB.Posunidadesmedida.Find(id);
                if (resultado != null)
                {
                    return resultado;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosunidadesmedidumModel Create(PosunidadesmedidumModel data)
        {
            try
            {
                var prd = _DB.Posunidadesmedida.Where(rec => (rec.Nombre.Equals(data.Nombre)));
                if (prd.Any())
                {
                    return null;
                }

                _DB.Posunidadesmedida.Add(data);
                _DB.SaveChanges();

                // Retorna el objeto con la información de actualizada
                return data;

            }
            catch (Exception e)
            {

                throw new Exception($"Error: Interno del servidor o BD. Contacte al administrador del sistema - ({e.Message})");

            }
        }

        public PosunidadesmedidumModel Update(PosunidadesmedidumModel data)
        {
            try
            {
                var newData = _DB.Posunidadesmedida.Where(rec => (rec.Id == data.Id) && (rec.Nombre.Equals(data.Nombre)));

                if (newData.Count() <= 0)
                {
                    return null;
                }

                _DB.Entry(data).State = EntityState.Modified;
                var ntask = _DB.SaveChanges();

                var oData = _DB.Posunidadesmedida.Find(data.Id);

                // Retorna el objeto con la información de actualizada
                return oData;
            }
            catch (Exception e)
            {
                throw new Exception($"Error: Interno del servidor o BD ({e.Message})");

            }
        }

        public bool Delete(int id)
        {
            try
            {
                var newData = _DB.Posunidadesmedida.Find(id);
                if (newData != null)
                {
                    // Retorna el objeto con la información de actualizada
                    var ntask = _DB.Posunidadesmedida.Remove(newData);
                    _DB.SaveChanges();

                    // Para efectos de auditoria, el user que realiza la operación sale del token del JWT 

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error: Interno del servidor o BD ({e.Message})");

            }
        }

    }
}
