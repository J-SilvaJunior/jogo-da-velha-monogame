using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JogoDaVelha;
public class Game1 : Game
{
    Vector2 scrSize = new Vector2() {
                                        X = 600,
                                        Y = 300
                                    };
    Dictionary<(short, short), char> bS;
        
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    char[] square = {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    List<Rectangle> grid;
    Rectangle replayButton = new Rectangle(350,200, 100, 50);
    Rectangle exitButton   = new Rectangle(451, 200, 100, 50);
    Texture2D cross, circle, board, dot;
    MouseState msOld, msNew;
    SpriteFont gameFont;
    Vector4 highLightedButtonColor = new Vector4()  {
                                                        X = 0.5f, //R
                                                        Y = 0.5f, //G
                                                        Z = 0.5f, //B
                                                        W = 1f  //A
                                                    };
    Vector4 nonHighLightedButtonColor = new Vector4()   {
                                                            X = 0.3f, //R
                                                            Y = 0.3f, //G
                                                            Z = 0.3f, //B
                                                            W = 1f  //A
                                                        };
    Vector4 replayButtonColor, exitButtonColor;

    enum GameStateDetails 
    {
        Begin,      //Esse é o estado inicial do jogo, antes de qualquer movimento ser feito.
        Playing,    //Após um movimento ser feito, o estado do jogo é este.
        PlayerXWon, //Após chamar o checkForWinners e ele retornar "X". o estado se torna este
        PlayerOWon, //Após chamar o checkForWinners e ele retornar "O". o estado se torna este
        Draw,       //Após chamar o checkForWinners e ele retornar "Draw".
        GameEnd     //Este estado é aplicado após a validação do resultado, e seta o jogo para o estado inicial
    };
    GameStateDetails gameState;
    string checkForWinner()
    {
        var player = 'X';
        bS.Clear();
        short k = 0;
        for (short i = 0; i < 3; i++)
        {
            for (short j = 0; j < 3; j++)
            {
                bS.Add((i,j), square[k]);
                k++;
            }
        }
        
        /*
            (1,1) (1,2) (1,3)
            (2,1) (2,2) (2,3)
            (3,1) (3,2) (3,3)
        */
            // check rows
            if (bS[(0, 0)] == player && bS[(0, 1)] == player && bS[(0, 2)] == player) { return player.ToString(); }
            if (bS[(1, 0)] == player && bS[(1, 1)] == player && bS[(1, 2)] == player) { return player.ToString(); }
            if (bS[(2, 0)] == player && bS[(2, 1)] == player && bS[(2, 2)] == player) { return player.ToString(); }

            // check columns
            if (bS[(0, 0)] == player && bS[(1, 0)] == player && bS[(2, 0)] == player) { return player.ToString(); }
            if (bS[(0, 1)] == player && bS[(1, 1)] == player && bS[(2, 1)] == player) { return player.ToString(); }
            if (bS[(0, 2)] == player && bS[(1, 2)] == player && bS[(2, 2)] == player) { return player.ToString(); }

            // check diags
            if (bS[(0, 0)] == player && bS[(1, 1)] == player && bS[(2, 2)] == player) { return player.ToString(); }
            if (bS[(0, 2)] == player && bS[(1, 1)] == player && bS[(2, 0)] == player) { return player.ToString(); }

            player = 'O';

            if (bS[(0, 0)] == player && bS[(0, 1)] == player && bS[(0, 2)] == player) { return player.ToString(); }
            if (bS[(1, 0)] == player && bS[(1, 1)] == player && bS[(1, 2)] == player) { return player.ToString(); }
            if (bS[(2, 0)] == player && bS[(2, 1)] == player && bS[(2, 2)] == player) { return player.ToString(); }

            // check columns
            if (bS[(0, 0)] == player && bS[(1, 0)] == player && bS[(2, 0)] == player) { return player.ToString(); }
            if (bS[(0, 1)] == player && bS[(1, 1)] == player && bS[(2, 1)] == player) { return player.ToString(); }
            if (bS[(0, 2)] == player && bS[(1, 2)] == player && bS[(2, 2)] == player) { return player.ToString(); }

            // check diags
            if (bS[(0, 0)] == player && bS[(1, 1)] == player && bS[(2, 2)] == player) { return player.ToString(); }
            if (bS[(0, 2)] == player && bS[(1, 1)] == player && bS[(2, 0)] == player) { return player.ToString(); }

        /*
        
        //checagem horizontal
        if (bS[(1,1)] == bS[(1,2)] && bS[(1,1)] == bS[(1,3)]) 
            return bS[(1,1)].ToString();
        
        if (bS[(2,1)] == bS[(2,2)] && bS[(2,1)] == bS[(2,3)]) 
            return bS[(2,1)].ToString();
        
        if (bS[(3,1)] == bS[(3,2)] && bS[(3,1)] == bS[(3,3)]) 
            return bS[(3,1)].ToString();

        //cheacagem vertical
        if (bS[(1,1)] == bS[(2,1)] && bS[(1,1)] == bS[(3,1)]) 
            return bS[(1,1)].ToString();

        if (bS[(1,2)] == bS[(2,2)] && bS[(1,2)] == bS[(3,2)]) 
            return bS[(1,2)].ToString();

        if (bS[(1,3)] == bS[(2,3)] && bS[(1,3)] == bS[(3,3)]) 
            return bS[(1,3)].ToString();

        //checagem diagonal
        if (bS[(1,1)] == bS[(2,2)] && bS[(2,2)] == bS[(3,3)]) 
            return bS[(1,1)].ToString();

        if (bS[(1,3)] == bS[(2,2)] && bS[(2,2)] == bS[(3,1)]) 
            return bS[(1,3)].ToString();
        */
        if (square[0] != ' ' && 
            square[1] != ' ' && 
            square[2] != ' ' &&
            square[3] != ' ' && 
            square[4] != ' ' && 
            square[5] != ' ' && 
            square[6] != ' ' && 
            square[7] != ' ' && 
            square[8] != ' '    ) return "Draw";
        return  "Playing";
    }
    
    char playerSign;
    string casoVencedor;
    public Game1()
    {
        gameState = GameStateDetails.Begin;
        
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth  = (int)scrSize.X; 
        _graphics.PreferredBackBufferHeight = (int)scrSize.Y; 
        _graphics.ApplyChanges();

        grid = new();
        int locationX = 0;
        int locationY = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)        
            {
                grid.Add(
                    new Rectangle(locationX, 
                                  locationY,
                                  99,
                                  99)
                );
                locationX+=100;
            }
            locationX = 0;
            locationY+= 100;
        } 
        bS = new();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        board       = Content.Load<Texture2D>("board");
        dot         = Content.Load<Texture2D>("blackDot");
        cross       = Content.Load<Texture2D>("cross");
        circle      = Content.Load<Texture2D>("circle");
        gameFont    = Content.Load<SpriteFont>("arialFont");
    }

    protected override void Update(GameTime gameTime)
    {

        msNew = Mouse.GetState();
        if (msOld.LeftButton != msNew.LeftButton)
        {
            if (msOld.LeftButton == ButtonState.Released && msNew.LeftButton == ButtonState.Pressed)
            {

                if (gameState == GameStateDetails.Begin | gameState == GameStateDetails.Playing)
                {
                    for (int i = 0; i < grid.Count; i++)
                    {
                        if (grid[i].Contains(msNew.Position) && square[i] == ' ')
                        {
                            playerSign = playerSign == 'X' ? 'O' : 'X';
                            square[i] = playerSign;
                            break;
                        }
                    }
                    gameState = GameStateDetails.Playing;
                }
                else if (gameState == GameStateDetails.GameEnd)
                {
                    if(replayButton.Contains(msNew.Position))
                    {
                        resetBoard();
                    }
                    if(exitButton.Contains(msNew.Position))
                    {
                        Exit();
                    }
                }
            }
        }

        replayButtonColor = replayButton.Contains(msNew.Position) ? highLightedButtonColor : nonHighLightedButtonColor;
        exitButtonColor   = exitButton.Contains(msNew.Position)   ? highLightedButtonColor : nonHighLightedButtonColor;   

        msOld = Mouse.GetState();
        
        switch (checkForWinner())
        {
            case "Draw":
                gameState = GameStateDetails.Draw;
                break;

            case "X":
                gameState = GameStateDetails.PlayerXWon;
                break;

            case "O":
                gameState = GameStateDetails.PlayerOWon;
                break;
        }
    
        switch (gameState)
        {
            case GameStateDetails.PlayerOWon:
                casoVencedor = "O jogador O venceu!";
                gameState = GameStateDetails.GameEnd;
                break;

            case GameStateDetails.PlayerXWon:
                casoVencedor = "O jogador X venceu!";
                gameState = GameStateDetails.GameEnd;
                break;

            case GameStateDetails.Draw:
                casoVencedor = "Deu velha";
                gameState = GameStateDetails.GameEnd;
                break;

            case GameStateDetails.Begin:
                casoVencedor = "";    
                break;
        }
        base.Update(gameTime);
    }
    void resetBoard()
    {
        for(int i = 0; i < square.Length; i++)
        {
            square[i] = ' ';
        }
        gameState = GameStateDetails.Begin;
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        
        for (int i = 0; i < grid.Count; i++)
        {
            spriteBatch.Draw(
                texture:                square[i] == 'X' ? cross: square[i] == 'O' ? circle: dot,
                destinationRectangle:   grid[i],
                color:                  Color.White
            );
        }
        spriteBatch.Draw(
            board,
            new Vector2(), 
            null,
            Color.White,
            0f,
            new Vector2(),
            Vector2.One * 0.83f,
            SpriteEffects.None,
            0f
        );
        if(gameState == GameStateDetails.GameEnd)
        {
            spriteBatch.Draw(
                texture: dot,
                destinationRectangle: exitButton,
                color: Color.FromNonPremultiplied(exitButtonColor)
            );

            spriteBatch.Draw(
                texture: dot,
                destinationRectangle: replayButton,
                color: Color.FromNonPremultiplied(replayButtonColor)
            );
        
            spriteBatch.DrawString(
                gameFont,
                "Rejogar",
                replayButton.Center.ToVector2() - (gameFont.MeasureString("Rejogar") / 2),
                Color.Black
            );

            spriteBatch.DrawString(
                gameFont,
                "Sair",
                exitButton.Center.ToVector2() - (gameFont.MeasureString("Sair") / 2),
                Color.Black
            );
        }
        spriteBatch.DrawString(
            gameFont,
            casoVencedor,
            new Vector2(scrSize.X - 200, 0),
            Color.Black
        );
        spriteBatch.End();
        base.Draw(gameTime);
    }
}