using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IOrdenServicioServices
    {
        Task<OrdenServicio?> GetByNumberOrden(string numberOrden);
        Task<OrdenServicio?> GetById(int id);
        Task<List<OrdenServicio>> GetAll();
        Task<int> Insert(OrdenServicio orden);
        Task<int> Update(OrdenServicio orden);
        Task<int> Delete(int id);
    }
    public class OrdenServicioServices: IOrdenServicioServices
    {
        private readonly IOrdenServicioRepository _ordenRepository;

        public OrdenServicioServices(IOrdenServicioRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }
        

        public async Task<List<OrdenServicio>> GetAll()
        {
            List <OrdenServicio> ordens = new List<OrdenServicio>();
            try
            {
                ordens = await _ordenRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
            return ordens;
        }

        public async Task<OrdenServicio?> GetById(int id)
        {
            OrdenServicio? orden;
            try
            {
                orden = await _ordenRepository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return orden;
        }

        public async Task<OrdenServicio?> GetByNumberOrden(string numberOrden)
        {
            OrdenServicio? orden;
            try
            {
                orden = await _ordenRepository.GetByNumberOrden(numberOrden);
            }
            catch (Exception)
            {

                throw;
            }
            return orden;
        }

        public async Task<int> Insert(OrdenServicio orden)
        {
            int result = 0;
            try
            {
                OrdenServicio? ordenVerificate = await _ordenRepository.GetByNumberOrden(orden.NumeroOrden);

                if (ordenVerificate is not null) throw new Exception($"{orden.NumeroOrden} ya registrada");

                await _ordenRepository.Insert(orden);
                result = await _ordenRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<int> Update(OrdenServicio orden)
        {
            int result = 0;
            try
            {
                OrdenServicio? ordenVerificate = await _ordenRepository.GetByNumberOrden(orden.NumeroOrden);
                
                if (ordenVerificate is not null && orden.IdOrden != ordenVerificate.IdOrden) throw new Exception($"{orden.NumeroOrden} ya registrada");

                _ordenRepository.Update(orden);
                result = await _ordenRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public async Task<int> Delete(int id)
        {
            int result = 0;
            try
            {
                OrdenServicio? orden = await _ordenRepository.GetById(id);

                if (orden is null) throw new Exception("OrdenServicio no registrado");

                _ordenRepository.Delete(orden);
                result = await _ordenRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
