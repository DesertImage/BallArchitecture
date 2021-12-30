using Components;
using DesertImage;
using DesertImage.Entities;
using Framework.FX.DataComponents;
using UnityEngine;
using Behaviour = DesertImage.Behaviours.Behaviour;

namespace Coloring
{
    public class ParticleSystemColorBehaviour : Behaviour, IListen<ChangeColorEvent>
    {
        private DataParticleSystem _dataParticleSystem;

        public override void Activate()
        {
            base.Activate();

            Entity.ListenEvent<ChangeColorEvent>(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Entity.UnlistenEvent<ChangeColorEvent>(this);
        }

        public override void Link(IEntity entity)
        {
            base.Link(entity);

            _dataParticleSystem = entity.Get<DataParticleSystem>();
        }

        public void handleCallback(ChangeColorEvent arguments)
        {
            var mainModule = _dataParticleSystem.Value.main;
            
            mainModule.startColor = new ParticleSystem.MinMaxGradient
            {
                color = arguments.Value
            };
        }
    }
}