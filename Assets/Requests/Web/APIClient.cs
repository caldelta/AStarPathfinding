using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Requests.Web
{
    public class APIClient
    {
        private static readonly IDictionary<string, string> _defaultRequestHeaders = new Dictionary<string, string>();

        private static APIClient _instance;

        private APIClient() { }

        public static APIClient Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = new APIClient();
                return _instance;
            }
        }


        private const string kAccessToken = "accesstoken";
        private const string kUserName = "username";

        public async UniTask<string> GetJson(string path)
        {
            var headers = new Dictionary<string, string>(_defaultRequestHeaders)
            {
                {"Content-Type", "application/json"},
                //{kAccessToken, EnvironmentManager.Instance.AccessToken},
            };
            try
            {              
                var result = await APIRequest.GetText(headers, path);
                APICheckStatus.Check(result);
                return result;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.LogError(ex.Message);
#endif
                //var page = PageManager.Instance.GetPageByName(Page.Login);
                //_ = PageManager.Instance.Display(page);
                return await new UniTask<string>();                
            }
        }

        public async UniTask<Texture2D> GetTexture2D(string path, string accessToken)
        {
            var headers = new Dictionary<string, string>(_defaultRequestHeaders)
            {
            };
            try
            {
                return await APIRequest.GetTexture2D(headers, path);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.LogError(ex.Message);
#endif
                //var page = PageManager.Instance.GetPageByName(Page.Login);
                //_ = PageManager.Instance.Display(page);
                return await new UniTask<Texture2D>();
            }
        }

        public async UniTask<string> Post(string path, string accessToken, byte[] payload)
        {
            var headers = new Dictionary<string, string>(_defaultRequestHeaders)
            {
                {"Content-Type", "application/json"},
                //{kAccessToken, EnvironmentManager.Instance.AccessToken},
            };
            try
            {
                return await APIRequest.Post(headers, path, payload);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.LogError(ex.Message);
#endif
                //var page = PageManager.Instance.GetPageByName(Page.Login);
                //_ = PageManager.Instance.Display(page);
            }
            return await new UniTask<string>();
        }

        public static void SetAccessTokenToHeader(string accessToken)
        {
            RemoveAccessTokenToHeader();

            _defaultRequestHeaders.Add(kAccessToken, accessToken);
        }

        public static void RemoveAccessTokenToHeader()
        {
            if (_defaultRequestHeaders.ContainsKey(kAccessToken))
            {
                _defaultRequestHeaders.Remove(kAccessToken);
            }
        }
        public bool IsOnline()
        {
            return Application.internetReachability == NetworkReachability.NotReachable;
        }
    }
}