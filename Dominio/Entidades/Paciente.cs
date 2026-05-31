namespace SistemaCitas.Dominio.Entidades
{
    // SRP: Solo representa los datos de un paciente
    public class Paciente
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;

        public Paciente(int identificador, string nombre, string telefono, string correoElectronico)
        {
            Identificador = identificador;
            Nombre = nombre;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
        }
    }
}
