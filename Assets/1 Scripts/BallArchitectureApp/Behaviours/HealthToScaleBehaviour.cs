using DesertImage.Components;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp
{
    public class HealthToScaleBehaviour : DesertImage.Behaviours.Behaviour
    {
        private DataTransform _dataTransform;

        public override void Activate()
        {
            base.Activate();

            _dataTransform = Entity.Get<DataTransform>();

            var dataHealth = Entity.Get<DataHealth>();
            dataHealth.Health.Where(health => health >= 0f).Subscribe(SetScale);
        }

        private void SetScale(float value)
        {
            LeanTween.cancel(_dataTransform.Value.gameObject);

            _dataTransform.Value.gameObject
                .LeanScale(Vector3.one * value, .3f)
                .setEaseOutBack();
        }
    }
}