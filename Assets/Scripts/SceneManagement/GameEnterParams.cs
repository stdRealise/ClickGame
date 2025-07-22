namespace SceneManagement
{
    public class GameEnterParams : SceneEnterParams
    {
        public int Location;   //...
        public int Level;
        public GameEnterParams(int location, int level) : base(Scenes.LevelScene)
        {
            Location = location;
            Level = level;
        }
    }
}