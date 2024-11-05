using Microsoft.Data.Sqlite;
public class PresupuestoRepository
{
    private string _stringConnection;
    public PresupuestoRepository(string stringConnection)
    {
        _stringConnection = stringConnection;
    }
    public void Create(Presupuesto presupuesto)
    {
        string query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) 
        VALUES (@NombreDestinatario, @FechaCreacion);";
        using (var connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public List<Presupuesto> GetAll()
    {
        List<Presupuesto> presupuestos = [];

        string query = @"SELECT P.idPresupuesto, P.NombreDestinatario, P.FechaCreacion FROM Presupuestos P;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var presupuesto = new Presupuesto();
                presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                presupuestos.Add(presupuesto);
            }
            connection.Close();
        }
        return presupuestos;
    }
    public Presupuesto GetById(int idPresupuesto)
    {
        Presupuesto presupuesto = new Presupuesto();
        int presupuestoEncontrado = 0;
        string query = @"SELECT 
                            P.idPresupuesto,
                            P.NombreDestinatario,
                            P.FechaCreacion,
                            PR.idProducto,
                            PR.Descripcion AS Producto,
                            PR.Precio,
                            PD.Cantidad,
                            (PR.Precio * PD.Cantidad) AS Subtotal
                        FROM 
                            Presupuestos P
                        JOIN 
                            PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                        JOIN 
                            Productos PR ON PD.idProducto = PR.idProducto
                        WHERE 
                            P.idPresupuesto = @idPresupuesto;";
        using (SqliteConnection connection = new SqliteConnection(_stringConnection))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //Si encuentro un presupuesto
                    if (presupuestoEncontrado == 0)
                    {
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                        presupuesto.Detalle = [];
                        presupuestoEncontrado++;
                    }

                    var producto = new Producto();

                    producto.Idproducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Producto"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    PresupuestoDetalle detalle = new PresupuestoDetalle(producto,Convert.ToInt32(reader["Cantidad"]));
                    presupuesto.Detalle.Add(detalle);
                }
            }
            connection.Close();
        }
        return presupuesto;
    }
}