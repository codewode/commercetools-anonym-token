using System.Net.Http;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Base.Client.Tokens;
using commercetools.Base.Serialization;
using commercetools.Sdk.Api.Extensions;
using commercetools.Sdk.Api.Models.Carts;
using Microsoft.AspNetCore.Mvc;

namespace commercetools.sample_anonymtoken_auth.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
       
        protected readonly IClientConfiguration clientConfiguration;
 
        private readonly IHttpClientFactory httpClientFactory;

        private readonly ISerializerService serializerService;

        public CartController(IClientConfiguration clientConfiguration, IHttpClientFactory httpClientFactory, ISerializerService serializerService) {
            this.clientConfiguration = clientConfiguration;
            this.httpClientFactory = httpClientFactory;
            this.serializerService = serializerService;
        }

        [HttpGet("getCart/{token}", Name = "Get Cart")]
        public async Task<ICart> GetCart([FromRoute]string token)
        {
            return await this.GetClientByToken(token).WithApi(clientConfiguration.ProjectKey).Me().ActiveCart().Get().ExecuteAsync();
        }


        [HttpGet("token", Name = "Create Token")]
        public string Token()
        {
            return  CreateToken();
        }



        private IClient GetClientByToken(string token)
        {
            return
                 ClientFactory.Create(
                "MeClient",
                clientConfiguration,
                httpClientFactory,
                serializerService,
               new AnonymToken(token));
            ;
        }


        private string CreateToken()
        {
            var anonymTokenProvider = TokenProviderFactory
               .CreateAnonymousSessionTokenProvider(clientConfiguration,
               httpClientFactory, new InMemoryAnonymousCredentialsStoreManager(null));

            return  anonymTokenProvider.Token.AccessToken;
        }

    }
}
