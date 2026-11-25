namespace Gestionale.Api.Entities;
public class Giacenza
{
    //chiave composta della tabella Giacenza: IdArticolo + IdUbicazione
    public int IdArticolo { get; set; }
    public Articolo Articolo { get; set; } = null!;
    public int IdUbicazione { get; set; }
    public Ubicazione Ubicazione { get; set; } = null!;
    public decimal Quantita { get; set; } = 0;//mai negativa, può essere zero se l'articolo è esaurito
    public decimal SogliaMinima { get; set; }
    public StatoRiordino  StatoRiordino { get; set; } = StatoRiordino.Ok;
    public DateTime DataAggiornamento { get; set; } = DateTime.UtcNow; // sempre UTC
}