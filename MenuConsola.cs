// SoC: La presentación NO contiene lógica de negocio, solo muestra y recibe datos
// KISS: Menú simple y directo
using SistemaCitas.Aplicacion.Servicios;
using SistemaCitas.Compartido.Excepciones;
using SistemaCitas.Dominio.Entidades;

namespace SistemaCitas.Presentacion.Consola
{
    public class MenuConsola
    {
        private readonly ServicioPaciente _servicioPaciente;
        private readonly ServicioMedico _servicioMedico;
        private readonly ServicioEspecialidad _servicioEspecialidad;
        private readonly ServicioCita _servicioCita;

        public MenuConsola(
            ServicioPaciente servicioPaciente,
            ServicioMedico servicioMedico,
            ServicioEspecialidad servicioEspecialidad,
            ServicioCita servicioCita)
        {
            _servicioPaciente = servicioPaciente;
            _servicioMedico = servicioMedico;
            _servicioEspecialidad = servicioEspecialidad;
            _servicioCita = servicioCita;
        }

        public void Ejecutar()
        {
            bool continuar = true;
            while (continuar)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine() ?? "";

                try
                {
                    continuar = ProcesarOpcion(opcion);
                }
                catch (ExcepcionValidacion ex)
                {
                    MostrarError($"Validación: {ex.Message}");
                }
                catch (ExcepcionEntidadNoEncontrada ex)
                {
                    MostrarError($"No encontrado: {ex.Message}");
                }
                catch (ExcepcionDisponibilidad ex)
                {
                    MostrarError($"Disponibilidad: {ex.Message}");
                }
                catch (FormatException)
                {
                    MostrarError("Formato de fecha incorrecto. Use dd/MM/yyyy HH:mm (ejemplo: 25/12/2025 10:30)");
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            Console.WriteLine("\n");
            Console.WriteLine("       SISTEMA DE GESTIÓN DE CITAS      ");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1.  Registrar Especialidad ");
            Console.WriteLine("2.  Registrar Médico                ");
            Console.WriteLine("3.  Registrar Paciente              ");
            Console.WriteLine("4.  Agendar Cita                    ");
            Console.WriteLine("5.  Ver Citas por Paciente          ");
            Console.WriteLine("6.  Ver Citas por Médico            ");
            Console.WriteLine("7.  Cancelar Cita                   ");
            Console.WriteLine("8.  Reprogramar Cita                ");
            Console.WriteLine("9.  Enviar Recordatorio             ");
            Console.WriteLine("10. Listar todo                     ");
            Console.WriteLine("0.  Salir                           ");
            Console.WriteLine("\n");
            Console.Write("Seleccione una opción: ");
        }

        private bool ProcesarOpcion(string opcion)
        {
            switch (opcion)
            {
                case "1":  RegistrarEspecialidad(); break;
                case "2":  RegistrarMedico();       break;
                case "3":  RegistrarPaciente();     break;
                case "4":  AgendarCita();           break;
                case "5":  VerCitasPorPaciente();   break;
                case "6":  VerCitasPorMedico();     break;
                case "7":  CancelarCita();          break;
                case "8":  ReprogramarCita();       break;
                case "9":  EnviarRecordatorio();    break;
                case "10": ListarTodo();            break;
                case "0":  return false;
                default:   MostrarError("Opción no válida."); break;
            }
            return true;
        }

        // ── Acciones del menú ──────────────────────────────────────────────────

        private void RegistrarEspecialidad()
        {
            Console.Write("Nombre de la especialidad: ");
            string nombre = Console.ReadLine() ?? "";
            var especialidad = _servicioEspecialidad.Registrar(nombre);
            MostrarExito($"Especialidad registrada → Nº {especialidad.Identificador} | {especialidad.Nombre}");
        }

        private void RegistrarMedico()
        {
            ListarEspecialidades();
            Console.Write("Nombre del médico: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Correo electrónico: ");
            string correo = Console.ReadLine() ?? "";
            Console.Write("Número de especialidad: ");
            int numeroEspecialidad = int.Parse(Console.ReadLine() ?? "0");

            var medico = _servicioMedico.Registrar(nombre, correo, numeroEspecialidad);
            MostrarExito($"Médico registrado → Nº {medico.Identificador} | Dr. {medico.Nombre} ({medico.Especialidad.Nombre})");
        }

        private void RegistrarPaciente()
        {
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            Console.Write("Correo electrónico: ");
            string correo = Console.ReadLine() ?? "";

            var paciente = _servicioPaciente.Registrar(nombre, telefono, correo);
            MostrarExito($"Paciente registrado → Nº {paciente.Identificador} | {paciente.Nombre}");
        }

        private void AgendarCita()
        {
            ListarPacientes();
            Console.Write("Número del paciente: ");
            int numeroPaciente = int.Parse(Console.ReadLine() ?? "0");

            ListarMedicos();
            Console.Write("Número del médico: ");
            int numeroMedico = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fecha y hora (dd/MM/yyyy HH:mm): ");
            DateTime fechaHora = DateTime.ParseExact(
                Console.ReadLine() ?? "",
                "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);

            var cita = _servicioCita.Agendar(numeroPaciente, numeroMedico, fechaHora);
            MostrarExito($"Cita agendada → Nº {cita.Identificador} | {cita.Paciente.Nombre} con Dr. {cita.Medico.Nombre}");
        }

        private void VerCitasPorPaciente()
        {
            Console.Write("Número del paciente: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");
            MostrarCitas(_servicioCita.ObtenerPorPaciente(numero));
        }

        private void VerCitasPorMedico()
        {
            Console.Write("Número del médico: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");
            MostrarCitas(_servicioCita.ObtenerPorMedico(numero));
        }

        private void CancelarCita()
        {
            Console.Write("Número de la cita a cancelar: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");
            _servicioCita.Cancelar(numero);
            MostrarExito($"Cita Nº {numero} cancelada exitosamente.");
        }

        private void ReprogramarCita()
        {
            Console.Write("Número de la cita a reprogramar: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nueva fecha y hora (dd/MM/yyyy HH:mm): ");
            DateTime nuevaFechaHora = DateTime.ParseExact(
                Console.ReadLine() ?? "",
                "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);

            _servicioCita.Reprogramar(numero, nuevaFechaHora);
            MostrarExito($"Cita Nº {numero} reprogramada para {nuevaFechaHora:dd/MM/yyyy HH:mm}.");
        }

        private void EnviarRecordatorio()
        {
            Console.Write("Número de la cita: ");
            int numero = int.Parse(Console.ReadLine() ?? "0");
            _servicioCita.EnviarRecordatorio(numero);
        }

        private void ListarTodo()
        {
            ListarEspecialidades();
            ListarMedicos();
            ListarPacientes();
        }

        // ── Ayudantes de visualización (DRY: formato reutilizado en varios métodos) ──

        private void ListarEspecialidades()
        {
            Console.WriteLine("\n── Especialidades ──");
            foreach (var especialidad in _servicioEspecialidad.ObtenerTodas())
                Console.WriteLine($"  [Nº {especialidad.Identificador}] {especialidad.Nombre}");
        }

        private void ListarMedicos()
        {
            Console.WriteLine("\n── Médicos ──");
            foreach (var medico in _servicioMedico.ObtenerTodos())
                Console.WriteLine($"  [Nº {medico.Identificador}] Dr. {medico.Nombre} — {medico.Especialidad.Nombre}");
        }

        private void ListarPacientes()
        {
            Console.WriteLine("\n── Pacientes ──");
            foreach (var paciente in _servicioPaciente.ObtenerTodos())
                Console.WriteLine($"  [Nº {paciente.Identificador}] {paciente.Nombre} | {paciente.Telefono}");
        }

        private static void MostrarCitas(IEnumerable<Cita> citas)
        {
            var lista = citas.ToList();
            if (!lista.Any())
            {
                Console.WriteLine("  Sin citas registradas.");
                return;
            }
            foreach (var cita in lista)
                Console.WriteLine(
                    $"  [Nº {cita.Identificador}] {cita.FechaHora:dd/MM/yyyy HH:mm} | " +
                    $"{cita.Paciente.Nombre} → Dr. {cita.Medico.Nombre} | Estado: {cita.Estado}");
        }

        private static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✔ {mensaje}");
            Console.ResetColor();
        }

        private static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✖ Error: {mensaje}");
            Console.ResetColor();
        }
    }
}
