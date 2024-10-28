using Microsoft.Data.Sqlite;
public class ProductoRepository
{
    private string _stringConnection;

    public ProductoRepository(string stringConnection)
    {
        _stringConnection = stringConnection;
    }

    public void CrearProducto(Productos producto)
    {
        string query = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    void ModificarProducto(int idproducto, Productos producto)
    {

    }
    public List<Productos> ListarProductos()
    {
        List<Productos> productos = [];

        string query = @"SELECT P.idProducto,P.Descripcion, P.Precio FROM Productos P;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var producto = new Productos();
                producto.Idproducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = reader["Descripcion"].ToString();
                producto.Precio = Convert.ToInt32(reader["Precio"]);
                productos.Add(producto);
            }
            connection.Close();
        }
        return productos;
    }
    Productos ObtenerProducto(int idproducto)
    {
        return null;
    }
    void EliminarProducto(int idproducto)
    {
    }
}