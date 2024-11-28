


using BoilerPlate.Entity.Results.Abstract;
using BoilerPlate.Entity.Results.ComplexTypes;

namespace BoilerPlate.Entity.Results.Concrete;
public class DataResult<T> : IDataResult<T>
{
    public DataResult(ResultStatus resultStatus, T data)
    {
        ResultStatus = resultStatus;
        Data = data;
    }
    public DataResult(ResultStatus resultStatus,string message, T data)
    {
        ResultStatus = resultStatus;
        Message = message;
        Data = data;
    }
    public DataResult(ResultStatus resultStatus,string message,Exception exception, T data)
    {
        ResultStatus = resultStatus;
        Message = message;
        Exception = exception;
        Data = data;
    }
	public T Data {get;}

	public ResultStatus ResultStatus {get;}

	public string Message {get;}

	public Exception Exception {get;}
}