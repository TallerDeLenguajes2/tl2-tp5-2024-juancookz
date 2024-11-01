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
        
    }
}