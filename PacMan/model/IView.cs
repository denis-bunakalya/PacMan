namespace PacMan.model
{
    interface IView
    {
        void NotifyModelChanges();
        void NotifyNewLevel();
    }
}
