using System;
using System.Collections.Generic;
using System.IO;
using DCXAirChallenge.Domain.Entities;
using Newtonsoft.Json;

namespace DCXAirChallenge.Infrastructure.Services
{
    /// Servicio encargado de cargar rutas desde un archivo JSON.

    public class RouteLoaderService
    {
        private List<Routes> _routes; // Variable miembro para almacenar las rutas

        public RouteLoaderService()
        {
            LoadRoutesFromFile(); // Cargar las rutas al inicializar el servicio
        }

        /// Carga las rutas desde un archivo JSON y las almacena en memoria.
        /// <returns>Lista de objetos Routes.</returns>
        private void LoadRoutesFromFile()
        {
            try
            {
                var json = File.ReadAllText("Data/markets.json");
                _routes = JsonConvert.DeserializeObject<List<Routes>>(json);
            }
            catch (FileNotFoundException)
            {
                // Manejo de archivo no encontrado
                throw new FileNotFoundException("El archivo especificado no se encontró.");
            }
            catch (JsonException)
            {
                // Manejo de error de deserialización JSON
                throw new JsonException("El archivo JSON no tiene un formato válido.");
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                throw new Exception("Ocurrió un error al cargar las rutas.", ex);
            }
        }

        /// Obtiene las rutas previamente cargadas.
        /// <returns>Lista de objetos Routes.</returns>
        public List<Routes> GetRoutes()
        {
            if (_routes == null)
            {
                // Si las rutas no han sido cargadas, cargarlas ahora
                LoadRoutesFromFile();
            }
            return _routes;
        }
    }
}
