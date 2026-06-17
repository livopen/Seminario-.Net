using System;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record AgregarExpedienteResponse(Guid IdExpediente, string Caratula, DateTime FechaCreacion);