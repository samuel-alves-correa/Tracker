using System;
using System.Linq;
using System.Collections.Generic;
using Tracker.Domain.ValueObjects;

namespace Tracker.Domain.Entities
{
    public class RelatorioDeEvolucao
    {
        public static int CalcularStreak(IEnumerable<RegistroEstudo> registros)
        {
            if (registros == null) return 0;

            var datasEstudadas = registros
                .Where(r => r != null)
                .Select(r => r.Data.Date)
                .Select(d => DateTime.SpecifyKind(d, DateTimeKind.Unspecified))
                .ToHashSet();

            if (datasEstudadas.Count == 0) return 0;

            var hoje = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified);
            var ontem = hoje.AddDays(-1);

            if (!datasEstudadas.Contains(hoje) && !datasEstudadas.Contains(ontem))
                return 0;

            var diaAnalise = datasEstudadas.Contains(hoje) ? hoje : ontem;
            int totalStreak = 0;

            while (datasEstudadas.Contains(diaAnalise))
            {
                totalStreak++;
                diaAnalise = diaAnalise.AddDays(-1);
            }

            return totalStreak;
        }

        public static TimeSpan TotalHorasEstudadas(IEnumerable<RegistroEstudo> registros)
        {
        if (registros == null) return TimeSpan.Zero;
        long totalTicks = registros.Where(r => r != null).Sum(r => r.Duracao.Ticks);
        return TimeSpan.FromTicks(totalTicks);
        }

        public static IDictionary<string, TimeSpan> HorasPorModulo(IEnumerable<Modulo> modulos)
        {
        var resultado = new Dictionary<string, TimeSpan>(StringComparer.OrdinalIgnoreCase);
        if (modulos == null) return resultado;

        foreach (var modulo in modulos)
        {
            if (modulo == null) continue;
            var nome = (modulo.Nome ?? string.Empty).Trim();
            var topicos = modulo.ObterTopicos() ?? Enumerable.Empty<Topico>();
            var totalTicks = topicos
                .Where(t => t != null)
                .SelectMany(t => t.ObterRegistros() ?? Enumerable.Empty<RegistroEstudo>())
                .Where(r => r != null)
                .Sum(r => r.Duracao.Ticks);
            var duracao = TimeSpan.FromTicks(totalTicks);

            if (resultado.TryGetValue(nome, out var existente))
                resultado[nome] = existente.Add(duracao);
            else
                resultado[nome] = duracao;
        }

        return resultado;
        }

        public static IEnumerable<Topico> TopicosEmRisco(IEnumerable<Topico> topicos, int diasLimite)
        {
            if (topicos == null) yield break;
            if (diasLimite < 0) diasLimite = 0;

            var hoje = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Unspecified);

            foreach (var topico in topicos)
            {
                if (topico == null) continue;
                if (topico.Status == Tracker.Entities.Enums.StatusTopico.Concluido) continue;

                var registros = topico.ObterRegistros();
                if (registros == null || registros.Count == 0)
                {
                    yield return topico;
                    continue;
                }

                var ultimaData = registros
                    .Where(r => r != null)
                    .Select(r => DateTime.SpecifyKind(r.Data.Date, DateTimeKind.Unspecified))
                    .Max();

                var diasDesdeUltimo = (hoje - ultimaData).TotalDays;
                if (diasDesdeUltimo > diasLimite)
                    yield return topico;
            }
        }
    }
}