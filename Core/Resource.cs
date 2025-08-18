using System.Collections.Generic;

namespace tasks_and_resources.Core
{
    // Models (runtime graph)
    public sealed class Resource
    {
        public string Name { get; }
        public string Category { get; }
        public string Type { get; }
        public int BaseValue { get; }
        public int Weight { get; }
        public int DecayRate { get; }

        // Fully resolved references:
        public IReadOnlyDictionary<Resource, int> Consumes { get; }

        // Example computed property you might add later:
        //public int TotalMassCost => Consumes.Sum(kv => kv.Key.Weight * kv.Value);

        public Resource(
            string name, string category, string type,
            int baseValue, int weight, int decayRate,
            IReadOnlyDictionary<Resource,int> consumes)
        {
            Name = name;
            Category = category;
            Type = type;
            BaseValue = baseValue;
            Weight = weight;
            DecayRate = decayRate;
            Consumes = consumes;
        }
    }
}