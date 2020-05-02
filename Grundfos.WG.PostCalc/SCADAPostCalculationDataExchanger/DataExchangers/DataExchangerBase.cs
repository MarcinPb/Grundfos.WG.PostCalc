using Haestad.Domain;
using Haestad.Support.OOP.Logging;

namespace Grundfos.WG.PostCalc.DataExchangers
{
    public abstract class DataExchangerBase
    {
        public DataExchangerBase(ActionLogger logger, IScenario scenario, IDomainDataSet domainDataSet)
        {
            this.Logger = logger;
            this.Scenario = scenario;
            this.DomainDataSet = domainDataSet;
        }

        public ActionLogger Logger { get; }
        public IScenario Scenario { get; }
        public IDomainDataSet DomainDataSet { get; }
        public abstract bool DoDataExchange(object dataExchangeContext);
    }
}
