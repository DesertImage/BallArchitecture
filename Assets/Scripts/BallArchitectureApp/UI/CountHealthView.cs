using BallArchitectureApp.Components;
using DesertImage.Subjects;
using UnityEngine;
using UnityEngine.UI;

namespace BallArchitectureApp.UI
{
    public class CountHealthView : SubjectHealthView
    {
        [SerializeField] private Text _countLabel;
        [SerializeField] private Image _colorIcon;

        protected override void Bind(ISubject subject)
        {
            base.Bind(subject);

            var dataColor = subject.get<DataColor>();
            
            if(dataColor == null) return;

            _colorIcon.color = dataColor.Value;
        }

        protected override void SetValue(float value)
        {
            _countLabel.text = value.ToString("0");
        }
    }
}