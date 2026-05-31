// SRP: Solo gestiona la lógica de negocio de especialidades
// DIP: Depende de la abstracción IRepositorioEspecialidad
using SistemaCitas.Aplicacion.Validadores;
using SistemaCitas.Compartido.Excepciones;
using SistemaCitas.Dominio.Entidades;
using SistemaCitas.Dominio.Interfaces;

namespace SistemaCitas.Aplicacion.Servicios
{
    public class ServicioEspecialidad
    {
        private readonly IRepositorioEspecialidad _repositorio;
        private int _contadorIdentificador = 1;

        public ServicioEspecialidad(IRepositorioEspecialidad repositorio)
        {
            _repositorio = repositorio;
        }

        public Especialidad Registrar(string nombre)
        {
            // DRY: Usa el validador centralizado
            Validador.ValidarTextoRequerido(nombre, "Nombre");

            var especialidad = new Especialidad(_contadorIdentificador++, nombre);
            _repositorio.Agregar(especialidad);
            return especialidad;
        }

        public Especialidad ObtenerPorIdentificador(int identificador)
        {
            Validador.ValidarIdentificador(identificador, "especialidad");
            return _repositorio.ObtenerPorIdentificador(identificador)
                ?? throw new ExcepcionEntidadNoEncontrada("Especialidad", identificador);
        }

        public IEnumerable<Especialidad> ObtenerTodas() => _repositorio.ObtenerTodas();
    }
}
