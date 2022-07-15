using System;
using commercetools.Base.Client;
using commercetools.Base.Client.Domain;
using commercetools.Base.Client.Tokens;

namespace commercetools.sample_anonymtoken_auth.Controllers
{
    public class AnonymToken : ITokenProvider
    {
        public AnonymToken(string token)
        {
            this.token = token;
        }

        private readonly string token;

        public Token Token
        {
            get
            {
                return new Token()
                {
                    AccessToken = token
                };

            }
        }

        public TokenFlow TokenFlow
        {
            get
            {
                return TokenFlow.AnonymousSession;
            }
        }

        public IClientConfiguration ClientConfiguration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}
