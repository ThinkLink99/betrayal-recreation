using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_shared
{
    public abstract class Writable<T>
    {
        /// <summary>
        /// Exports a JSON formatted string of the object.
        /// </summary>
        /// <returns></returns>
        public string ExportJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// Creates a new object from the given JSON formatted string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ImportJSONAsObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static List<T> ImportJSONAsList(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
