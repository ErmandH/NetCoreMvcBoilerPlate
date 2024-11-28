using BoilerPlate.Entity.Results.ComplexTypes;

namespace BoilerPlate.Entity.Results.Abstract;

public interface IResult
{
    public ResultStatus ResultStatus { get;}  // ResultStatus.Success gibi kullanicaz
    public string Message { get;}
    public Exception Exception { get;}
}
