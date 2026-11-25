namespace Gestionale.Api.Entities;

public class Categoria
{
    public int IdCategoria { get; set; }// NomeClasse + Id -> chiave primaria, generata da Postgres
    public string NomeCategoria { get; set; } = string.Empty;// NOT NULL
    public List<Articolo> ArticoliCategoria { get; set; } = []; // una categoria ha molti articoli
}