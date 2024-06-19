namespace Corona.Persistance.Exceptions;

public interface IBaseException
{
    int StatusCode { get; } 
    string CustomMessage { get; }
}
