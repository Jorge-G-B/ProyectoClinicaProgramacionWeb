using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaModels
{
    public class Paciente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Pnombre { get; set; } = null!;

        public string Snombre { get; set; } = null!;

        public string Papellido { get; set; } = null!;

        public string Sapellido { get; set; } = null!;

        public int Edad { get; set; }

        public int Telefono { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        public string Email { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        public string NombreResponsable { get; set; } = null!;

        public int TelResponsable { get; set; }
    }
}
