namespace MonoControls;
interface IGamePiece
{
    void Draw(SpriteBatch sb);
    void Update(GameTime gt);
    void LoadContent();
    void UnloadContent();
}