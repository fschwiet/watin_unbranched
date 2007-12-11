namespace WatiN.Core.Interfaces
{
    public interface IImage : IElement
    {
        string Src { get; }

        string Alt { get; }

        string Name { get; }
    }
}