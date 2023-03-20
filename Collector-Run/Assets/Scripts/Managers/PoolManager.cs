using System.Collections.Generic;
using System.Linq;
using Bases;
using Extenders;

namespace Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private List<PlatformBase> _platformBases;
        private List<BallPackBase> _ballPackBases;

        public PlatformBase GetAvailablePlatform(PlatformType platformType)
        {
            if(_platformBases == null)
                _platformBases = new List<PlatformBase>();
            
            var platform = _platformBases?.FirstOrDefault(x => !x.isActive && x.PlatformType == platformType);
            if (platform == null)
            {
                platform = AssetManager.Instance.GetPlatform(platformType);
                platform = Instantiate(platform, transform);
                platform.Initialize();
                _platformBases?.Add(platform);
            }
            
            platform.Activate();
            return platform;
        }

        public BallPackBase GetAvailableBallPack(BallPackType ballPackType)
        {
            if(_ballPackBases == null)
                _ballPackBases = new List<BallPackBase>();

            var ball = _ballPackBases?.FirstOrDefault(x => !x.isActive && x.ballPackType == ballPackType);
            if (ball == null)
            {
                ball = AssetManager.Instance.GetBallPack(ballPackType);
                ball = Instantiate(ball, transform);
                ball.Initialize();
                _ballPackBases?.Add(ball);
            }
            
            ball.Activate();
            return ball;
        }
        

        public void DeactivateWholePool()
        {
            if(_platformBases.Count <= 0)
                return;
            
            foreach (var platform in _platformBases)
            {
                platform.Deactivate();
            }

            foreach (var ball in _ballPackBases)
            {
                ball.Deactivate();
            }
        }
    }
}
