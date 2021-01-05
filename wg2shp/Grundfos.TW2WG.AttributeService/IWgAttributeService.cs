namespace Grundfos.TW2WG.AttributeService
{
    public interface IWgAttributeService
    {
        double GetAttributeValue(int wgObjectID, int wgAttributeID);
    }
}