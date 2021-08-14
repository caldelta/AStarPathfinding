using Cysharp.Threading.Tasks;
using Requests.Web.APIModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using Utility;

namespace Requests.Web
{
    public static class APIRequest
    {
        static TimeoutController m_timeoutController = new TimeoutController(); // setup to field for reuse.
        private const int kTimeOut = 15000;

        public static async UniTask<string> GetText(Dictionary<string, string> headers, string url)
        {
            var requestParam = RequestParameter.Get(url);
            var request = requestParam.CreateUnityWebRequest();

            using (request)
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.SetRequestHeader(header.Key, header.Value);
                    }
                }

                try
                {
#if DEBUG
                    Debug.Log($"GET: {url}");
#endif
                    var req = await request.SendWebRequest().WithCancellation(m_timeoutController.Timeout(kTimeOut));

                    if (IsError(req))
                    {
#if DEBUG
                        var text = $"API call error" + Environment.NewLine
                            + $"{url}"
                            + req.responseCode + Environment.NewLine
                            + req.error;
                        Debug.Log(text);
                        //await PopupManager.Instance.Close();
                        throw new UnityWebRequestException(req);
#endif
                    }
                    m_timeoutController.Reset();
#if DEBUG
                    DebugLog.Yellow($"Result: {req.downloadHandler.text}");
#endif

                    return req.downloadHandler.text;
                }
                catch (OperationCanceledException ex)
                {
                    if (m_timeoutController.IsTimeout())
                    {
#if DEBUG
                        Debug.LogError($"API call time out" + Environment.NewLine
                            + $"{url}" + Environment.NewLine
                            + ex.Message);
#endif
                        //await PopupManager.Instance.Close();
                        throw new TimeoutException(ex.Message);
                    }
                    throw new UnityWebRequestException(request);
                }
            }
        }

        public static async UniTask<Texture2D> GetTexture2D(Dictionary<string, string> headers, string url)
        {
            var requestParam = RequestParameter.GetTexture2D(url);
            var request = requestParam.CreateUnityWebRequestTexture();
            using (request)
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.SetRequestHeader(header.Key, header.Value);
                    }
                }
                try
                {
#if DEBUG
                    Debug.Log($"GETTEXTURE2D: {url}");
#endif
                    var req = await request.SendWebRequest().WithCancellation(m_timeoutController.Timeout(kTimeOut));
                    if (IsError(req))
                    {
#if DEBUG
                        var text = $"API call error" + Environment.NewLine
                            + $"{url}"
                            + req.responseCode + Environment.NewLine
                            + req.error;
                        Debug.LogError(text);
                        //await PopupManager.Instance.Close();
                        throw new UnityWebRequestException(req);
#endif
                    }
                    m_timeoutController.Reset();
                    var texture = (req.downloadHandler as DownloadHandlerTexture)?.texture;
#if DEBUG
                    DebugLog.Yellow($"Loaded texture2d {texture.width}x{texture.height} pixels");
#endif
                    return texture;

                }
                catch (OperationCanceledException ex)
                {
                    if (m_timeoutController.IsTimeout())
                    {
#if DEBUG
                        Debug.LogError($"API call time out" + Environment.NewLine
                            + $"{url}" + Environment.NewLine
                            + ex.Message);
#endif
                        //await PopupManager.Instance.Close();
                        throw new TimeoutException(ex.Message);
                    }
                    throw new UnityWebRequestException(request);
                }
            }
        }

        public static async UniTask<string> Post(Dictionary<string, string> headers, string url, byte[] payload)
        {
            var requestParam = RequestParameter.Post(url, payload);
            var request = requestParam.CreateUnityWebRequest();
            using (request)
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.SetRequestHeader(header.Key, header.Value);
                    }
                }
                try
                {
#if DEBUG
                    Debug.Log($"POST: {url}");
#endif
                    var req = await request.SendWebRequest().WithCancellation(m_timeoutController.Timeout(kTimeOut));
                    if (IsError(req))
                    {
#if DEBUG
                        var text = $"API call error" + Environment.NewLine
                            + $"{url}"
                            + req.responseCode + Environment.NewLine
                            + req.error;
                        Debug.LogError(text);
                        //await PopupManager.Instance.Close();
                        throw new UnityWebRequestException(req);
#endif
                    }
                    m_timeoutController.Reset();
#if DEBUG
                    Debug.Log($"Result: {req.downloadHandler.text}");
#endif
                    return req.downloadHandler.text;

                }
                catch (OperationCanceledException ex)
                {
                    if (m_timeoutController.IsTimeout())
                    {
#if DEBUG
                        Debug.LogError($"API call time out" + Environment.NewLine
                            + $"{url}" + Environment.NewLine
                            + ex.Message);
#endif
                        //await PopupManager.Instance.Close();
                        throw new TimeoutException(ex.Message);
                    }
                    throw new UnityWebRequestException(request);
                }
            }
        }

        private static bool IsError(UnityWebRequest req)
        {
            return !string.IsNullOrEmpty(req.error) || req.responseCode.ToString().StartsWith("4") || req.responseCode.ToString().StartsWith("5");
        }
    }
}