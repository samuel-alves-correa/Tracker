namespace Tracker.Domain.ValueObjects;

public class RegistroEstudo
{
    public string Id { get; private set; }
    public string TopicoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Duracao { get; private set; }

    public RegistroEstudo(string topicoId, DateTime data, TimeSpan duracao)
    {
        if (string.IsNullOrWhiteSpace(topicoId))
            throw new ArgumentNullException("O tópico não pode estar vazio. ", nameof(topicoId));
        if(duracao.TotalSeconds <= 0)
            throw new ArgumentOutOfRangeException("A duraçao deve ser maior que 0.", nameof(duracao));
        Id = Guid.NewGuid().ToString();
        TopicoId = topicoId;
        Data = data;
        Duracao = duracao;
    }
}