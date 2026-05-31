// DIP: Los servicios dependen de abstracciones, no de implementaciones concretas
// ISP: Interfaces pequeñas y específicas por cada entidad

namespace SistemaCitas.Dominio.Interfaces
{
    public interface IRepositorioPaciente
    {
        void Agregar(Entidades.Paciente paciente);
        Entidades.Paciente? ObtenerPorIdentificador(int identificador);
        IEnumerable<Entidades.Paciente> ObtenerTodos();
    }

    public interface IRepositorioMedico
    {
        void Agregar(Entidades.Medico medico);
        Entidades.Medico? ObtenerPorIdentificador(int identificador);
        IEnumerable<Entidades.Medico> ObtenerTodos();
    }

    public interface IRepositorioEspecialidad
    {
        void Agregar(Entidades.Especialidad especialidad);
        Entidades.Especialidad? ObtenerPorIdentificador(int identificador);
        IEnumerable<Entidades.Especialidad> ObtenerTodas();
    }

    public interface IRepositorioCita
    {
        void Agregar(Entidades.Cita cita);
        Entidades.Cita? ObtenerPorIdentificador(int identificador);
        IEnumerable<Entidades.Cita> ObtenerPorPaciente(int identificadorPaciente);
        IEnumerable<Entidades.Cita> ObtenerPorMedico(int identificadorMedico);
        IEnumerable<Entidades.Cita> ObtenerTodas();
    }
}
