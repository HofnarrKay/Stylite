public class MoveSocketContentActionArguments : ActionArguments
{
    public SocketPositionInformation ContentOwner;
    public SocketPositionInformation ContentDestination;

    public MoveSocketContentActionArguments(string key, SocketPositionInformation contentOwner, SocketPositionInformation contentDestination) : base(key)
    {
        ContentOwner = contentOwner;
        ContentDestination = contentDestination;
    }
}
