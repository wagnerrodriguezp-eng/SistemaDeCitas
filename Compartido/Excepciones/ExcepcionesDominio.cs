namespace SistemaCitas.Compartido.Excepciones
{
    // KISS: Excepciones claras y descriptivas para el dominio

    public class ExcepcionEntidadNoEncontrada : Exception
    {
        public ExcepcionEntidadNoEncontrada(string nombreEntidad, int identificador)
            : base($"{nombreEntidad} con identificador {identificador} no fue encontrado.") { }
    }

    public class ExcepcionValidacion : Exception
    {
        public ExcepcionValidacion(string mensaje) : base(mensaje) { }
    }

    public class ExcepcionDisponibilidad : Exception
    {
        public ExcepcionDisponibilidad(string mensaje) : base(mensaje) { }
    }
}
