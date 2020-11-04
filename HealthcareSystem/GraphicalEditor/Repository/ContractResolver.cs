using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Repository
{
    public class ContractResolver : DefaultContractResolver
    {
        private String _propertyForElimination = "FocusVisualStyle";

        public ContractResolver(){}

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            properties =
                properties.Where(p => !p.PropertyName.Equals(_propertyForElimination)).ToList();

            return properties;
        }
    }
}
