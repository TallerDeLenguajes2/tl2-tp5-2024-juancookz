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
    [HttpPost("api/Presupuesto")]
    public ActionResult Create([FromForm] string nombreDestinatario)
    {
        if (nombreDestinatario == null)
        {
            return BadRequest("Presupuesto inválido.");
        }
        Presupuesto presupuesto = new Presupuesto(nombreDestinatario, DateTime.Now);
        presupuestoRepository.Create(presupuesto);
        return Ok();
    }
    [HttpGet("api/Presupuestos")]
    public ActionResult GetAll()
    {
        return Ok(presupuestoRepository.GetAll());
    }
    [HttpGet("api/Presupuesto/{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(presupuestoRepository.GetById(id));
    }
}
