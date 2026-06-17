using System;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion;

namespace SGE.Aplicacion.Expedientes;
public class AgregarExpedienteUseCase
{
    private readonly IExpedienteRepository repositorio;
    private readonly IAutorizacionService ServicioAutorizacion;

    public AgregarExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService servicioAutorizacion)
    {
        this.repositorio = repositorio;
        this.ServicioAutorizacion = servicioAutorizacion;
    }
    
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        if (!this.ServicioAutorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteAlta.ToString()))
            throw new AutorizacionException("No autorizado.");

        var caratula = new CaratulaExpendiente(request.Caratula);
        var expediente = new Expediente(caratula, request.IdUsuario);

        repositorio.Agregar(expediente);

        return new AgregarExpedienteResponse(expediente.Id, expediente.Caratula.Valor, expediente.FechaCreacion);
    }
}