using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    internal class ServiceResults<T>
    {
        public Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
        public bool HasErrors { get { return Errors.Count > 0; } }

        public T Result { get; set; }

        public ServiceResults()
        {
            Errors = new Dictionary<string, string>();
        }
    }
}
