using System;
using System.Collections.Generic;
using tasks_and_resources.Data;

// Builder / Mapper
namespace tasks_and_resources.Core
{
    internal static class TaskAndResourceFactory
    {
        public static IReadOnlyDictionary<string, Resource> BuildResources(IEnumerable<ResourceDto> dtos)
        {
            // 1) Registry (case-insensitive, avoids duplicate keys by casing)
            var registry = new Dictionary<string, Resource>(StringComparer.OrdinalIgnoreCase);

            // 2) Create Resource shells with empty consumes for now
            var pending = new List<(Resource model, ResourceDto dto)>();
            foreach (var dto in dtos)
            {
                if (registry.ContainsKey(dto.Name))
                    throw new InvalidOperationException($"Duplicate resource name: {dto.Name}");

                var model = new Resource(
                    dto.Name, dto.Category, dto.Type,
                    dto.BaseValue, dto.Weight, dto.DecayRate,
                    consumes: new Dictionary<Resource, int>() // temp, will replace in pass 2
                );

                registry.Add(dto.Name, model);
                pending.Add((model, dto));
            }

            // 3) Second pass: resolve consumes
            foreach (var (model, dto) in pending)
            {
                var resolved = new Dictionary<Resource, int>();
                foreach (var kv in dto.Consumes)
                {
                    if (!registry.TryGetValue(kv.Key, out var dep))
                        throw new KeyNotFoundException($"Resource '{model.Name}' consumes unknown resource '{kv.Key}'.");

                    resolved[dep] = kv.Value;
                }

                // replace temporary dictionary with an immutable view
                var readOnly = new Dictionary<Resource, int>(resolved);
                // reflection-free update via a new Resource, or make Consumes settable internally:
                ReplaceConsumes(model, readOnly);
            }

            return registry;
        }

        // One simple pattern: internal setter or a small helper that uses an internal constructor
        static void ReplaceConsumes(Resource r, IReadOnlyDictionary<Resource,int> resolved)
        {
            // If you prefer strict immutability, instead of mutating,
            // build the objects in a slightly different way:
            //  - first pass create a lightweight 'ResourceCore' (no consumes)
            //  - second pass produce final 'Resource' instances and replace in registry.
            // For brevity here, we’d change Resource to have an internal set; not shown.
            // In practice choose one approach and stick to it.
            ((Dictionary<Resource,int>)r.Consumes).Clear(); // if you used a temp dictionary
            foreach (var kv in resolved)
                ((Dictionary<Resource,int>)r.Consumes).Add(kv.Key, kv.Value);
        }
    }
}
