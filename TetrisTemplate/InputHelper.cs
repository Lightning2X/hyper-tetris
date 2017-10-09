using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class InputHelper
{
    protected Vector2 scale, offset;

    // current and previous mouse/keyboard states

    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;


    public InputHelper()
    {
        scale = Vector2.One;
        offset = Vector2.Zero;
    }
    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }
    // time passed since the last key press

    double timeSinceLastKeyPress;
     
     // time interval to read separate keypresses when holding a key
      
    double keyPressInterval;

     
     // constructor method
      
   /* public InputHelper()
    {
        keyPressInterval = 100;
        timeSinceLastKeyPress = 0;
    }*/
     
     // updates the input helper object by updating the mouse and keyboard states and updating the timeSinceLastKeyPress variable
      
    public void Update(GameTime gameTime)
    {
        // check if keys are pressed and update the timeSinceLastKeyPress variable
        Keys[] prevKeysDown = previousKeyboardState.GetPressedKeys();
        Keys[] currKeysDown = currentKeyboardState.GetPressedKeys();
        if (currKeysDown.Length != 0 && (prevKeysDown.Length == 0 || timeSinceLastKeyPress > keyPressInterval))
            timeSinceLastKeyPress = 0;
        else
            timeSinceLastKeyPress += gameTime.ElapsedGameTime.TotalMilliseconds;

        // update the mouse and keyboard states
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }
     
     // returns the current mouse position
      
    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }
     
     // indicates whether the left mouse button is pressed
      
    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }
 
     // indicates whether the player has pressed the key in the current update, a key press is detected
     // if either the key wasn't pressed in the previous state, or enough time has passed since the last time the key press
     // was detected
      
    public bool KeyPressedS(Keys s, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(s) && (previousKeyboardState.IsKeyUp(s) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    public bool KeyPressedA(Keys a, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(a) && (previousKeyboardState.IsKeyUp(a) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    public bool KeyPressedW(Keys w, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(w) && (previousKeyboardState.IsKeyUp(w) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    public bool KeyPressedD(Keys d, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(d) && (previousKeyboardState.IsKeyUp(d) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    public bool KeyPressedLeft(Keys left, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(left) && (previousKeyboardState.IsKeyUp(left) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    public bool KeyPressedRight(Keys right, bool detecthold = true)
    {
        return currentKeyboardState.IsKeyDown(right) && (previousKeyboardState.IsKeyUp(right) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
    }
    // indicates whether key is currently down

    public bool IsKeyDownA(Keys a)
    {
        return currentKeyboardState.IsKeyDown(a);
    }
    public bool IsKeyDownW(Keys w)
    {
        return currentKeyboardState.IsKeyDown(w);
    }
    public bool IsKeyDownS(Keys s)
    {
        return currentKeyboardState.IsKeyDown(s);
    }
    public bool IsKeyDownD(Keys d)
    {
        return currentKeyboardState.IsKeyDown(d);
    }
}