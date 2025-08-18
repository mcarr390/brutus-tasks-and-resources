using System.Collections.Generic;
using UnityEditor;

namespace tasks_and_resources.Core
{
    public static class TaskRegistry
    {
        public static IReadOnlyDictionary<string, Task> Registry;
    
        public static void Init(IEnumerable<Resource> resources)
        {
            Registry = TaskGenerator.GenerateTasks(resources);
        }
    }
}