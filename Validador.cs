// DRY: Toda la lógica de validación centralizada en un solo lugar
// KISS: Métodos simples, cortos y enfocados
using SistemaCitas.Compartido.Excepciones;

namespace SistemaCitas.Aplicacion.Validadores
{
    public static class Validador
    {
        // DRY: Validación de texto reutilizable en todos los servicios
        public static void ValidarTextoRequerido(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ExcepcionValidacion($"El campo '{nombreCampo}' es obligatorio.");
        }

        // DRY: Validación de fecha futura reutilizable al agendar y reprogramar
        public static void ValidarFechaFutura(DateTime fecha)
        {
            if (fecha <= DateTime.Now)
                throw new ExcepcionValidacion("La fecha de la cita debe ser en el futuro.");
        }

        // DRY: Validación de correo electrónico reutilizable
        public static void ValidarCorreoElectronico(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
                throw new ExcepcionValidacion($"El correo '{correo}' no tiene un formato válido.");
        }

        // DRY: Validación de identificador positivo reutilizable
        public static void ValidarIdentificador(int identificador, string nombreEntidad)
        {
            if (identificador <= 0)
                throw new ExcepcionValidacion($"El identificador de {nombreEntidad} debe ser mayor que cero.");
        }
    }
}
