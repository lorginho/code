sing System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraUProgram_01
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f); // Fondo gris oscuro
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0); // Proyección ortográfica
            
            DibujarEjes(); // Dibujar los ejes X e Y
            DibujarLetraU(); // Dibujar la letra "U"
            
            SwapBuffers();
        }

        private void DibujarEjes()
        {
            GL.Begin(PrimitiveType.Lines);
            
            // Eje X (en rojo)
            GL.Color3(1.0f, 0.0f, 0.0f); // Rojo
            GL.Vertex2(-1.0f, 0.0f); // Izquierda
            GL.Vertex2(1.0f, 0.0f);  // Derecha
            
            // Eje Y (en verde)
            GL.Color3(0.0f, 1.0f, 0.0f); // Verde
            GL.Vertex2(0.0f, -1.0f); // Abajo
            GL.Vertex2(0.0f, 1.0f);  // Arriba
            
            GL.End();
        }

        private void DibujarLetraU()
        {
            GL.Begin(PrimitiveType.Quads);
            
            GL.Color3(1.0f, 1.0f, 1.0f); // Color blanco
            
            // Parte izquierda de la U
            GL.Vertex2(-0.5f, 0.8f);
            GL.Vertex2(-0.3f, 0.8f);
            GL.Vertex2(-0.3f, -0.4f);
            GL.Vertex2(-0.5f, -0.4f);
            
            // Parte inferior de la U
            GL.Vertex2(-0.5f, -0.4f);
            GL.Vertex2(0.5f, -0.4f);
            GL.Vertex2(0.5f, -0.6f);
            GL.Vertex2(-0.5f, -0.6f);
            
            // Parte derecha de la U
            GL.Vertex2(0.3f, 0.8f);
            GL.Vertex2(0.5f, 0.8f);
            GL.Vertex2(0.5f, -0.4f);
            GL.Vertex2(0.3f, -0.4f);
            
            GL.End();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height); // Ajustar el viewport al nuevo tamaño
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Key.Escape))
            {
                Exit(); // Salir del programa si se presiona Escape
            }
        }
    }

    class Program
    {
        static void Main_01(string[] args)
        {
            using (Game game = new Game(400, 300, "Letra U en 2D con Ejes"))
            {
                game.Run(60.0); // Iniciar el bucle principal a 60 FPS
            }
        }
    }
}