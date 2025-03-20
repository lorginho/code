using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraUProgram_02
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
            
            // Limpiar el búfer de color y profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // Configurar la proyección ortográfica
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-100, 100, -100, 100, -1, 1); // Plano de -100 a 100 en X e Y
            
            // Dibujar los ejes primero
            DibujarEjes();
            
            // Dibujar la letra "U" en una posición relativa al centro
            DibujarLetraU(-50, 50); // Cambia estas coordenadas para mover la "U"
            
            // Intercambiar los búferes para mostrar el resultado
            SwapBuffers();
        }

        private void DibujarEjes()
        {
            GL.Begin(PrimitiveType.Lines);
            
            // Eje X (en rojo)
            GL.Color3(1.0f, 0.0f, 0.0f); // Rojo
            GL.Vertex2(-100, 0); // Izquierda
            GL.Vertex2(100, 0);  // Derecha
            
            // Eje Y (en verde)
            GL.Color3(0.0f, 1.0f, 0.0f); // Verde
            GL.Vertex2(0, -100); // Abajo
            GL.Vertex2(0, 100);  // Arriba
            
            GL.End();
        }

        private void DibujarLetraU(float posX, float posY)
        {
            GL.Begin(PrimitiveType.Quads);
            
            GL.Color3(1.0f, 1.0f, 1.0f); // Color blanco
            
            // Definir las dimensiones de la "U"
            float ancho = 40; // Ancho total de la "U"
            float altura = 60; // Altura total de la "U"
            float grosor = 10; // Grosor de las líneas de la "U"
            
            // Calcular las coordenadas en relación con la posición (posX, posY)
            float izquierda = posX - ancho / 2; // Centrar horizontalmente
            float derecha = posX + ancho / 2;
            float arriba = posY + altura / 2; // Centrar verticalmente
            float abajo = posY - altura / 2;
            
            // Parte izquierda de la U
            GL.Vertex2(izquierda, arriba); // Arriba izquierda
            GL.Vertex2(izquierda + grosor, arriba); // Arriba derecha
            GL.Vertex2(izquierda + grosor, abajo + grosor); // Abajo derecha
            GL.Vertex2(izquierda, abajo + grosor); // Abajo izquierda
            
            // Parte inferior de la U
            GL.Vertex2(izquierda, abajo + grosor); // Izquierda arriba
            GL.Vertex2(derecha, abajo + grosor); // Derecha arriba
            GL.Vertex2(derecha, abajo); // Derecha abajo
            GL.Vertex2(izquierda, abajo); // Izquierda abajo
            
            // Parte derecha de la U
            GL.Vertex2(derecha - grosor, arriba); // Arriba izquierda
            GL.Vertex2(derecha, arriba); // Arriba derecha
            GL.Vertex2(derecha, abajo + grosor); // Abajo derecha
            GL.Vertex2(derecha - grosor, abajo + grosor); // Abajo izquierda
            
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
        static void Main2(string[] args)
        {
            using (Game game = new Game(800, 800, "Letra U en 2D con Ejes y Posición Relativa"))
            {
                game.Run(60.0); // Iniciar el bucle principal a 60 FPS
            }
        }
    }
}