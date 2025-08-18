using tasks_and_resources.Data;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace tasks_and_resources.Core
{
    internal static class JsonLoader
    {
        internal static ResourceDto[] GetResourceDtos()
        {
            TextAsset resourceJson = Resources.Load<TextAsset>("resources_2");
            var resourceDtos = JsonConvert.DeserializeObject<ResourceCatalogDto>(resourceJson.text).Resources;
            return resourceDtos;
        }
        
    }
}
