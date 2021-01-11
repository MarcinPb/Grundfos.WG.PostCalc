namespace DataRepository.WaterConsumption
{
    public interface IListRepository : IMultiDeleteListRepository<DataModel.WbEasyCalcData>
    {
        int Clone(int id);
        int CreateAll();
    }
}
