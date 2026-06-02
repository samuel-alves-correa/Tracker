using Tracker.Domain.Interfaces;
using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Entities;

class MetaMensal : IMeta
{
    public int Id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public TimeSpan MetaHorasMes { get; set; }
    public bool EstaAtiva(DateTime agora)
    {
        throw new NotImplementedException();
    }

    public bool FoiCumprida(IEnumerable<RegistroEstudo> registros)
    {
        throw new NotImplementedException();
    }
}