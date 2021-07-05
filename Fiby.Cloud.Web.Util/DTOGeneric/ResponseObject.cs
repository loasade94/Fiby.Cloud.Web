using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Cross.Util.DTOGeneric
{
    public class ResponseObject<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public ErrorDetails Details { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
