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
        throw new NotImplementedException();
    }

    public bool FoiCumprida(IEnumerable<RegistroEstudo> registros)
    {
        throw new NotImplementedException();
    }
}