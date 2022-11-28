using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace JogoDaVelha;

interface IGameObject
{
    public virtual void Draw()
    {

    }
    public virtual void Update(GameTime gt)
    {

    }
    public virtual void LoadContent()
    {

    }
    public virtual void UnloadContent()
    {

    }
}