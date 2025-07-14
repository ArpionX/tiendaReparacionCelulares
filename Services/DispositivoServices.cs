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
        Task<List<Dispositivo>> GetByClienteId(int idCliente);
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
        public async Task<List<Dispositivo>> GetByClienteId(int idCliente) // ¡NUEVO!
        {
            List<Dispositivo> dispositivos = new List<Dispositivo>();
            try
            {
                dispositivos = await _dispositivoRepository.GetByClienteId(idCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return dispositivos;
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
                result = 1;
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
                // Validar que el IMEI no esté duplicado por otro dispositivo (si el IMEI se ha cambiado)
                Dispositivo? dispositivoByImei = await _dispositivoRepository.GetByImei(dispositivo.Imei);
                if (dispositivoByImei is not null && dispositivoByImei.IdDispositivo != dispositivo.IdDispositivo)
                {
                    throw new Exception("El IMEI ya está registrado para otro dispositivo.");
                }

                Dispositivo? existingDispositivo = await _dispositivoRepository.GetById(dispositivo.IdDispositivo);

                if (existingDispositivo is null)
                {
                    throw new Exception("Dispositivo no encontrado para actualizar.");
                }

                existingDispositivo.IdCliente = dispositivo.IdCliente;
                existingDispositivo.Marca = dispositivo.Marca;
                existingDispositivo.Modelo = dispositivo.Modelo;
                existingDispositivo.Imei = dispositivo.Imei;
                existingDispositivo.Color = dispositivo.Color;
                existingDispositivo.AnioFabricacion = dispositivo.AnioFabricacion;
                existingDispositivo.SistemaOperativo = dispositivo.SistemaOperativo;

                _dispositivoRepository.Update(existingDispositivo);
                result = 1;
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
                result = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
