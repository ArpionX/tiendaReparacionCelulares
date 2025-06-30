using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IDispositivoServices
    {
        Task<Dispositivo?> GetByImei(string imei);
        Task<Dispositivo?> GetById(int id);
        Task<List<Dispositivo>> GetAll();
        Task<int> Insert(Dispositivo dispositivo);
        Task<int> Update(Dispositivo dispositivo);
        Task<int> Delete(int id);
    }


    public class DispositivoServices: IDispositivoServices
    {
        private readonly IDispositivoRepository _dispositivoRepository;

        public DispositivoServices(IDispositivoRepository dispositivoRepository)
        {
            _dispositivoRepository = dispositivoRepository;
        }

        public async Task<List<Dispositivo>> GetAll()
        {
            List<Dispositivo> dispositivos = new List<Dispositivo>();
            try
            {
                dispositivos = await _dispositivoRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
            return dispositivos;
        }

        public async Task<Dispositivo?> GetById(int id)
        {
            Dispositivo? dispositivo;
            try
            {
                dispositivo = await _dispositivoRepository.GetById(id); 
            }
            catch (Exception)
            {

                throw;
            }
            return dispositivo;
        }

        public async Task<Dispositivo?> GetByImei(string imei)
        {
            Dispositivo? dispositivo;
            try
            {
                dispositivo = await _dispositivoRepository.GetByImei(imei);
            }
            catch (Exception)
            {

                throw;
            }
            return dispositivo;
        }

        public async Task<int> Insert(Dispositivo dispositivo)
        {
            int result = 0;
            try
            {
                Dispositivo? dispositivoVerificate = await _dispositivoRepository.GetByImei(dispositivo.Imei);

                if (dispositivoVerificate is not null) throw new Exception($"{dispositivo.Imei} ya registrada");

                await _dispositivoRepository.Insert(dispositivo);
                result = await _dispositivoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<int> Update(Dispositivo dispositivo)
        {
            int result = 0;
            try
            {
                Dispositivo? dispositivoVerificate = await _dispositivoRepository.GetByImei(dispositivo.Imei);

                if (dispositivoVerificate is not null && dispositivo.IdDispositivo != dispositivoVerificate.IdDispositivo) throw new Exception($"{dispositivo.Imei} ya registrada");

                _dispositivoRepository.Update(dispositivo);
                result = await _dispositivoRepository.SaveChangesAsync();
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
                Dispositivo? dispositivo = await _dispositivoRepository.GetById(id);

                if (dispositivo is null) throw new Exception("Dispositivo no registrado");

                _dispositivoRepository.Delete(dispositivo);
                result = await _dispositivoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
