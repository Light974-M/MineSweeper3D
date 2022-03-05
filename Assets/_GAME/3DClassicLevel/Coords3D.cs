namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// coordinate system for Cells map of mineSweeper2D
    ///</summary>
    public struct Coords3D
    {
        public int x;
        public int y;
        public int z;

        public Coords3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Coords3D(int x, int y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }

        public static Coords3D Zero
        {
            get { return new Coords3D(0, 0, 0); }
        }

        public static Coords3D One
        {
            get { return new Coords3D(1, 1, 1); }
        }

        public static Coords3D Ten
        {
            get { return new Coords3D(10, 10, 10); }
        }
    } 
}
