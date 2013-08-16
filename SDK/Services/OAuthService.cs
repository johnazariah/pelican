﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using MYOB.AccountRight.SDK.Communication;
using MYOB.AccountRight.SDK.Contracts;
using MYOB.AccountRight.SDK.Extensions;

namespace MYOB.AccountRight.SDK.Services
{
    public class OAuthService
    {
        private readonly IApiConfiguration _configuration;
        private readonly IWebRequestFactory _factory;

        public OAuthService(IApiConfiguration configuration, IWebRequestFactory factory = null)
        {
            _configuration = configuration;
            _factory = factory ?? new WebRequestFactory();
        }

        public void GetTokens(string code, Action<HttpStatusCode, OAuthTokens> onComplete, Action<Uri, Exception> onError)
        {
            var handler = new OAuthRequestHandler(_configuration);
            var request = _factory.Create(OAuthRequestHandler.OAuthRequestUri);
            handler.GetOAuthTokens(request, code, onComplete, onError);
        }

        public OAuthTokens GetTokens(string code)
        {
            var wait = new ManualResetEvent(false);
            OAuthTokens oauthTokens = null;
            Exception ex = null;
            var requestUri = default(Uri);

            GetTokens(code,
                (statusCode, tokens) =>
                    {
                        oauthTokens = tokens;
                        wait.Set();
                    },
                (uri, exception) =>
                    {
                        requestUri = uri;
                        ex = exception;
                        wait.Set();
                    });

            if (wait.WaitOne(new TimeSpan(0, 0, 0, 60)))
            {
                ex.ProcessException(requestUri);
            }

            return oauthTokens;
        }

        public void RenewTokens(OAuthTokens oauthTokens, Action<HttpStatusCode, OAuthTokens> onComplete, Action<Uri, Exception> onError)
        {
            var handler = new OAuthRequestHandler(_configuration);
            var request = _factory.Create(OAuthRequestHandler.OAuthRequestUri);
            handler.RenewOAuthTokens(request, oauthTokens, onComplete, onError);
        }

        public OAuthTokens RenewTokens(OAuthTokens oauthTokens)
        {
            var wait = new ManualResetEvent(false);
            OAuthTokens newTokens = null;
            Exception ex = null;
            var requestUri = default(Uri);

            RenewTokens(
                oauthTokens,
                (statusCode, tokens) =>
                    {
                        newTokens = tokens;
                        wait.Set();
                    },
                (uri, exception) =>
                    {
                        requestUri = uri;
                        ex = exception;
                        wait.Set();
                    });

            if (wait.WaitOne(new TimeSpan(0, 0, 0, 60)))
            {
                ex.ProcessException(requestUri);
            }

            return newTokens;
        }
        
    }
}
