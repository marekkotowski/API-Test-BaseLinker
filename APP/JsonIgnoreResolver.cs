using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft;
using System.Reflection;


namespace API_Test_BaseLinker.APP
{
    /// <summary>
    /// Klasa do wyłączenia wybranych włąściwości z serializacji 
    /// obiekt klasy jest parametrem dla JsonSerializerSettings i tworzy obiekt ContractResolver
    ///  Newtonsoft.Json.JsonConvert.SerializeObject(_order, new JsonSerializerSettings { ContractResolver = new JsonIgnoreResolver( string[]) })
    /// </summary>
    public class JsonIgnoreResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        private HashSet<string> ignoreProps;

        /// <summary>
        /// konstruktor z listą nazwa właściwości do wyłączenia z serializacji
        /// </summary>
        /// <param name="propNamesToIgnore"></param>
        public JsonIgnoreResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }


        /// <summary>
        /// wyłączenie właściwości z serializacji
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            Newtonsoft.Json.Serialization.JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this.ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }
}
