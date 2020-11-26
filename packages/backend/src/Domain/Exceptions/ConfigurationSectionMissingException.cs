using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Domain.Exceptions
{
    public class ConfigurationSectionMissingException: Exception
    {
        public ConfigurationSectionMissingException(string sectionName): base($"ConfigurationSection '{sectionName}' is missing")
        {}
    }
}
