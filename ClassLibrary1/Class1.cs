using FluentAssertions;
using ServiceReference1;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Xunit;

namespace ClassLibrary1;

public class Class1
{
    [Fact]
    public async Task Test()
    {
        var elementCollection = new BindingElementCollection
        {
            new TextMessageEncodingBindingElement(),
            new HttpTransportBindingElement
            {
                AllowCookies = true,
                BypassProxyOnLocal = false,
                KeepAliveEnabled = true,
                ManualAddressing = false,
                MaxBufferPoolSize = 8589934588,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                TransferMode = TransferMode.Buffered,
                UseDefaultWebProxy = true,
                AuthenticationScheme = AuthenticationSchemes.Digest,
            }
        };

        var binding = new CustomBinding(elementCollection);
        var endpoint = new EndpointAddress("http://localhost");
        var keystoreClient = Activator.CreateInstance(typeof(KeystoreClient), binding, endpoint) as KeystoreClient;

        Func<Task> func = async () => await keystoreClient.GetAllCertPathValidationPoliciesAsync();

        await func.Should().NotThrowAsync();
    }
}
