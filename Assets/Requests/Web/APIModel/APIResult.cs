using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Requests.Web.APIModel
{
    [System.Serializable]
    public class APIResult<T> : APIStatus
    {
        public T data;
    }
}