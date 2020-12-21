using Common.UI;

namespace Assets.Scripts.States.Common.View
{
    public class ViewManager
    {
        private ModalView currentView;

        public void ShowView(ModalView view)
        {
            if (currentView != null)
            {
                currentView.Hide();
            }
            view.Show();
            currentView = view;
        }

        public void HideView(ModalView view)
        {
            if (currentView != null)
            {
                currentView.Hide();
            }
            view.Hide();
            currentView = null;
        }
    }
}
