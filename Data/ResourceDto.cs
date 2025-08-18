using System.Collections.Generic;

namespace tasks_and_resources.Data
{
    public sealed class ResourceDto
    {
        public string Name { get; set; }
        public string Category { get; set; }  // consider enum later
        public string Type { get; set; }      // consider enum later
        public int BaseValue { get; set; }
        public int Weight { get; set; }
        public int DecayRate { get; set; }
        public Dictionary<string, int> Consumes { get; set; } = new Dictionary<string, int>();
    }
}