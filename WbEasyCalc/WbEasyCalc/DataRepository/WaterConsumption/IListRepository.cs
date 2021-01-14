namespace DataRepository.WaterConsumption
{
    public interface IListRepository : IMultiDeleteListRepository<DataModel.WaterConsumption>
    {
        int Clone(int id);
    }
}
