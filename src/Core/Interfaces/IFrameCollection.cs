namespace WatiN.Core.Interfaces
{
    public interface IFrameCollection : IBaseElementCollection
    {
        IFrame this[int index] { get; }
    }
}