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
    public void ModificarProducto(Productos producto)
    {
        string query = @"UPDATE Productos SET Descripcion = @descripcion WHERE idProducto = @id;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@id", producto.Idproducto));
            command.ExecuteNonQuery();
            connection.Close ();
        }
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
    public Productos ObtenerProducto(int idproducto)
    {
        Productos producto = new Productos();
        string query = @"SELECT P.idProducto,P.Descripcion, P.Precio FROM Productos P WHERE P.idProducto = @id;";

        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", idproducto));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    producto.Idproducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                }
            }
            connection.Close();
        }

        return producto;
    }
    void EliminarProducto(int idproducto)
    {
    }
}