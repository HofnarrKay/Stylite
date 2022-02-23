public struct Position
{
    //Looks stupid, i will add an hexagonal field later on
    public int Height;

    public Position(int height)
    {
        Height = height;
    }

    public override bool Equals(object obj)
    {
        Position position = (Position)obj;

        if (position.Height != Height) return false;

        return true;
    }

    public override int GetHashCode()
    {
        return Height;
    }
}
