using Tracker.Entities.Enums;
using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Entities;

class Topico
{
    public string Id { get; private set; }
    public string Nome { get; set; }
    public StatusTopico Status { get; private set; }
    private List<RegistroEstudo> _registros;

    public Topico(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentNullException("O nome não pode ser vazio.", nameof(nome));
        Nome = nome;
        Id = Guid.NewGuid().ToString();
        Status = StatusTopico.NaoIniciado;
        _registros = new List<RegistroEstudo>();
    }

    public IReadOnlyList<RegistroEstudo> ObterRegistros()
    {
        return _registros;
    }

    public void AlterarStatus(StatusTopico novoStatus)
    {
        if (Status == StatusTopico.Concluido)
            throw new InvalidOperationException("Não é possível alterar o status de um tópico já concluído.");
        Status = novoStatus;
    }

    public void AdicionarRegistro(RegistroEstudo registro)
    {
        if (Status == StatusTopico.NaoIniciado)
            throw new InvalidOperationException("Não é possível adicionar um registro a um tópico não iniciado.");
        _registros.Add(registro);
        if (Status == StatusTopico.NaoIniciado)
            Status = StatusTopico.Iniciado;
    }
}