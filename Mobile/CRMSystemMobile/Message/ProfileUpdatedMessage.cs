using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CRMSystemMobile.Message;

public class ProfileUpdatedMessage(string value) : ValueChangedMessage<string>(value);
