using System;
using System.Collections.Generic;

namespace APIClinica.Models;

public partial class Paciente
{
    public int Id { get; set; }

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

    public virtual ICollection<Caso> Casos { get; set; } = new List<Caso>();
}
