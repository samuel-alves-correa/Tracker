using Tracker.Domain.Interfaces;
using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Entities;

class MetaDiaria : IMeta
{
    public int Id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public TimeSpan MetaHorasDia { get; set; }

    public MetaDiaria(int id, DateTime inicio, DateTime fim, TimeSpan metaHorasDia)
    {
        Id = id;
        Inicio = inicio;
        Fim = fim;
        MetaHorasDia = metaHorasDia;
    }

    public bool EstaAtiva(DateTime agora)
    {
        return agora >= Inicio && agora <= Fim;
    }

    public bool FoiCumprida(IEnumerable<RegistroEstudo> registros)
    {
        var registosDoDia = registros.Where(r => r.Data.Date == Inicio.Date);
        var totalMinutosEstudados = registosDoDia.Sum(r => r.Duracao.TotalMinutes);
        return totalMinutosEstudados >= MetaHorasDia.TotalMinutes;
    }
}