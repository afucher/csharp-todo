/*
 *  Código retirado do artigo: http://high5devs.com/2019/01/testes-de-unidade-para-httpclient-no-net-com-o-flurl/
 */

using Flurl.Http;
using Flurl.Http.Content;
using Flurl.Http.Testing;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
 
namespace ToDoUnitTest.Helpers
{
    public class FakeHttpClientMessageHandler : FakeHttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var flurlRequest = new FlurlRequest(request.RequestUri.ToString());
            var stringContent = (request.Content as StringContent);
 
            if (stringContent != null)
                request.Content = new CapturedStringContent(await stringContent.ReadAsStringAsync(), GetEncodingFromCharSet(stringContent.Headers?.ContentType?.CharSet), stringContent.Headers?.ContentType?.MediaType);
 
            if (request?.Properties != null)
                request.Properties["FlurlHttpCall"] = new HttpCall()
                {
                    FlurlRequest = flurlRequest,
                    Request = request
                };
 
            return await base.SendAsync(request, cancellationToken);
        }
 
        private Encoding GetEncodingFromCharSet(string charset)
        {
            try
            {
                return HasQuote(charset)
                    ? Encoding.GetEncoding(charset.Substring(1, charset.Length - 2))
                    : Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
 
        private bool HasQuote(string text)
            => text.Length > 2 && text[0] == '\"' && text[text.Length - 1] == '\"';
    }
}