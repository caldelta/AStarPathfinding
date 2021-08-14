using UnityEngine.Networking;

namespace Requests.Web.APIModel
{
    public class RequestParameter
    {
        private readonly string _url;

        private readonly string _method;

        private readonly byte[] _payload;

        private RequestParameter(string url, string method, byte[] payload)
        {
            _url = url;
            _method = method;
            _payload = payload;
        }

        public UnityWebRequest CreateUnityWebRequest()
        {
            var download = new DownloadHandlerBuffer();
            var upload = new UploadHandlerRaw(_payload);
            var request = new UnityWebRequest(_url, _method, download, upload);

            return request;
        }
        public UnityWebRequest CreateUnityWebRequestTexture()
        {
            return UnityWebRequestTexture.GetTexture(_url);
        }
        public static RequestParameter GetTexture2D(string url)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbGET, null);
        }

        public static RequestParameter Get(string url)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbGET, null);
        }

        public static RequestParameter Post(string url, byte[] payload)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbPOST, payload);
        }

        public static RequestParameter Put(string url, byte[] payload)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbPUT, payload);
        }

        public static RequestParameter Delete(string url)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbDELETE, null);
        }

        public static RequestParameter Delete(string url, byte[] payload)
        {
            return new RequestParameter(url, UnityWebRequest.kHttpVerbDELETE, payload);
        }
    }
}