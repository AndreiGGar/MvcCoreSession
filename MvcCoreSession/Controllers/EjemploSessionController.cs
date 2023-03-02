using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        int numero = 1;

        public IActionResult Index()
        {
            ViewData["NUMERO"] = "Valor del número: " + this.numero;
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionPersona(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Persona persona = new Persona();
                    persona.Nombre = "Alumnno";
                    persona.Email = "Alumno@gmail.es";
                    persona.Edad = 25;
                    byte[] data = HelperBinarySession.ObjectToByte(persona);
                    HttpContext.Session.Set("PERSONA", data);
                    ViewData["MENSAJE"] = "Datos almacenados";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("PERSONA");
                    Persona persona = (Persona)HelperBinarySession.ByteToObject(data);
                    ViewData["PERSONA"] = persona;
                }
            }
            return View();
        }

        public IActionResult ColeccionPersonas(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Persona> personas = new List<Persona>
                {
                    new Persona { Nombre = "John", Email = "John@g.es", Edad = 23},
                    new Persona { Nombre = "Sandra", Email = "sandra@gm.es", Edad = 28 },
                    new Persona { Nombre = "ASD", Email = "asdd@g.ees", Edad = 21}
                };
                    byte[] data = HelperBinarySession.ObjectToByte(personas);
                    HttpContext.Session.Set("PERSONAS", data);
                    ViewData["MENSAJE"] = "Datos almacenados";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("PERSONAS");
                    List<Persona> personas = (List<Persona>)HelperBinarySession.ByteToObject(data);
                    return View(personas);
                }
            }
            return View();
        }

        public IActionResult SessionPersonaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Persona persona = new Persona();
                    persona.Nombre = "Alumnno";
                    persona.Email = "Alumno@gmail.es";
                    persona.Edad = 25;
                    string jsonPersona = HelperJsonSession.SerializeObject<Persona>(persona);
                    HttpContext.Session.SetString("PERSONA", jsonPersona);
                    ViewData["MENSAJE"] = "Datos almacenados";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string jsonPersona = HttpContext.Session.GetString("PERSONA");
                    Persona persona = HelperJsonSession.DeserializeObject<Persona>(jsonPersona);
                    ViewData["PERSONA"] = persona;
                }
            }
            return View();
        }
    }
}
