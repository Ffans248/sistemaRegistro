using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaRegistro
{
    public static class Session
    {
            
            public static int UsuarioID { get; set; }
            public static string NombreUsuario { get; set; }
            public static string Correo { get; set; }
            public static string Rol { get; set; }
            public static Boolean Estado { get; set; }


    }
}
