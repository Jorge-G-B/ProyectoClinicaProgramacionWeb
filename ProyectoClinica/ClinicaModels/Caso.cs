using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaModels
{
    public class Caso
    {
        public int Id { get; set; }

        public DateTime FechaDeApertura { get; set; }

        public int UsuarioCrea { get; set; }

        public int Idpaciente { get; set; }

        public string MotivoConsulta { get; set; } = null!;

        public string Antecedentes { get; set; } = null!;

        public string Diagnostico { get; set; } = null!;

        public string ReferidoPor { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime FechaDeCierre { get; set; }

        public string MotivoDeCierre { get; set; } = null!;
    }
}
