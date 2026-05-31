namespace SistemaCitas.Dominio.Entidades
{
    // SRP: Solo representa una cita médica y su estado
    public enum EstadoCita
    {
        Programada,
        Cancelada,
        Reprogramada
    }

    public class Cita
    {
        public int Identificador { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoCita Estado { get; set; }

        public Cita(int identificador, Paciente paciente, Medico medico, DateTime fechaHora)
        {
            Identificador = identificador;
            Paciente = paciente;
            Medico = medico;
            FechaHora = fechaHora;
            Estado = EstadoCita.Programada;
        }

        public void Cancelar() => Estado = EstadoCita.Cancelada;

        public void Reprogramar(DateTime nuevaFechaHora)
        {
            FechaHora = nuevaFechaHora;
            Estado = EstadoCita.Reprogramada;
        }
    }
}
