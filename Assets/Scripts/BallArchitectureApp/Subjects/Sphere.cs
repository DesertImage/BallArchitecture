using BallArchitectureApp.Behaviours;
using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage.Components;
using DesertImage.Subjects;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp.Subjects
{
    public class Sphere : MonoSubject
    {
        [SerializeField] private Renderer _renderer;

        protected override void InitStuff()
        {
            base.InitStuff();

            add<DataSphere>();
            add<DataColor>();

            add(new DataRenderer {Value = _renderer});
            add(new DataTransform {Value = transform});
            add(new DataHealth {Health = new ReactiveProperty<float>(10f), StartHealth = 10f});

            add<DieBehaviour>();
            add<GetDamageBehaviour>();
            add<GetRandomDamageOnClickBehaviour>();
            add<SetColorBehaviour>();
            add<HealthToScaleBehaviour>();
        }

        private void OnMouseDown()
        {
            send(new ClickEvent());
        }
    }
}