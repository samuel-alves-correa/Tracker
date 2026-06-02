using Tracker.Domain.Interfaces;
using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Entities;

class MetaMensal : IMeta
{
    public int Id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public TimeSpan MetaHorasMes { get; set; }
    public int TopicosAlvo { get; set; }

    public MetaMensal(int id, DateTime inicio, DateTime fim, TimeSpan metaHorasMes, int topicosAlvo)
    {
        Id = id;
        Inicio = inicio;
        Fim = fim;
        MetaHorasMes = metaHorasMes;
        TopicosAlvo = topicosAlvo;
    }

    public bool EstaAtiva(DateTime agora)
    {
        return agora >= Inicio && agora <= Fim;
    }

    public bool FoiCumprida(IEnumerable<RegistroEstudo> registros)
    {
        var registrosDoMes = registros.Where(r => r.Data.Month == Inicio.Month
                                                  && r.Data.Year == Inicio.Year);
        double totalMinutosEstudados = registrosDoMes.Sum(r => r.Duracao.TotalMinutes);
        bool bateuMetaHoras = totalMinutosEstudados >= MetaHorasMes.TotalMinutes;
        int quantidadeTopicosDistintos = registrosDoMes
            .Select(r => r.TopicoId)
            .Distinct()
            .Count();
        bool bateuMetaTopicos = quantidadeTopicosDistintos >= TopicosAlvo;

        return bateuMetaHoras && bateuMetaTopicos;
    }
}