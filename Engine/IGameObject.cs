using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace MonoControls;

interface IGameObject
{
    void Draw(SpriteBatch sb);
    void Update(GameTime gt);

    void LoadContent();
    void UnloadContent();
}