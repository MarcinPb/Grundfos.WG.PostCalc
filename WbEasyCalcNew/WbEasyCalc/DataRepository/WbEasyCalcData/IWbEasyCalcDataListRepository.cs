namespace DataRepository.WbEasyCalcData
{
    public interface IWbEasyCalcDataListRepository : IMultiDeleteListRepository<DataModel.WbEasyCalcData>
    {
        void SetUpRank(int id);
    }
}
