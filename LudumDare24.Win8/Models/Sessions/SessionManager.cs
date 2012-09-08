using Windows.Storage;

namespace LudumDare24.Models.Sessions
{
    public class SessionManager
    {
        private const string CurrentLevelKey = "CurrentLevel";

        private readonly Session session;

        public SessionManager(Session session)
        {
            this.session = session;
        }

        public void WriteSession()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values[SessionManager.CurrentLevelKey] = this.session.CurrentLevel;
        }

        public void ReadSession()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey(SessionManager.CurrentLevelKey))
            {
                this.session.CurrentLevel = (int)roamingSettings.Values[SessionManager.CurrentLevelKey];
            }
            else
            {
                this.session.CurrentLevel = 1;
            }
        }
    }
}