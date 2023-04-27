using System;
using System.Collections.Generic;

namespace APIClinica.Models;

public partial class Consultum
{
    public int Id { get; set; }

    public int Idcaso { get; set; }

    public DateTime FechaDeConsulta { get; set; }

    public string DatosSubjetivos { get; set; } = null!;

    public string DatosObjetivos { get; set; } = null!;

    public string NuevosDatos { get; set; } = null!;

    public string PlanTerapuetico { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Caso IdcasoNavigation { get; set; } = null!;
}
