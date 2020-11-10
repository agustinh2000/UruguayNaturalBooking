using System;
using System.Collections.Generic;
using System.Text;

namespace Importation
{
    public class Parameter
    {
        public string Name{ get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public Parameter() { }

        public override bool Equals(object obj)
        {
            return obj is Parameter parameter &&
                   Name == parameter.Name &&
                   Type == parameter.Type;
        }
    }
}
