namespace DataRepository.WbEasyCalcData
{
    public interface IWbEasyCalcDataListRepository : IMultiDeleteListRepository<DataModel.WbEasyCalcData>
    {
        int Clone(int id);
    }
}
