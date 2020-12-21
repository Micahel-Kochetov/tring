namespace Common.UI
{
    public interface ISelectable
    {
        void SetSelected();
        void SetDeselected();
        void UpdateUIState(bool selected);
        bool IsSelected { get; }
    }
}
