using System.Collections.Generic;
using System.Linq;

namespace ASample.Npoi.Config
{
    public class ExcelTableMap
    {
        private static Dictionary<string, TableConfig> _configs
            = new Dictionary<string, TableConfig>();

        public static TableConfig<T> Config<T>(int startRowIndex, int startCellIndex,
            int endCellIndex, bool firstRowAsHeader = true)
        {
            TableConfig<T> config;
            var configKey = typeof(T).FullName;
            if (_configs.Keys.Contains(configKey))
                config = (TableConfig<T>)_configs[configKey];
            else
            {
                config = new TableConfig<T>
                {
                    FirstRowAsHeader = firstRowAsHeader,
                    EndCellIndex = endCellIndex,
                    StartRowIndex = startCellIndex,
                    StartCellIndex = startRowIndex
                };
                _configs.Add(configKey, config);
            }
            return config;
        }

        public static TableConfig<T> Config<T>(string subKey, int startRowIndex, int startCellIndex,
            int endCellIndex, bool firstRowAsHeader = true)
        {
            TableConfig<T> config;
            if (string.IsNullOrWhiteSpace(subKey))
                return Config<T>(startRowIndex, startCellIndex, endCellIndex, firstRowAsHeader);
            var configKey = typeof(T).FullName + "_" + subKey;
            if (_configs.Keys.Contains(configKey))
                config = (TableConfig<T>)_configs[configKey];
            else
            {
                config = new TableConfig<T>
                {
                    FirstRowAsHeader = firstRowAsHeader,
                    EndCellIndex = endCellIndex,
                    StartRowIndex = startCellIndex,
                    StartCellIndex = startRowIndex
                };
                _configs.Add(configKey, config);
            }
            return config;
        }
    }
}
