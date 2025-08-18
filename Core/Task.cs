using System.Collections.Generic;

namespace tasks_and_resources.Core
{
    public class Task 
    {
        public string Name { get; set; }
        
        public string Category { get; }

        public Dictionary<Resource, int> Produces { get; }

        // Fully resolved references:
        public Dictionary<Resource, int> Consumes { get; set; }

        public Task()
        {
            Produces = new Dictionary<Resource, int>();
            Consumes = new Dictionary<Resource, int>();
        }
    }
}
