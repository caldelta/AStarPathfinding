using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Requests.Web.APIModel
{
    [System.Serializable]
    public class APIStatus
    {
        public int statusCode;
        public string message;
    }
}