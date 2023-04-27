using System;
using System.Collections.Generic;

namespace ProyectoClinica.Models;

public partial class User
{
    public int Id { get; set; }

    public string User1 { get; set; } = null!;

    public short Rol { get; set; }

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<Caso> Casos { get; } = new List<Caso>();

    public virtual Role RolNavigation { get; set; } = null!;
}
