using System.Collections.Generic;
using DesertImage.Components;
using DesertImage.Entities;
using DesertImage.Extensions;
using UnityEngine;

namespace DesertImage
{
    public static class EntitiesExtensions
    {
        public static void AddToEntities(this IEntity entity)
        {
            if (Core.Instance == null) return;

            var manager = Core.Instance.Get<EntitiesManager>();

            manager?.Add(entity);
        }

        public static void RemoveFromEntities(this IEntity entity)
        {
            if (Core.Instance == null) return;

            var manager = Core.Instance.Get<EntitiesManager>();

            manager?.Remove(entity);
        }

        public static IEntity GetNewEntity(this object sender)
        {
            if (Core.Instance == null) return null;

            var manager = Core.Instance.Get<EntitiesManager>();

            return manager?.GetEntity();
        }

        public static IEntity GetNearestTo(this IEnumerable<IEntity> entities, Transform transform)
        {
            IEntity nearest = null;

            var minDistance = 2500f;

            foreach (var entity in entities)
            {
                var dataTransform = entity.Get<DataTransform>();

                if (dataTransform == null || !dataTransform.Value || dataTransform.Value == transform) continue;

                var distance = dataTransform.Value.GetDistanceTo(transform);

                if (distance >= minDistance) continue;

                nearest = entity;
                minDistance = distance;
            }

            return nearest;
        }

        public static IEntity GetNearestTo(this IEnumerable<IEntity> entities, IEntity entity)
        {
            var dataTransform = entity?.Get<DataTransform>();

            if (dataTransform == null || !dataTransform.Value) return null;

            return GetNearestTo(entities, dataTransform.Value);
        }
    }
}