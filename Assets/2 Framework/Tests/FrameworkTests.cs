using System.Collections;
using DesertImage;
using DesertImage.Extensions;
using DesertImage.Managers;
using DesertImage.Pools;
using Framework.Managers;
using UnityEngine;
using UnityEngine.TestTools;

public class FrameworkTests
{
    [UnityTest]
    public IEnumerator TestPoolGameObject()
    {
        var core = new Core();

        core.Add(new ManagerUpdate());
        core.Add(new ManagerTimers());

        var poolParent = new GameObject("Pools");

        var pool = new PoolGameObject(poolParent.transform);

        var pref = new GameObject("TestPrefab");

        pool.Register(pref, 3);

        for (var i = 0; i < 20; i++)
        {
            var instance = pool.GetInstance(pref);

            yield return new WaitForSecondsRealtime(Random.Range(.05f, .3f));

            this.DoActionWithDelay(() => pool.ReturnInstance(instance), Random.Range(.1f, .5f));
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestPoolInstanceId()
    {
        var poolParent = new GameObject("Pools");

        var pool = new PoolGameObject(poolParent.transform);

        var pref = new GameObject("TestPrefab");

        pool.Register(pref, 4);

        var instance = pool.GetInstance(pref);

#if DEBUG
        UnityEngine.Debug.LogError($"[FrameworkTests] gettet instnce id {instance.GetInstanceID()}");
#endif

        pool.ReturnInstance(instance);

        var secondInstance = pool.GetInstance(pref);

#if DEBUG
        UnityEngine.Debug.LogError($"[FrameworkTests] cloned instance id {secondInstance.GetInstanceID()}");
#endif


        yield return null;
    }
}