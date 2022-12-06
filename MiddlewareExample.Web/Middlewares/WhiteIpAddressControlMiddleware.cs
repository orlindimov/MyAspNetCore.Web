using System.Net;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        //RequestDelegate ile middleware olarak kullanabiliyoruz

        private const string WhiteIpAdress = "::1";


        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //IPV4 => 127.0.0.1 => localhost
            //IPV6 => ::1 => localhost

            var reqIpAddress = context.Connection.RemoteIpAddress;

            bool AnyWhiteIpAdress=IPAddress.Parse(WhiteIpAdress).Equals(reqIpAddress);

            if (AnyWhiteIpAdress==true) 
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode=HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("forbidden");

            }

        }
    }
}
