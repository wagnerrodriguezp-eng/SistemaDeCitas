namespace SistemaCitas.Dominio.Entidades
{
    // SRP: Esta clase solo representa la especialidad médica
    public class Especialidad
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public Especialidad(int identificador, string nombre)
        {
            Identificador = identificador;
            Nombre = nombre;
        }
    }
}
