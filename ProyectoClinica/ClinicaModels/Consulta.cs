using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaModels
{
    public class Consulta
    {
        public int Id { get; set; }

        public int Idcaso { get; set; }

        public DateTime FechaDeConsulta { get; set; }

        public string DatosSubjetivos { get; set; } = null!;

        public string DatosObjetivos { get; set; } = null!;

        public string PlanTerapuetico { get; set; } = null!;

        public string NuevosDatos { get; set; } = null!;

        public string Estado { get; set; } = null!;
    }
}
