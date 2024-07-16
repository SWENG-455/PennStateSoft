namespace PennStateSoft
{
    public class Room
    {
        private static int num = 0;
        private int number = 0;
        private bool special = false;
        private Room(bool special)
        {
            num++;
            this.special = special;
        }
    }
}
