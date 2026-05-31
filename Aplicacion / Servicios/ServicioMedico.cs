// SRP: Solo gestiona la lógica de negocio de médicos
// DIP: Depende de abstracciones, no de implementaciones concretas
using SistemaCitas.Aplicacion.Validadores;
using SistemaCitas.Compartido.Excepciones;
using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Aplicacion.Servicios
{
    public class ServicioMedico
    {
        private readonly IRepositorioMedico _repositorio;
        private readonly ServicioEspecialidad _servicioEspecialidad;
        private int _contadorIdentificador = 1;

        public ServicioMedico(IRepositorioMedico repositorio, ServicioEspecialidad servicioEspecialidad)
        {
            _repositorio = repositorio;
            _servicioEspecialidad = servicioEspecialidad;
        }

        public Medico Registrar(string nombre, string correoElectronico, int identificadorEspecialidad)
        {
            // DRY: Usa el validador centralizado
            Validador.ValidarTextoRequerido(nombre, "Nombre");
            Validador.ValidarCorreoElectronico(correoElectronico);
            Validador.ValidarIdentificador(identificadorEspecialidad, "especialidad");

            var especialidad = _servicioEspecialidad.ObtenerPorIdentificador(identificadorEspecialidad);
            var medico = new Medico(_contadorIdentificador++, nombre, correoElectronico, especialidad);
            _repositorio.Agregar(medico);
            return medico;
        }

        public Medico ObtenerPorIdentificador(int identificador)
        {
            Validador.ValidarIdentificador(identificador, "médico");
            return _repositorio.ObtenerPorIdentificador(identificador)
                ?? throw new ExcepcionEntidadNoEncontrada("Médico", identificador);
        }

        public IEnumerable<Medico> ObtenerTodos() => _repositorio.ObtenerTodos();
    }
}
