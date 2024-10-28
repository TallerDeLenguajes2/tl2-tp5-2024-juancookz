using Microsoft.AspNetCore.Mvc;
namespace Tienda.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoRepository productoRepository;

    public ProductoController()
    {
        productoRepository = new ProductoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
    }

    [HttpPost]
    public ActionResult CrearProducto([FromForm] string descripcion, int precio)
    {
        if (string.IsNullOrEmpty(descripcion) || precio <= 0)
        {
            return BadRequest("Producto inválido. Verifica que la descripción no esté vacía y el precio sea mayor a 0.");
        }
        ProductoRepository productoRepository = new ProductoRepository(@"Data Source=db\Tienda.db;Cache=Shared");
        Productos producto = new Productos(descripcion, precio);
        productoRepository.CrearProducto(producto);
        return Ok();
    }
    [HttpGet]
    public ActionResult ListarProductos()
    {
        List<Productos> productos= productoRepository.ListarProductos();
        return Ok(productos);
    }
}
