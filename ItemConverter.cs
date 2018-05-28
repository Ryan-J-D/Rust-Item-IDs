using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using System.Linq;
using Newtonsoft.Json;
using Rust;

namespace Oxide.Plugins
{
    [Info("ItemConverter", "Ryan", "1.0.0")]

    public class ItemConverter : RustPlugin
    {
        public class DataModel
        {
            public Dictionary<int, string> IdToName = new Dictionary<int, string>();
            public Dictionary<string, int> NameToId = new Dictionary<string, int>();
        }

        private void OnServerInitialized()
        {
            var data = new DataModel();

            foreach (var item in ItemManager.itemList)
            {
                data.IdToName.Add(item.itemid, item.shortname);
                data.NameToId.Add(item.shortname, item.itemid);
            }

            Interface.Oxide.DataFileSystem.WriteObject($"ItemIds_v{Protocol.network}", data);
            Puts($"Done");
        }
    }
}