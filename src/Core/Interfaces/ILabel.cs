namespace WatiN.Core.Interfaces
{
    public interface ILabel : IElement
    {
        string AccessKey { get; }

        string For { get; }
    }
}