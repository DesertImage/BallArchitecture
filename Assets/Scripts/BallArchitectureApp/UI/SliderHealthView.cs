using BallArchitectureApp.Components;
using DesertImage.Subjects;
using UnityEngine;
using UnityEngine.UI;

namespace BallArchitectureApp.UI
{
    public class SliderHealthView : SubjectHealthView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _colorIcon;

        private float _maxValue;
        
        protected override void Bind(ISubject subject)
        {
            base.Bind(subject);

            _maxValue = subject.get<DataHealth>().StartHealth;
            
            var dataColor = subject.get<DataColor>();
            
            if(dataColor == null) return;

            _colorIcon.color = dataColor.Value;
        }

        protected override void SetValue(float value)
        {
            _slider.value = value / _maxValue;
        }
    }
}