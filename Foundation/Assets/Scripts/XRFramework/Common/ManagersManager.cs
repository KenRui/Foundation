using System;
using System.Collections;
using System.Collections.Generic;

namespace XRFramework.Common
{
    public sealed class ManagersManager : BaseSingleton<ManagersManager>, IManager
    {
        private List<KeyValuePair<int, IManager>> managers = new List<KeyValuePair<int, IManager>>();

        public int Count => managers.Count;

        public void Register(IManager manager, int order)
        {
            var managerPair = new KeyValuePair<int, IManager>(order, manager);
            managers.Add(managerPair);
            managers.Sort(SortList);
        }

        private int SortList(KeyValuePair<int, IManager> x, KeyValuePair<int, IManager> y)
        {
            if (x.Key > y.Key)
            {
                return 1;
            }
            else
            {
                return -1;
            }

            return 0;
        }

        public IEnumerable Initialize()
        {
            for (var i = 0; i < managers.Count; ++i)
            {
                yield return managers[i].Value.Initialize();
            }
        }

        public void Shutdown()
        {
            for (var i = managers.Count - 1; i >= 0; i--)
            {
                managers[i].Value.Shutdown();
            }
            managers.Clear();
        }

        public void BeforeUpdate()
        {
            for (var i = 0; i < managers.Count; ++i)
            {
                managers[i].Value.BeforeUpdate();
            }
        }

        public void Update()
        {
            for (var i = 0; i < managers.Count; ++i)
            {
                managers[i].Value.Update();
            }
        }

        public void LateUpdate()
        {
            for (var i = 0; i < managers.Count; ++i)
            {
                managers[i].Value.LateUpdate();
            }
        }

        public void FixUpdate()
        {
            for (var i = 0; i < managers.Count; ++i)
            {
                managers[i].Value.FixUpdate();
            }
        }
    }
}