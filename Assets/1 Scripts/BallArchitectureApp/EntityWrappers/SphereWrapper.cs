using Components;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp
{
    public class SphereWrapper : EntityComponentWrapper
    {
        [SerializeField] private DataRenderer dataRenderer;

        public override void Link(IComponentHolder componentHolder)
        {
            base.Link(componentHolder);

            componentHolder.Add(dataRenderer);

            componentHolder.Add<DataSphere>();
            componentHolder.Add<DataColor>();

            componentHolder.Add
            (
                new DataHealth
                {
                    Health = new ReactiveProperty<float>(10f),
                    StartHealth = 10f
                }
            );

            componentHolder.Add<DieBehaviour>();
            componentHolder.Add<GetDamageBehaviour>();
            componentHolder.Add<GetRandomDamageOnClickBehaviour>();
            componentHolder.Add<ColorManagingBehaviour>();
            componentHolder.Add<HealthToScaleBehaviour>();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (dataRenderer != null && !dataRenderer.Value)
            {
                dataRenderer.Value = GetComponentInChildren<Renderer>();
            }
        }
    }
}