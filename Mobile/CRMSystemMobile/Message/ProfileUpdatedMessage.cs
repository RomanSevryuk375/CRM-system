using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CRMSystemMobile.Message;

public class ProfileUpdatedMessage : ValueChangedMessage<string>
{
    public ProfileUpdatedMessage(string value) : base(value) { }
}
