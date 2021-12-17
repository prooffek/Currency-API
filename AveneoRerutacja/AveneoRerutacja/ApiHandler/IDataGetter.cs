using System;
using System.Collections.Generic;

namespace AveneoRerutacja.ApiHandler
{
    public interface IDataGetter
    {
        public IList<string> GetData(ResponseKeys key);
        
        public Dictionary<ResponseKeys, Delegate> ResponseData { get; }
    }
}