using BallArchitectureApp.Components;
using BallArchitectureApp.Events;
using DesertImage;
using DesertImage.Subjects;
using UniRx;
using UnityEngine;

namespace BallArchitectureApp.UI
{
    public class SubjectHealthView : MonoBehaviour, IPoolable, IListen<DieEvent>
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        #region PUBLIC METHODS

        public void onCreate()
        {
        }

        public void returnToPool()
        {
            _disposable.Dispose();
            
            Core.Instance.get<FactorySpawn>().returnInstance(gameObject, name);
        }
        
        public void bind(ISubject subject)
        {
            Bind(subject);
        }

        #endregion

        protected virtual void Bind(ISubject subject)
        {
            subject.listen<DieEvent>(this);
            
            var dataHealth = subject.get<DataHealth>();

            dataHealth?.Health.Subscribe(SetValue).AddTo(_disposable);
        }

        protected virtual void SetValue(float value)
        {
        }

        public void handleCallback(DieEvent arguments)
        {
            returnToPool();
        }
    }
}