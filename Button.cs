using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JogoDaVelha;

class Button : IGameObject
{
    Rectangle DestinationRectangle;
    Texture2D texture;
    Color highlightColor;
    Color normalColor;
    bool isMouseOver,
         isVisible,
         isClicking,
         wasClicked;
    MouseState OldMouseState, NewMouseState;
    public Action Action;
    public Vector2 Center {get; private set;}

    public Button(Texture2D texture, Rectangle parameters)
    {
        DestinationRectangle = parameters;
        this.texture = texture;
        normalColor = new Color(200,200,200,255); 
        highlightColor = new Color(100,100,100,255);
    }
    public Button(Texture2D texture, Rectangle parameters, Action action)
    {
        DestinationRectangle = parameters;
        this.texture = texture;
        normalColor = new Color(200,200,200,255); 
        highlightColor = new Color(100,100,100,255);
    }

    public Button(Texture2D texture, Rectangle parameters, Action action, List<IGameObject> subscribeList)
    {
        DestinationRectangle = parameters;
        this.texture = texture;
        normalColor = new Color(200,200,200,255); 
        highlightColor = new Color(100,100,100,255);
    }

    public Button(Texture2D texture, Rectangle parameters, Color StaticColor, Color HighLightColor)
    {
        DestinationRectangle = parameters;
        this.texture = texture;
        normalColor = StaticColor; 
        highlightColor = HighLightColor;
    }

    public Button(Texture2D texture, Vector2 location, Vector2 size, Color StaticColor, Color HighLightColor)
    {
        DestinationRectangle = new Rectangle()  {
                                                    X     = (int)location.X,
                                                    Y     = (int)location.Y,
                                                    Width = (int)size.X,
                                                    Height= (int)size.Y 
                                                };
        this.texture = texture; 
        normalColor = StaticColor; 
        highlightColor = HighLightColor;
    }

    void Subscribe(List<IGameComponent> list)
    {
        try
        {
            list.Add((IGameComponent)this);
        }
        catch (System.Exception)
        {
            throw new Exception("Este objeto não pode inscrever-se a lista");
        }
    }
    public void Draw(SpriteBatch sb)
    {
        if(isVisible){
            sb.Draw(
                texture: this.texture,
                destinationRectangle: DestinationRectangle,
                color : isMouseOver ? highlightColor : normalColor
            );
        }
    }
    public void Update()
    {
        if(isVisible)
        {
            OldMouseState = Mouse.GetState();
            
            NewMouseState = Mouse.GetState();
        }
    }
    void OnClick()
    {
        if (Action != null)
            Action();
    }
    void MouseOver()
    {

    }
    void MouseExit()
    {

    }
}
