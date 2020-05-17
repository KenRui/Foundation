using System.Collections;

namespace XRFramework.Common
{
    public interface IManager
    {
        IEnumerable Initialize();
        void Shutdown();
        void BeforeUpdate();
        void Update();
        void LateUpdate();
        void FixUpdate();
    }
}