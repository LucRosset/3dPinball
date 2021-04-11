namespace Pinball.Props
{
    public interface IButton
    {
        ///<summary>
        /// Returns the implementing component's InstanceID.
        ///</summary>
        ///<return>
        /// The implementing component's InstanceID.
        ///</return>
        int GetButtonInstanceId();

        ///<summary>
        /// Returns the implementing component's state.
        ///</summary>
        ///<return>
        /// The implementing component's state.
        ///</return>
        bool GetButtonState();

        ///<summary>
        /// Interface method to set the Button's state.
        ///</summary>
        ///<param name="state">
        /// State to set the button prop to.
        ///</param>
        void SetButtonState(bool state);
    }
}