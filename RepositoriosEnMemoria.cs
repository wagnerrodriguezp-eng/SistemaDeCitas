// KISS: Repositorios simples en memoria (sin base de datos en versión 1)
// OCP: Si se necesita persistencia real, se crea una nueva implementación sin tocar los servicios
using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Infraestructura.Repositorios
{
    public class RepositorioPacienteEnMemoria : IRepositorioPaciente
    {
        private readonly List<Paciente> _pacientes = new();

        public void Agregar(Paciente paciente) => _pacientes.Add(paciente);

        public Paciente? ObtenerPorIdentificador(int identificador) =>
            _pacientes.FirstOrDefault(p => p.Identificador == identificador);

        public IEnumerable<Paciente> ObtenerTodos() => _pacientes.AsReadOnly();
    }

    public class RepositorioMedicoEnMemoria : IRepositorioMedico
    {
        private readonly List<Medico> _medicos = new();

        public void Agregar(Medico medico) => _medicos.Add(medico);

        public Medico? ObtenerPorIdentificador(int identificador) =>
            _medicos.FirstOrDefault(m => m.Identificador == identificador);

        public IEnumerable<Medico> ObtenerTodos() => _medicos.AsReadOnly();
    }

    public class RepositorioEspecialidadEnMemoria : IRepositorioEspecialidad
    {
        private readonly List<Especialidad> _especialidades = new();

        public void Agregar(Especialidad especialidad) => _especialidades.Add(especialidad);

        public Especialidad? ObtenerPorIdentificador(int identificador) =>
            _especialidades.FirstOrDefault(e => e.Identificador == identificador);

        public IEnumerable<Especialidad> ObtenerTodas() => _especialidades.AsReadOnly();
    }

    public class RepositorioCitaEnMemoria : IRepositorioCita
    {
        private readonly List<Cita> _citas = new();

        public void Agregar(Cita cita) => _citas.Add(cita);

        public Cita? ObtenerPorIdentificador(int identificador) =>
            _citas.FirstOrDefault(c => c.Identificador == identificador);

        public IEnumerable<Cita> ObtenerPorPaciente(int identificadorPaciente) =>
            _citas.Where(c => c.Paciente.Identificador == identificadorPaciente);

        public IEnumerable<Cita> ObtenerPorMedico(int identificadorMedico) =>
            _citas.Where(c => c.Medico.Identificador == identificadorMedico);

        public IEnumerable<Cita> ObtenerTodas() => _citas.AsReadOnly();
    }
}
