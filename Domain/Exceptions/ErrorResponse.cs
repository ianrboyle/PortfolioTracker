

using System.Net;

namespace Domain.Exceptions
{
  public class ErrorResponse
  {
    public string Error { get; set; }
    public string Details { get; set; }
    public int StatusCode { get; set; }
  }

}