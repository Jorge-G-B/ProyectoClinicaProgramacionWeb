using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaModels
{
    public class Usuario
    {
        public int Id { get; set; }

        public string User1 { get; set; } = null!;

        public short Rol { get; set; }

        public string Correo { get; set; } = null!;

        public string Contraseña { get; set; } = null!;
    }
}
