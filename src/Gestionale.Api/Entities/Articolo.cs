namespace Gestionale.Api.Entities;

public class Articolo
{
    public int IdArticolo { get; set; }
    public string NomeArticolo { get; set; } = string.Empty;

    // Chiave esterna verso Categoria: la colonna + l'oggetto collegato
    public int IdCategoria { get; set; }
    public Categoria Categoria { get; set; } = null!;// EF la valorizza quando carica i dati

    public string UnitaMisura { get; set; } = "pz";// pz, m, kg, l, conf
    public string? CodiceArticolo { get; set; }// facoltativo
    public bool Attivo { get; set; } = true;
    public DateTime DataCreazione { get; set; } = DateTime.UtcNow; // sempre UTC
    public List<Giacenza> GiacenzeArticolo { get; set; } = [];
    public List<Movimento> MovimentiArticolo { get; set; } = [];
}