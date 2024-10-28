public class Productos
{
    private int idproducto;
    private string descripcion;
    private int precio;
    public int Idproducto { get => idproducto; set => idproducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }
    public Productos(string descripcion, int precio)
    {
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public Productos()
    {
    }
}