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
}