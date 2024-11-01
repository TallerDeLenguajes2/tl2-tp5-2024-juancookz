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
        Producto producto = new Producto(descripcion, precio);
        productoRepository.Create(producto);
        return Ok();
    }
    [HttpGet]
    public ActionResult ListarProductos()
    {
        List<Producto> productos = productoRepository.GetAll();
        return Ok(productos);
    }
    [HttpPut("{id}")]
    public ActionResult EditarProducto(int id, string nuevaDescripcion)
    {
        Producto producto = productoRepository.Get(id);
        if (producto == null) { return BadRequest("No se encontro el producto"); }
        producto.Descripcion = nuevaDescripcion;
        productoRepository.Modify(producto);
        return Ok();
    }
}
