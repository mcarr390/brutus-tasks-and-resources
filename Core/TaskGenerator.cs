using System.Collections.Generic;

namespace tasks_and_resources.Core
{
    public static class TaskGenerator 
    {
        public static IReadOnlyDictionary<string, Task> GenerateTasks(IEnumerable<Resource> resources)
        {
            var taskDictionary = new Dictionary<string, Task>();

            foreach (var resource in resources)
            {
                if (resource.Type == "raw")
                {
                    Task gatherTask = new Task();
                    gatherTask.Name = $"Gather {resource.Name}";
                    gatherTask.Produces.Add(resource, 1);
                    taskDictionary.Add(gatherTask.Name, gatherTask);
                }
                if (resource.Type == "craftable")
                {
                    Task craftTask = new Task();
                    craftTask.Name = $"Craft {resource.Name}";
                    craftTask.Produces.Add(resource, 1);
                    foreach (var consumedRes in resource.Consumes)
                    {
                        craftTask.Consumes.Add(consumedRes.Key, consumedRes.Value);
                    }
                    taskDictionary.Add(craftTask.Name, craftTask);
                }
                Task sellTask = new Task();
                sellTask.Name = $"Sell {resource.Name}";
                taskDictionary.Add(sellTask.Name, sellTask);

            }

            return taskDictionary;
        }
    }
}
