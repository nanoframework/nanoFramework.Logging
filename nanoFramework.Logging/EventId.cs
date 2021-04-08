//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Event Id structure
    /// </summary>
    public readonly struct EventId
    {
        /// <summary>
        /// Implicitly convert int to EnvetId
        /// </summary>
        /// <param name="i"></param>
        public static implicit operator EventId(int i) => new EventId(i);

        /// <summary>
        /// Equal operator
        /// </summary>
        /// <param name="left">EventId left to compare</param>
        /// <param name="right">EvetnId right to compare</param>
        /// <returns>True if equal</returns>
        public static bool operator ==(EventId left, EventId right) => left.Equals(right);

        /// <summary>
        /// non equal operator
        /// </summary>
        /// <param name="left">EventId left to compare</param>
        /// <param name="right">EvetnId right to compare</param>
        /// <returns>True if not equal</returns>
        public static bool operator !=(EventId left, EventId right) => !left.Equals(right);

        /// <summary>
        /// Creates an EventId
        /// </summary>
        /// <param name="id">The ID number</param>
        /// <param name="name">The associated name</param>
        public EventId(int id, string name = null)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The name, null is none
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <returns>The name or ID if name is null</returns>
        public override string ToString()
        {
            return Name ?? Id.ToString();
        }

        /// <summary>
        /// Check if this EvetnId have the same Id as the other one
        /// </summary>
        /// <param name="other">The EvetnId to compare</param>
        /// <returns>True if Id is equal</returns>
        public bool Equals(EventId other)
        {
            return Id == other.Id;
        }

        /// <summary>
        /// Check if  this EventId is the same object as the other one
        /// </summary>
        /// <param name="obj">The EventId to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is EventId eventId && Equals(eventId);
        }

        /// <summary>
        /// Get the hash code
        /// </summary>
        /// <returns>ID is the hash code</returns>
        public override int GetHashCode()
        {
            return Id;
        }
    }
}