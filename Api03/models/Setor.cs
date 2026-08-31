using System.Text.Json.Serialization;

namespace Api03.models;

public class Setor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    [JsonIgnore]
    public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}