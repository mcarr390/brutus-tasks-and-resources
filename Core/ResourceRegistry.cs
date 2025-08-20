using System.Collections.Generic;
using UnityEditor;

namespace tasks_and_resources.Core
{
    public static class ResourceRegistry
    {
        public static IReadOnlyDictionary<string, Resource> ResourcesRegistry;
        public static IReadOnlyDictionary<string, Task> TaskRegistry;

        public static void Init()
        {
            var resourceDtos = JsonLoader.GetResourceDtos();
        
            ResourcesRegistry = TaskAndResourceFactory.BuildResources(resourceDtos);

            TaskRegistry = TaskGenerator.GenerateTasks(ResourcesRegistry.Values);
        }
    }
}
