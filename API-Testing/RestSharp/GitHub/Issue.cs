using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit_API_tests
{
    public class Issue
    {
        public long id { get; set; }
        public long number { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string name { get; set; }
    }
}
