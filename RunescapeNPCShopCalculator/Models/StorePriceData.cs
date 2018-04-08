using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunescapeNPCShopCalculator.StoreData
{
    public partial class StorePriceData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("store")]
        public long Store { get; set; }
    }

    public partial class StorePriceData
    {
        public static Dictionary<string, StorePriceData> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, StorePriceData>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, StorePriceData> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
