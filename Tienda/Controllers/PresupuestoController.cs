using Microsoft.AspNetCore.Mvc;
namespace Tienda.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestoController : ControllerBase
{
    private readonly PresupuestoRepository presupuestoRepository;

    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
    }
    [HttpPost]
    public ActionResult CrearPresupuesto([FromForm] string nombreDestinatario)
    {
        if (nombreDestinatario == null)
        {
            return BadRequest("Presupuesto inválido.");
        }
        Presupuesto presupuesto = new Presupuesto(nombreDestinatario, DateTime.Now);
        presupuestoRepository.Create(presupuesto);
        return Ok();
    }
    [HttpGet]
    public ActionResult ObtenerPresupuestos()
    {
        return Ok(presupuestoRepository.GetAll());
    }
}
