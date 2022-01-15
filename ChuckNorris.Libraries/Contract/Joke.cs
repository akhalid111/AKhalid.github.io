using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Libraries.Contract
{
    public class Joke
    {
        public string Icon_url { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }
}
