using Shadowsocks.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.Controller.Strategy
{
    class StrategyManager
    {
        private List<IStrategy> _strategies;

        public StrategyManager(ShadowsocksController controller)
        {
            // TODO: load DLL plugins
            _strategies = new List<IStrategy>();
            _strategies.Add(new BalancingStrategy(controller));
            _strategies.Add(new HighAvailabilityStrategy(controller));
        }
        public IList<IStrategy> GetStrategies()
        {
            return _strategies;
        }
    }
}
