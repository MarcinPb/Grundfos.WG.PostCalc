using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grundfos.WG.Model;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects;
using Haestad.Domain.ModelingObjects.Water;
using Haestad.Support.Support;

namespace Grundfos.WG.ObjectReaders
{
    public class WaterDemandPatternCurveReader
    {
        public WaterDemandPatternCurveReader(IDomainDataSet domainDataSet)
        {
            this.DomainDataSet = domainDataSet;
        }

        public IDomainDataSet DomainDataSet { get; }

        public Dictionary<string, int> GetPatterns()
        {
            var ids = ((IdahoDomainDataSet)this.DomainDataSet).IdahoPatternElementManager.Elements()
                .Cast<ModelingElementBase>()
                .ToDictionary(x => x.Label, x => x.Id, StringComparer.OrdinalIgnoreCase);
            ids[Constants.FixedPattern] = -1;

            return ids;
        }

        public IList<IdahoPatternPatternCurve> GetIdahoPatternPatternCurveList(IList<int> idahoPatternIdList)
        {
            //int supportElementId = 19098;
            var idahoPatternPatternCurveList = new List<IdahoPatternPatternCurve>();

            ISupportElementManager patternManager =
                this.DomainDataSet.SupportElementManager((int)SupportElementType.IdahoPatternElementManager);

            IEditField transientValveCurveField =
                patternManager.SupportElementField(StandardFieldName.PatternCurve) as IEditField;

            foreach (var idahoPattern in idahoPatternIdList)
            {
                ICollectionFieldListManager cflm = (ICollectionFieldListManager)transientValveCurveField.GetValue(idahoPattern);

                IUnitizedField timeFromStart = cflm.Field(StandardFieldName.PatternCurve_TimeFromStart) as IUnitizedField;
                IUnitizedField multiplier = cflm.Field(StandardFieldName.PatternCurve_Multiplier) as IUnitizedField;

                for (int i = 0; i < cflm.Count; ++i)
                {
                    idahoPatternPatternCurveList.Add(new IdahoPatternPatternCurve()
                    {
                        SupportElementId= idahoPattern,
                        PatternCurveTimeFromStart = timeFromStart.GetDoubleValue(i),
                        PatternCurveMultiplier = multiplier.GetDoubleValue(i),
                    });
                }
            }

            return idahoPatternPatternCurveList;
        }
    }
}
