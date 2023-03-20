using System.Collections.Generic;
using System.Linq;
using Extenders;
using Game;
using Game.PlatformSystem.PlatformTypes;

namespace Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private List<Platform> _platforms;
        private List<ObjectGroup> _objectGroups;

        public Platform GetAvailablePlatform(PlatformType platformType)
        {
            if(_platforms == null)
                _platforms = new List<Platform>();
            
            var platform = _platforms?.FirstOrDefault(x => !x.isActive && x.PlatformType == platformType);
            if (platform == null)
            {
                platform = AssetManager.Instance.GetPlatform(platformType);
                platform = Instantiate(platform, transform);
                platform.Initialize();
                _platforms?.Add(platform);
            }
            
            platform.Activate();
            return platform;
        }

        public ObjectGroup GetAvailableObjectGroup(ObjectGroupType objectGroupType)
        {
            if(_objectGroups == null)
                _objectGroups = new List<ObjectGroup>();

            var objectGroup = _objectGroups?.FirstOrDefault(x => !x.isActive && x.objectGroupType == objectGroupType);
            if (objectGroup == null)
            {
                objectGroup = AssetManager.Instance.GetObjectGroup(objectGroupType);
                objectGroup = Instantiate(objectGroup, transform);
                objectGroup.Initialize();
                _objectGroups?.Add(objectGroup);
            }
            
            objectGroup.Activate();
            return objectGroup;
        }
        

        public void DeactivateWholePool()
        {
            if(_platforms is not { Count: > 0 })
                return;
            
            foreach (var platform in _platforms)
            {
                platform.Deactivate();
            }

            foreach (var objectGroup in _objectGroups)
            {
                objectGroup.Deactivate();
            }
        }
    }
}
