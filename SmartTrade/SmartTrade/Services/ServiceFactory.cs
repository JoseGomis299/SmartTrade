using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Services
{
    public class ServiceFactory
    {
        private static SmartTradeProxy? _proxyInstance;

        private static SmartTradeService? _brokerInstance;

        public SmartTradeProxy GetProxy()
        {
            return _proxyInstance ??= new SmartTradeProxy();
        }

        public SmartTradeService GetService()
        {
            return _brokerInstance ??= new SmartTradeService(GetProxy());
        }

        public void Reset()
        {
            _proxyInstance = null;
            _brokerInstance = null;
        }
    }
}
