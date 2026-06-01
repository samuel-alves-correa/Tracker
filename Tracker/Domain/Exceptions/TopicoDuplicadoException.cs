namespace Tracker.Domain.Exceptions;

class TopicoDuplicadoException : Exception
{
    public TopicoDuplicadoException(string nome) :
        base($"O topico '{nome}' já existe neste modulo.") {}
}