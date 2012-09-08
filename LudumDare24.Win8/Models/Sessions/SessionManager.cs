using System;
using System.IO;
using System.Runtime.Serialization;

namespace LudumDare24.Models.Sessions
{
    public class SessionManager
    {
        private readonly Session session;
        private readonly string directoryPath;
        private readonly string sessionPath;


        public SessionManager(Session session)
        {
            this.session = session;
            this.directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MoveTheCheese");
            this.sessionPath = Path.Combine(this.directoryPath, "Session.xml");

        }

        public void WriteSession()
        {
            if (!Directory.Exists(this.directoryPath))
            {
                Directory.CreateDirectory(this.directoryPath);
            }

            using (var stream = File.Open(this.sessionPath, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Session));
                serializer.WriteObject(stream, this.session);
            }
        }

        public void ReadSession()
        {
            if (!Directory.Exists(this.directoryPath) || !File.Exists(this.sessionPath))
            {
                this.session.CurrentLevel = 1;
                return;
            }

            Session restoredSession;
            using (var stream = File.OpenRead(this.sessionPath))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Session));
                restoredSession = (Session)serializer.ReadObject(stream);
            }

            this.session.CurrentLevel = restoredSession.CurrentLevel;
        }
    }
}