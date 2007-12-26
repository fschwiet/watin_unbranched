namespace WatiN.Core.Interfaces
{
    public interface IRadioCheck : IElement
    {
        bool Checked { get; set; }

        string ToString();
    }
}