// DIP: Las dependencias concretas se ensamblan aquí (Raíz de Composición)
// KISS: Configuración clara, directa y sin frameworks externos
using SistemaCitas.Aplicacion.Servicios;
using SistemaCitas.Infraestructura.Notificaciones;
using SistemaCitas.Infraestructura.Repositorios;
using SistemaCitas.Presentacion.Consola;

// ── Raíz de Composición ──────────────────────────────────────────────────────
// Aquí se decide qué implementaciones concretas usar en esta versión.
// Para cambiar el canal de notificación, solo se modifica esta línea:
//   var notificador = new NotificadorCorreo();
//   var notificador = new NotificadorMensajeTexto();
var notificador = new NotificadorConsola();

// Repositorios en memoria (versión 1 — sin base de datos)
var repositorioPaciente    = new RepositorioPacienteEnMemoria();
var repositorioMedico      = new RepositorioMedicoEnMemoria();
var repositorioEspecialidad = new RepositorioEspecialidadEnMemoria();
var repositorioCita        = new RepositorioCitaEnMemoria();

// Servicios de aplicación
var servicioEspecialidad = new ServicioEspecialidad(repositorioEspecialidad);
var servicioMedico       = new ServicioMedico(repositorioMedico, servicioEspecialidad);
var servicioPaciente     = new ServicioPaciente(repositorioPaciente);
var servicioCita         = new ServicioCita(repositorioCita, servicioPaciente, servicioMedico, notificador);

// Presentación
var menu = new MenuConsola(servicioPaciente, servicioMedico, servicioEspecialidad, servicioCita);

menu.Ejecutar();

Console.WriteLine("\n¡Hasta luego!");
