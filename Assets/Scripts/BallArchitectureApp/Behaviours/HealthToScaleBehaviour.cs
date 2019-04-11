using BallArchitectureApp.Components;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Components;
using DesertImage.Subjects;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp.Behaviours
{
    public class HealthToScaleBehaviour : BehaviourBase
    {
        private DataTransform _dataTransform;
        
        protected override void Link(ISubject subject)
        {
            base.Link(subject);

            _dataTransform = subject.get<DataTransform>();
            
            var dataHelath = subject.get<DataHealth>();

            dataHelath.Health.Where(health => health >= 0f).Subscribe(SetScale);
        }

        private void SetScale(float value)
        {
            LeanTween.cancel(_dataTransform.Value.gameObject);
            
            _dataTransform.Value.TweenScale(Vector3.one * value);
        }
    }
}