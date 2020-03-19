using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class PropertyMappingValue
    {
        // FirstName 和 LastName
        public IEnumerable<string> DestinationProperties { get; set; }

        // 按年龄和按出生日期排序，结果正好相反，所以需要revert，为true就反转
        public bool Revert { get; set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }
    }
}
