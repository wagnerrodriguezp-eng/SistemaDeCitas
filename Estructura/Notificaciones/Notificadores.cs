// OCP: Cada nuevo canal es una nueva clase, sin modificar las existentes
// LSP: Cada implementación puede sustituir a INotificador sin romper el sistema
// DIP: ServicioCita depende de INotificador, no de NotificadorCorreo directamente

using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Infraestructura.Notificaciones
{
    // Implementación 1: Recordatorio por correo electrónico
    public class NotificadorCorreo : INotificador
    {
        public void EnviarRecordatorio(Cita cita)
        {
            // En producción: integración real con servicio de correo
            Console.WriteLine($"[CORREO] Recordatorio enviado a {cita.Paciente.CorreoElectronico}");
            Console.WriteLine($" Cita con Dr. {cita.Medico.Nombre} el {cita.FechaHora:dd/MM/yyyy HH:mm}");
        }
    }

    // Implementación 2: SMS — se agrega sin tocar NotificadorCorreo ni ServicioCita (OCP)
    public class NotificadorMensajeTexto : INotificador
    {
        public void EnviarRecordatorio(Cita cita)
        {
            Console.WriteLine($"[MENSAJE DE TEXTO] Recordatorio al {cita.Paciente.Telefono}");
            Console.WriteLine($" Cita: Dr. {cita.Medico.Nombre} — {cita.FechaHora:dd/MM/yyyy HH:mm}");
        }
    }

    // Implementación 3: Consola — útil para pruebas y desarrollo
    public class NotificadorConsola : INotificador
    {
        public void EnviarRecordatorio(Cita cita)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[RECORDATORIO] Paciente: {cita.Paciente.Nombre}");
            Console.WriteLine($" Médico:   Dr. {cita.Medico.Nombre} ({cita.Medico.Especialidad.Nombre})");
            Console.WriteLine($" Fecha:    {cita.FechaHora:dd/MM/yyyy HH:mm}");
            Console.ResetColor();
        }
    }
}
