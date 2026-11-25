namespace Gestionale.Api.Entities;
public class Movimento
{
    public long IdMovimento { get; set; }
    public TipoMovimento TipoMovimento {get; set;}
    public int IdArticolo { get; set; }
    public Articolo Articolo { get; set; } = null!;
    public decimal Quantita { get; set; }
    public int? IdUbicazioneOrigine { get; set; }
    public Ubicazione? UbicazioneOrigine { get; set; }
    public int? IdUbicazioneDestinazione { get; set; }
    public Ubicazione? UbicazioneDestinazione { get; set; }
    public DateTime DataMovimento { get; set; } = DateTime.UtcNow;
    public string? Note { get; set; }
}