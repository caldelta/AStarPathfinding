using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Requests.Web.Enums
{
    public enum StatusCode
    {
        OK = 2000,
        TOKEN_INVALID = 4000,
        TOKEN_EXPIRED = 4001,
    }
}