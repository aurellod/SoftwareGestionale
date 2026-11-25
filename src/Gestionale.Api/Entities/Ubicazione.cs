namespace Gestionale.Api.Entities;
public class Ubicazione
{
    public int IdUbicazione {get; set;}
    public string NomeUbicazione { get; set; } = string.Empty; //es. furgone1, magazzino, Rifter, ecc.
    public TipoUbicazione TipoUbicazione { get; set; } = TipoUbicazione.Furgone;
    public string? Targa { get; set; }
    public bool Attivo { get; set; } = true;
    public List<Giacenza> GiacenzeUbicazione { get; set; } = [];//cosa c'è sul furgone
}