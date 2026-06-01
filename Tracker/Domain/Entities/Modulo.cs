using Tracker.Domain.Exceptions;
using Tracker.Entities.Enums;

namespace Tracker.Domain.Entities;

class Modulo
{
    public string Nome { get; set; }
    public string Id { get; private set; }
    private List<Topico> _topicos;

    public Modulo(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentNullException(nameof(nome));
        Nome = nome.Trim();
        Id = Guid.NewGuid().ToString();
        _topicos = new List<Topico>();
    }

    public IReadOnlyList<Topico> ObterTopicos()
    {
        return _topicos;
    }

    public void AdicionarTopico(Topico topico)
    {
        _topicos.Add(topico);
        if (_topicos.Any(t => string.Equals(t.Nome, topico.Nome, StringComparison.OrdinalIgnoreCase)))
        {
            throw new TopicoDuplicadoException(topico.Nome);
        }
    }

    public double CalcularProgresso()
    {
        int concluidos = _topicos.Where(t => t.Status == StatusTopico.Concluido).Count();
        if (concluidos == 0 || _topicos.Count == 0)
            return 0;
        
        return (concluidos / (double)_topicos.Count)  * 100;
    }
}