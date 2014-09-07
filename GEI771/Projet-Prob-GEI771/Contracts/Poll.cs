using System.Runtime.Serialization;

namespace Definitions
{
    /// <summary>
    /// Cette classe définit un sondage.
    /// </summary>
    [DataContract]
    public class Poll
    {
        /// <summary>
        /// L'identifiant du sondage.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// La description du sondage.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Poll [Id = " + Id + ", Description = " + Description + "]";
        }
    }
}
