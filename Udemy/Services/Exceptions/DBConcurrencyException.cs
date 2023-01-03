namespace Udemy.Services.Exceptions;

public class DBConcurrencyException : ApplicationException {
    public DBConcurrencyException(string msg) : base(msg) { }

}
