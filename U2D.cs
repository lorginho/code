using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraUProgram
{
    public class Game : GameWindow
    {
        
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            
            // Dibujar la letra U con bordes rectos (sin curvas)
            GL.Begin(PrimitiveType.Quads);
            
            // Color blanco
            GL.Color3(1.0f, 1.0f, 1.0f);
            
            // Parte izquierda de la U
            GL.Vertex2(-0.5f, 0.8f);  // Arriba izquierda
            GL.Vertex2(-0.3f, 0.8f);  // Arriba derecha
            GL.Vertex2(-0.3f, -0.4f); // Abajo derecha
            GL.Vertex2(-0.5f, -0.4f); // Abajo izquierda
            
            // Parte inferior de la U
            GL.Vertex2(-0.5f, -0.4f); // Izquierda arriba
            GL.Vertex2(0.5f, -0.4f);  // Derecha arriba
            GL.Vertex2(0.5f, -0.6f);  // Derecha abajo
            GL.Vertex2(-0.5f, -0.6f); // Izquierda abajo
            
            // Parte derecha de la U
            GL.Vertex2(0.3f, 0.8f);   // Arriba izquierda
            GL.Vertex2(0.5f, 0.8f);   // Arriba derecha
            GL.Vertex2(0.5f, -0.4f);  // Abajo derecha
            GL.Vertex2(0.3f, -0.4f);  // Abajo izquierda
            
            GL.End();
            
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }
    }

    class Program
    {
         //static void Main(string[] args)
         static void Main0(string[] args)
        {
            
            using (Game game = new Game(400, 300, "Letra U en 2D, Blanco s/ Gris"))
            {
                game.Run(60.0);
            }
        }
    }
}
