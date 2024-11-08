using Microsoft.AspNetCore.Mvc;
namespace Tienda.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestoController : ControllerBase
{
    private readonly PresupuestoRepository presupuestoRepository;

    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository(@"Data Source=db/Tienda.db;Cache=Shared");
    }
    [HttpPost("Crear")]
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
    [HttpGet("ObtenerTodos")]
    public ActionResult GetAll()
    {
        return Ok(presupuestoRepository.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(presupuestoRepository.GetById(id));
    }
    [HttpPost("AgregarProducto")]
    public ActionResult AddProduct(int idProducto, int idPresupuesto, int cantidad)
    {
        if (!presupuestoRepository.AddProduct(idProducto, idPresupuesto, cantidad))
        {
            return NotFound("Producto o presupuesto no encontrado.");
        }
        return Ok();
    }
    [HttpDelete]
    public ActionResult Delete(int idPresupuesto)
    {
        presupuestoRepository.Delete(idPresupuesto);
        return Ok();
    }
}
