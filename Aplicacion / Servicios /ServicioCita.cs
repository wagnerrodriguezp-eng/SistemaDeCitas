// SRP: Solo gestiona la lógica de negocio de citas
// DIP: Depende de abstracciones: IRepositorioCita, INotificador
// OCP: Usa INotificador → se pueden agregar más canales sin modificar este servicio
using SistemaCitas.Aplicacion.Validadores;
using SistemaCitas.Compartido.Excepciones;
using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Aplicacion.Servicios
{
    public class ServicioCita
    {
        private readonly IRepositorioCita _repositorioCita;
        private readonly ServicioPaciente _servicioPaciente;
        private readonly ServicioMedico _servicioMedico;
        private readonly INotificador _notificador;
        private int _contadorIdentificador = 1;

        public ServicioCita(
            IRepositorioCita repositorioCita,
            ServicioPaciente servicioPaciente,
            ServicioMedico servicioMedico,
            INotificador notificador)
        {
            _repositorioCita = repositorioCita;
            _servicioPaciente = servicioPaciente;
            _servicioMedico = servicioMedico;
            _notificador = notificador;
        }

        public Cita Agendar(int identificadorPaciente, int identificadorMedico, DateTime fechaHora)
        {
            // DRY: Validaciones centralizadas
            Validador.ValidarIdentificador(identificadorPaciente, "paciente");
            Validador.ValidarIdentificador(identificadorMedico, "médico");
            Validador.ValidarFechaFutura(fechaHora);

            var paciente = _servicioPaciente.ObtenerPorIdentificador(identificadorPaciente);
            var medico = _servicioMedico.ObtenerPorIdentificador(identificadorMedico);

            ValidarDisponibilidadMedico(identificadorMedico, fechaHora);

            var cita = new Cita(_contadorIdentificador++, paciente, medico, fechaHora);
            _repositorioCita.Agregar(cita);
            return cita;
        }

        public void Cancelar(int identificadorCita)
        {
            var cita = ObtenerCitaActiva(identificadorCita);
            cita.Cancelar();
        }

        public void Reprogramar(int identificadorCita, DateTime nuevaFechaHora)
        {
            // DRY: Reutiliza validación de fecha futura
            Validador.ValidarFechaFutura(nuevaFechaHora);

            var cita = ObtenerCitaActiva(identificadorCita);
            ValidarDisponibilidadMedico(cita.Medico.Identificador, nuevaFechaHora,
                excluirIdentificadorCita: identificadorCita);
            cita.Reprogramar(nuevaFechaHora);
        }

        public void EnviarRecordatorio(int identificadorCita)
        {
            var cita = ObtenerCitaActiva(identificadorCita);
            // OCP: El canal de notificación es inyectable sin cambiar este método
            _notificador.EnviarRecordatorio(cita);
        }

        public IEnumerable<Cita> ObtenerPorPaciente(int identificadorPaciente)
        {
            Validador.ValidarIdentificador(identificadorPaciente, "paciente");
            return _repositorioCita.ObtenerPorPaciente(identificadorPaciente);
        }

        public IEnumerable<Cita> ObtenerPorMedico(int identificadorMedico)
        {
            Validador.ValidarIdentificador(identificadorMedico, "médico");
            return _repositorioCita.ObtenerPorMedico(identificadorMedico);
        }

        // KISS: Método privado simple para reutilizar búsqueda + validación de estado activo
        private Cita ObtenerCitaActiva(int identificadorCita)
        {
            Validador.ValidarIdentificador(identificadorCita, "cita");

            var cita = _repositorioCita.ObtenerPorIdentificador(identificadorCita)
                ?? throw new ExcepcionEntidadNoEncontrada("Cita", identificadorCita);

            if (cita.Estado == EstadoCita.Cancelada)
                throw new ExcepcionValidacion($"La cita {identificadorCita} ya fue cancelada.");

            return cita;
        }

        // DRY: Validación de disponibilidad centralizada (usada al agendar y al reprogramar)
        private void ValidarDisponibilidadMedico(int identificadorMedico, DateTime fechaHora,
            int excluirIdentificadorCita = 0)
        {
            var hayConflicto = _repositorioCita.ObtenerPorMedico(identificadorMedico)
                .Any(c => c.Identificador != excluirIdentificadorCita
                       && c.Estado != EstadoCita.Cancelada
                       && c.FechaHora == fechaHora);

            if (hayConflicto)
                throw new ExcepcionDisponibilidad(
                    $"El médico ya tiene una cita programada para {fechaHora:dd/MM/yyyy HH:mm}.");
        }
    }
}
