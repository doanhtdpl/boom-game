using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SCSEngine.Audio;
using SCSEngine.ResourceManagement;
using SCSEngine.Services;
using SCSEngine.Services.Audio;
using SCSEngine.Sprite;
using SCSEngine.Utils.Adapters;

namespace SCSEngine.Control
{
    /*
    public class Button : DrawableGameComponent
    {
        #region Fields
        public enum Button_State
        {
            NORMAL,
            HOLD,
            TOUCHED
        }

        private ISprite normal_img;
        private ISprite hold_img;
        private SCSServices services;

        public Sound PressedSound { get; set; }

        private Button_State state;

        public event EventHandler OnPressed;
        public event EventHandler OnReleased;

        public string ButtonName { get; set; }
        #endregion

        #region Properties

        private Rectangle bound;

        public Rectangle Bound
        {
            get { return bound; }
            set { bound = value; }
        }

        #endregion

        public Button(Game game, String buttonName, ISprite normal, ISprite hold)
            : base(gamePage)
        {
            this.normal_img = normal;
            this.hold_img = hold;
            this.ButtonName = buttonName;

            this.services = (game.Services.GetService(typeof(SCSServices)) as SCSServices);

            this.state = Button_State.NORMAL;
        }

        public Button(Game game, String buttonName, ISprite normal, ISprite hold, Sound touchSound)
            : base(gamePage)
        {
            this.normal_img = normal;
            this.hold_img = hold;
            this.ButtonName = buttonName;
            this.PressedSound = touchSound;

            this.state = Button_State.NORMAL;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (state == Button_State.HOLD)
            {
                this.services.SpritePlayer.Draw(hold_img, this.bound, Color.White);
            }
            else
            {
                this.services.SpritePlayer.Draw(normal_img, this.bound, Color.White);
            }

            base.Draw(gameTime);
        }

        public void Tap(Vector2 position, TouchLocationState state)
        {
            if (state == TouchLocationState.Moved || state == TouchLocationState.Pressed)
            {
                if (state == TouchLocationState.Pressed)
                {
                    this.buttonOnPressed();
                }

                this.state = Button_State.HOLD;
            }
            else
            {
                this.state = Button_State.NORMAL;
                this.buttonOnReleased();
            }
        }

        public void Drag(Vector2 begin, Vector2 current, TouchLocationState state)
        {
            if (state == TouchLocationState.Moved)
            {
                if (this.Contains((int) current.X, (int) current.Y))
                {
                    this.state = Button_State.HOLD;
                }
                else
                {
                    this.state = Button_State.NORMAL;
                }
            }
            else if (state == TouchLocationState.Released)
            {
                if (this.Contains((int)current.X, (int)current.Y))
                {
                    this.buttonOnReleased();
                }

                this.state = Button_State.NORMAL;
            }
        }

        private void buttonOnPressed()
        {
            try
            {
                if (this.PressedSound != null)
                {
                    services.AudioManager.PlaySound(this.PressedSound, true, false);
                }
            }
            catch (System.Exception)
            {

            }

            if (this.OnPressed != null)
            {
                this.OnPressed(this, null);
            }
        }

        private void buttonOnReleased()
        {
            if (this.OnReleased != null)
            {
                this.OnReleased(this, null);
            }
        }

        public bool IsTouchCompleted()
        {
            return false;
        }

        public bool Contains(int x, int y)
        {
            return this.bound.Contains(x, y);
        }

        public bool CanMultitouch { get { return false; } }
        public bool CanSweep { get { return false; } }
        public bool CanDrag { get { return false; } }
    }
  */

    public delegate void OnButtonTouchedHandler(Button sender, EventArgs args);

    public abstract class Button : DrawableGameComponentAsSpriteModel
    {
        public ISprite Background { get; set; }

        private Vector2 size;
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public event OnButtonTouchedHandler OnTouched;

        private SCSServices services;

        public Button(Game game, ISprite background)
            : base(game)
        {
            this.services = game.Services.GetService(typeof(SCSServices)) as SCSServices;
            this.Background = background;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GestureSample gesture in this.services.InputHandle.Gestures)
            {
                if (gesture.GestureType == GestureType.Tap && this.Contains(gesture.Position))
                {
                    this.controlOnTouched();
                }
            }

            base.Update(gameTime);
        }

        protected virtual void controlOnTouched()
        {
            if (this.OnTouched != null)
            {
                this.OnTouched(this, null);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                this.Background.TimeStep(gameTime);
                this.services.SpritePlayer.Draw(this.Background, this);
            }

            base.Draw(gameTime);
        }

        protected abstract bool Contains(Vector2 pos);
    }
}
