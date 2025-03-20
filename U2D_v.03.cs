using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraUProgram
{
    public class Game : GameWindow
    {
        // Variables para la posición, tamaño y rotación de la "U"
        private float posX = -50, posY = 50; // Posición inicial de la "U"
        private float ancho = 40, altura = 60, grosor = 10; // Tamaño de la "U"
        private float escala = 1.0f; // Escala de la "U"
        private float rotacion = 0.0f; // Rotación de la "U" en grados

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
            
            // Dibujar los ejes (sin transformaciones)
            DibujarEjes();
            
            // Aplicar transformaciones solo a la "U"
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.PushMatrix(); // Guardar el estado actual de la matriz
            GL.Translate(posX, posY, 0); // Mover la "U" a la posición (posX, posY)
            GL.Scale(escala, escala, 1); // Escalar la "U"
            GL.Rotate(rotacion, 0, 0, 1); // Rotar la "U"
            
            DibujarLetraU();
            
            GL.PopMatrix(); // Restaurar el estado de la matriz (ignorar transformaciones para los ejes)
            
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

        private void DibujarLetraU()
        {
            GL.Begin(PrimitiveType.Quads);
            
            GL.Color3(1.0f, 1.0f, 1.0f); // Color blanco
            
            // Calcular las coordenadas en relación con el centro (0, 0)
            float izquierda = -ancho / 2;
            float derecha = ancho / 2;
            float arriba = altura / 2;
            float abajo = -altura / 2;
            
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
            
            // Mover la "U" con las teclas de dirección
            float velocidad = 2.0f; // Velocidad de movimiento
            if (keyState.IsKeyDown(Key.Left))
                posX -= velocidad;
            if (keyState.IsKeyDown(Key.Right))
                posX += velocidad;
            if (keyState.IsKeyDown(Key.Up))
                posY += velocidad;
            if (keyState.IsKeyDown(Key.Down))
                posY -= velocidad;
            
            // Escalar la "U" con las teclas + y -
            if (keyState.IsKeyDown(Key.Plus) || keyState.IsKeyDown(Key.KeypadPlus))
                escala += 0.1f;
            if (keyState.IsKeyDown(Key.Minus) || keyState.IsKeyDown(Key.KeypadMinus))
                escala -= 0.1f;
            
            // Rotar la "U" con las teclas R y T
            if (keyState.IsKeyDown(Key.R))
                rotacion += 2.0f;
            if (keyState.IsKeyDown(Key.T))
                rotacion -= 2.0f;
            
            // Salir del programa si se presiona Escape
            if (keyState.IsKeyDown(Key.Escape))
                Exit();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 800, "Letra U en 2D con Ejes Fijos"))
            {
                game.Run(60.0); // Iniciar el bucle principal a 60 FPS
            }
        }
    }
}