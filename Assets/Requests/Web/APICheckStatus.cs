using Requests.Web.APIModel;
using Requests.Web.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Requests.Web
{
    public class APICheckStatus
    {
        public static bool Check(string apiResult)
        {
            var apiStatus = JsonUtility.FromJson<APIStatus>(apiResult);

            return DoCheck(apiStatus);
        }

        public static bool Check(APIStatus apiStatus)
        {
            return DoCheck(apiStatus);
        }

        private static bool DoCheck(APIStatus apiStatus)
        {
            if (apiStatus == null)
                return false;
            var statusCode = StatusCodeToEnum(apiStatus.statusCode);
            switch (statusCode)
            {
                case StatusCode.OK:
                    return true;
                case StatusCode.TOKEN_INVALID:
                case StatusCode.TOKEN_EXPIRED:
                    throw new OperationCanceledException(apiStatus.message);
                default:
                    throw new OperationCanceledException(apiStatus.message);
            }
        }

        public static StatusCode StatusCodeToEnum(int statusCode) => statusCode switch
        {
            2000 => StatusCode.OK,
            4000 => StatusCode.TOKEN_INVALID,
            4001 => StatusCode.TOKEN_EXPIRED,
            _ => throw new System.NotImplementedException()
        };
    }
}