using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Behaviours;
using DesertImage.Subjects;

namespace BallArchitectureApp.Behaviours
{
    public class SetColorBehaviour : BehaviourBase, IListen<SetColorEvent>
    {
        private DataColor _dataColor;
        private DataRenderer _dataRenderer;

        protected override void Link(ISubject subject)
        {
            base.Link(subject);

            subject.listen<SetColorEvent>(this);

            _dataColor = subject.get<DataColor>();
            _dataRenderer = subject.get<DataRenderer>();
        }

        public void handleCallback(SetColorEvent arguments)
        {
            _dataColor.Value = arguments.Value;
            _dataRenderer.Value.SetColor(arguments.Value);
        }
    }
}