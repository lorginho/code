using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraU3D
{
    public class Game : GameWindow
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private const float rotationSpeed = 1.0f;

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                Width / (float)Height,
                0.1f,
                100.0f);
                
            Matrix4 lookat = Matrix4.LookAt(
                new Vector3(0.0f, 0.0f, 3.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f));
                
            Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationX)) *
                               Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY));
                
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            
            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = rotation * lookat;
            GL.LoadMatrix(ref modelview);
            
            // Dibujar la letra U en 3D
            DrawU3D();
            
            SwapBuffers();
        }

        private void DrawU3D()
        {
            float thickness = 0.2f; // Grosor de la U
            float width = 1.0f;     // Anchura de la U
            float height = 1.5f;    // Altura de la U
            float depth = 0.4f;     // Profundidad de la U
            
            // Dibujar los 6 lados de cada parte de la U
            // Usando diferentes tonos para cada cara para dar sensación de profundidad
            
            // Parte izquierda de la U
            DrawCuboid(
                -width/2, -height/2, -depth/2,  // Esquina inferior izquierda trasera
                -width/2 + thickness, height/2, depth/2  // Esquina superior derecha frontal
            );
            
            // Parte inferior de la U
            DrawCuboid(
                -width/2, -height/2, -depth/2,  // Esquina inferior izquierda trasera
                width/2, -height/2 + thickness, depth/2  // Esquina superior derecha frontal
            );
            
            // Parte derecha de la U
            DrawCuboid(
                width/2 - thickness, -height/2, -depth/2,  // Esquina inferior izquierda trasera
                width/2, height/2, depth/2  // Esquina superior derecha frontal
            );
        }

        private void DrawCuboid(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            // Definir los 8 vértices del cuboid
            float[] vertices = {
                x1, y1, z1, // 0: Esquina inferior izquierda trasera
                x2, y1, z1, // 1: Esquina inferior derecha trasera
                x2, y2, z1, // 2: Esquina superior derecha trasera
                x1, y2, z1, // 3: Esquina superior izquierda trasera
                x1, y1, z2, // 4: Esquina inferior izquierda frontal
                x2, y1, z2, // 5: Esquina inferior derecha frontal
                x2, y2, z2, // 6: Esquina superior derecha frontal
                x1, y2, z2  // 7: Esquina superior izquierda frontal
            };

            // Cara frontal (z = z2)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 1.0f, 1.0f); // Blanco
            GL.Vertex3(vertices[12], vertices[13], vertices[14]); // 4
            GL.Vertex3(vertices[15], vertices[16], vertices[17]); // 5
            GL.Vertex3(vertices[18], vertices[19], vertices[20]); // 6
            GL.Vertex3(vertices[21], vertices[22], vertices[23]); // 7
            GL.End();

            // Cara trasera (z = z1)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.8f, 0.8f, 0.8f); // Gris claro
            GL.Vertex3(vertices[0], vertices[1], vertices[2]);   // 0
            GL.Vertex3(vertices[3], vertices[4], vertices[5]);   // 1
            GL.Vertex3(vertices[6], vertices[7], vertices[8]);   // 2
            GL.Vertex3(vertices[9], vertices[10], vertices[11]); // 3
            GL.End();

            // Cara superior (y = y2)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.9f, 0.9f, 0.9f); // Casi blanco
            GL.Vertex3(vertices[9], vertices[10], vertices[11]); // 3
            GL.Vertex3(vertices[6], vertices[7], vertices[8]);   // 2
            GL.Vertex3(vertices[18], vertices[19], vertices[20]); // 6
            GL.Vertex3(vertices[21], vertices[22], vertices[23]); // 7
            GL.End();

            // Cara inferior (y = y1)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.7f, 0.7f, 0.7f); // Gris medio
            GL.Vertex3(vertices[0], vertices[1], vertices[2]);   // 0
            GL.Vertex3(vertices[3], vertices[4], vertices[5]);   // 1
            GL.Vertex3(vertices[15], vertices[16], vertices[17]); // 5
            GL.Vertex3(vertices[12], vertices[13], vertices[14]); // 4
            GL.End();

            // Cara derecha (x = x2)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.85f, 0.85f, 0.85f); // Gris claro medio
            GL.Vertex3(vertices[3], vertices[4], vertices[5]);   // 1
            GL.Vertex3(vertices[6], vertices[7], vertices[8]);   // 2
            GL.Vertex3(vertices[18], vertices[19], vertices[20]); // 6
            GL.Vertex3(vertices[15], vertices[16], vertices[17]); // 5
            GL.End();

            // Cara izquierda (x = x1)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.75f, 0.75f, 0.75f); // Gris medio claro
            GL.Vertex3(vertices[0], vertices[1], vertices[2]);   // 0
            GL.Vertex3(vertices[9], vertices[10], vertices[11]); // 3
            GL.Vertex3(vertices[21], vertices[22], vertices[23]); // 7
            GL.Vertex3(vertices[12], vertices[13], vertices[14]); // 4
            GL.End();
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
            
            // Rotación con las teclas de flecha
            if (keyState.IsKeyDown(Key.Up))
                rotationX += rotationSpeed;
                
            if (keyState.IsKeyDown(Key.Down))
                rotationX -= rotationSpeed;
                
            if (keyState.IsKeyDown(Key.Left))
                rotationY -= rotationSpeed;
                
            if (keyState.IsKeyDown(Key.Right))
                rotationY += rotationSpeed;
                
            // Restablecer rotación
            if (keyState.IsKeyDown(Key.R))
            {
                rotationX = 0.0f;
                rotationY = 0.0f;
            }
        }
    }

    class Program
    {
       // static void Main(string[] args)
        static void Main1(string[] args)
        {
            using (Game game = new Game(800, 600, "Letra U en 3D c/Rotacion, Blanco s/ Gris"))
            {
                game.Run(60.0);
            }
        }
    }
}