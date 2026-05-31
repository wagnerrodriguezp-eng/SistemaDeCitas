// OCP + DIP: Abstracción que permite agregar nuevos canales de notificación
// sin modificar el código existente (Correo, SMS, WhatsApp, etc.)

namespace SistemaCitas.Dominio.Interfaces
{
    public interface INotificador
    {
        void EnviarRecordatorio(Entidades.Cita cita);
    }
}
