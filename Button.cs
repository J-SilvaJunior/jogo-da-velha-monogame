using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoControls;

class Button : IGameObject
{
    String Text;
    Rectangle DestinationRectangle;
    Texture2D texture;
    Color highlightColor;
    Color normalColor;
    bool isMouseOver,
         isVisible,
         isClicking,
         wasClicked;
    MouseState OldMouseState, NewMouseState;
    Vector2 OriginalPosition, NewPosition;
    public Action ActionLeftClick;
    public Action ActionMiddleClick;
    public Action ActionRightClick;
    public Action ActionMouseOver;
    public Action ActionMouseExit;
    public Action ActionMouseHold;
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

    //esse construtor é temporário, é somente para testar se tudo vai dar certo pegando referencias
    public Button(Texture2D texture, Rectangle parameters, Action action, List<IGameObject> subscribeList, MouseState NMS, MouseState OMS)
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
            throw new Exception("Este objeto não pôde inscrever-se a lista");
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
    /*
    public void Update()
    {
        
    } //*/
    void LeftClick()
    {
        if (ActionLeftClick != null)
            ActionLeftClick();
    }

    void MouseOver()
    {

    }
    void MouseExit()
    {

    }
    void MouseDown()
    {

    }

    
    public void LoadContent()
    {

    }
    public void UnloadContent()
    {

    }

    public void Update(GameTime gt)
    {
        var click = (MouseState m1,MouseState  m2, out Vector2 oriPos) => { 
            oriPos = m2.Position.ToVector2();return m1.LeftButton == ButtonState.Released && m2.LeftButton == ButtonState.Pressed;
        };
        
        var completeClick = (MouseState v1, MouseState v2, Rectangle bounds, Vector2 oriPos) => {
            return v1.LeftButton == ButtonState.Pressed && v2.LeftButton == ButtonState.Released && bounds.Contains(oriPos);
        };
        var over = (MouseState m1, Rectangle bounds) => {
            return bounds.Contains(m1.Position);
        };

        if(isVisible)
        {
            //Mar
            if (OldMouseState != NewMouseState)
            {
                if(click(OldMouseState, NewMouseState, out OriginalPosition))
                {
                    MouseDown();
                }    
            
                if(completeClick(OldMouseState, NewMouseState, DestinationRectangle, OriginalPosition))
                {
                    LeftClick();
                }
                if(over(NewMouseState, DestinationRectangle) && !isMouseOver)
                {
                    isMouseOver = true;
                    MouseOver();
                }
                if(!over(NewMouseState, DestinationRectangle) && isMouseOver)
                {
                    isMouseOver = false;
                    MouseExit();
                }
            }
        }
    }



}
