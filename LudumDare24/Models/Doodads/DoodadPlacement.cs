using System.Runtime.Serialization;

namespace LudumDare24.Models.Doodads
{
    [DataContract]
    public class DoodadPlacement
    {
        [DataMember]
        public string DoodadType { get; set; }

        [DataMember]
        public int Column { get; set; }

        [DataMember]
        public int Row { get; set; }
    }
}