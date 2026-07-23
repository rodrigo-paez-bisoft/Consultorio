using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Usuario;

public class ConsultarUsuariosResponse
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; }
    public string Email { get; set; }
    public string Rol { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
}

// Aplicacion/DTOs/Usuario/EliminarUsuarioResponse.cs
public class EliminarUsuarioResponse
{
    public bool Exitoso { get; set; }
    public string Mensaje { get; set; }
}

// Aplicacion/DTOs/Usuario/ConsultarUsuarioPorIdRequest.cs
public class ConsultarUsuarioPorIdRequest
{
    public int Id { get; set; }
}