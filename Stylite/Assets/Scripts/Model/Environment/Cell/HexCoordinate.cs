public class HexCoordinate
{

    // Q + R + S = 0
    // S = -(Q + R)
    public int Q;  // Columns
    public int R;  // Rows
    public int S;
    public int H;

    public HexCoordinate(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q - r);
    }

    public bool Equals(HexCoordinate hexCoord)
    {
        return true;
    }
    public void Add()
    {

    }
}
