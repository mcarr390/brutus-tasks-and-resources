using System.Collections.Generic;
using UnityEditor;

namespace tasks_and_resources.Core
{
    public static class ResourceRegistry
    {
        public static IReadOnlyDictionary<string, Resource> Registry;
    
        [MenuItem("Tools/Generate Resources")]
        public static void Init()
        {
            var resourceDtos = JsonLoader.GetResourceDtos();
        
            Registry = TaskAndResourceFactory.BuildResources(resourceDtos);

        }
    }
}
