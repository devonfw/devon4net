using Excalibur.Shared.Business;

namespace Excalibur.Shared.Utils
{
    /// <summary>
    /// This class is used for communication by <see cref="PubSub"/> to notify an object has changed. 
    /// This can include the actual object that has been updated or just a generic object/list.
    /// 
    /// An instance of this class should be used when notifying for a certain object.
    /// </summary>
    /// <typeparam name="T">The type of the object that should by used for notification</typeparam>
    public class MessageBase<T>
    {
        /// <summary>
        /// Initializes a new instance of MessageBase when wanting to notify with a certain object.
        /// State will be defaulted to Updated.
        /// </summary>
        /// <param name="object">The object that should be passed along</param>
        public MessageBase(T @object)
        {
            Object = @object;
        }

        /// <summary>
        /// Initializes a new instance of MessageBase when wanting to notify with a certain object together with a given State.
        /// </summary>
        /// <param name="object">The object that should be passed along</param>
        /// <param name="state">The state of the object</param>
        public MessageBase(T @object, EDomainState state)
        {
            Object = @object;
            State = state;
        }

        /// <summary>
        /// The object that is passed along with the message
        /// </summary>
        public T Object { get; set; }
        /// <summary>
        /// The state of the <see cref="Object"/>
        /// </summary>
        public EDomainState State { get; set; } = EDomainState.Updated;
    }
}
