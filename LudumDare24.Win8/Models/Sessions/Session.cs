using System.Runtime.Serialization;

namespace LudumDare24.Models.Sessions
{
    [DataContract]
    public class Session
    {
        [DataMember]
        public int CurrentLevel { get; set; }
    }
}