namespace SistemaCitas.Dominio.Entidades
{
    // SRP: Solo representa los datos de un médico
    public class Medico
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public Especialidad Especialidad { get; set; }

        public Medico(int identificador, string nombre, string correoElectronico, Especialidad especialidad)
        {
            Identificador = identificador;
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            Especialidad = especialidad;
        }
    }
}
