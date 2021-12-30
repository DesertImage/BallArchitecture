﻿using Framework.Managers;using UnityEngine;namespace DesertImage{    public class ComponentUpdate : MonoBehaviour    {        private ManagerUpdate _managerUpdate;        #region MONO BEHAVIOUR METHODS        private void Update()        {            ManagerTick();        }        private void FixedUpdate()        {            ManagerFixedTick();        }        private void LateUpdate()        {            ManagerLateTick();        }        #endregion        public void Setup(ManagerUpdate managerUpdate)        {            _managerUpdate = managerUpdate;        }        private void ManagerTick()        {            _managerUpdate?.Tick();        }        private void ManagerFixedTick()        {            _managerUpdate?.FixedTick();        }        private void ManagerLateTick()        {            _managerUpdate?.LateTick();        }    }}