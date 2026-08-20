using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Communication
{
    [Serializable]
    public class Response
    {
        public bool IsSuccessful { get; set; } = true
        public string Message{ get; set; }
        public object Result { get; set; }
    }
}
