// SRP: Solo gestiona la lógica de negocio de pacientes
// DIP: Depende de la abstracción IRepositorioPaciente
using SistemaCitas.Aplicacion.Validadores;
using SistemaCitas.Compartido.Excepciones;
using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Aplicacion.Servicios
{
    public class ServicioPaciente
    {
        private readonly IRepositorioPaciente _repositorio;
        private int _contadorIdentificador = 1;

        public ServicioPaciente(IRepositorioPaciente repositorio)
        {
            _repositorio = repositorio;
        }

        public Paciente Registrar(string nombre, string telefono, string correoElectronico)
        {
            // DRY: Usa el validador centralizado en lugar de repetir condiciones
            Validador.ValidarTextoRequerido(nombre, "Nombre");
            Validador.ValidarTextoRequerido(telefono, "Teléfono");
            Validador.ValidarCorreoElectronico(correoElectronico);

            var paciente = new Paciente(_contadorIdentificador++, nombre, telefono, correoElectronico);
            _repositorio.Agregar(paciente);
            return paciente;
        }

        public Paciente ObtenerPorIdentificador(int identificador)
        {
            Validador.ValidarIdentificador(identificador, "paciente");
            return _repositorio.ObtenerPorIdentificador(identificador)
                ?? throw new ExcepcionEntidadNoEncontrada("Paciente", identificador);
        }

        public IEnumerable<Paciente> ObtenerTodos() => _repositorio.ObtenerTodos();
    }
}
