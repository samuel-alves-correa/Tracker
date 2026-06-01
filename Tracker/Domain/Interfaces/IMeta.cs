using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Interfaces;

public interface IMeta
{
    int Id { get; set; }
    DateTime Inicio { get; set; }
    DateTime Fim { get; set; }

    bool EstaAtiva(DateTime agora);
    bool FoiCumprida(IEnumerable<RegistroEstudo> registros);
}