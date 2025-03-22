using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraU3DPlus
{
    public class Game : GameWindow
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private const float rotationSpeed = 1.5f;
        
        // Color del fondo y de las líneas
        private Color4 backgroundColor = new Color4(0.05f, 0.1f, 0.2f, 1.0f); // Azul oscuro
        private Color4 lineColor = new Color4(1.0f, 1.0f, 1.0f, 1.0f); // Blanco
        
        // Parámetros de la U
        private float thickness = 0.2f;
        private float width = 1.0f;
        private float height = 1.5f;
        private float depth = 0.4f;
        
        // Mostrar ejes
        private bool showAxes = true;
        
        // Ancho de línea
        private float lineWidth = 1.5f;

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(backgroundColor);
            GL.Enable(EnableCap.DepthTest);
            
            // Configurar suavizado de líneas
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // Configurar matriz de proyección
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                Width / (float)Height,
                0.1f,
                100.0f);
                
            // Configurar cámara
            Matrix4 lookat = Matrix4.LookAt(
                new Vector3(0.0f, 0.0f, 3.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f));
                
            // Aplicar rotaciones
            Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationX)) *
                               Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY));
                
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            
            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = rotation * lookat;
            GL.LoadMatrix(ref modelview);
            
            // Dibujar ejes si están habilitados
            if (showAxes)
                DrawAxes();
            
            // Establecer ancho de línea para la U
            GL.LineWidth(lineWidth);
            
            // Dibujar la letra U como wireframe
            DrawUWireframe();
            
            // Mostrar información en el título de la ventana
            Title = $"Letra U Wireframe - Rotación: X={rotationX:F1}° Y={rotationY:F1}° | " +
                    $"Ancho de línea: {lineWidth:F1} | " +
                    $"Ejes: {(showAxes ? "Visibles" : "Ocultos")}";
            
            SwapBuffers();
        }

        private void DrawAxes()
        {
            float axisLength = 1.5f;
            
            GL.LineWidth(1.0f);
            GL.Begin(PrimitiveType.Lines);
            
            // Eje X - Rojo
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(axisLength, 0, 0);
            
            // Eje Y - Verde
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, axisLength, 0);
            
            // Eje Z - Azul
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, axisLength);
            
            GL.End();
        }

        private void DrawUWireframe()
        {
            // Coordenadas de los vértices para la U
            float left = -width / 2;
            float right = width / 2;
            float bottom = -height / 2;
            float top = height / 2;
            float front = depth / 2;
            float back = -depth / 2;
            float leftInner = left + thickness;
            float rightInner = right - thickness;
            float bottomInner = bottom + thickness;
            
            // Establecer color blanco para las líneas
            GL.Color4(lineColor);
            
            // Dibujar la U solo con líneas
            GL.Begin(PrimitiveType.Lines);
            
            // Parte izquierda - aristas verticales frontales
            GL.Vertex3(left, bottom, front);
            GL.Vertex3(left, top, front);
            
            GL.Vertex3(leftInner, bottom, front);
            GL.Vertex3(leftInner, top, front);
            
            // Parte izquierda - aristas verticales traseras
            GL.Vertex3(left, bottom, back);
            GL.Vertex3(left, top, back);
            
            GL.Vertex3(leftInner, bottom, back);
            GL.Vertex3(leftInner, top, back);
            
            // Parte izquierda - aristas horizontales superiores
            GL.Vertex3(left, top, front);
            GL.Vertex3(leftInner, top, front);
            
            GL.Vertex3(left, top, back);
            GL.Vertex3(leftInner, top, back);
            
            // Parte izquierda - conectores frente-atrás superiores
            GL.Vertex3(left, top, front);
            GL.Vertex3(left, top, back);
            
            GL.Vertex3(leftInner, top, front);
            GL.Vertex3(leftInner, top, back);
            
            // Parte izquierda - conectores frente-atrás inferiores
            GL.Vertex3(left, bottom, front);
            GL.Vertex3(left, bottom, back);
            
            GL.Vertex3(leftInner, bottom, front);
            GL.Vertex3(leftInner, bottom, back);
            
            // Parte inferior - aristas horizontales frontales
            GL.Vertex3(left, bottom, front);
            GL.Vertex3(right, bottom, front);
            
            GL.Vertex3(left, bottomInner, front);
            GL.Vertex3(right, bottomInner, front);
            
            // Parte inferior - aristas horizontales traseras
            GL.Vertex3(left, bottom, back);
            GL.Vertex3(right, bottom, back);
            
            GL.Vertex3(left, bottomInner, back);
            GL.Vertex3(right, bottomInner, back);
            
            // Parte inferior - conectores izquierda-derecha frontales
            GL.Vertex3(left, bottom, front);
            GL.Vertex3(left, bottomInner, front);
            
            GL.Vertex3(right, bottom, front);
            GL.Vertex3(right, bottomInner, front);
            
            // Parte inferior - conectores izquierda-derecha traseros
            GL.Vertex3(left, bottom, back);
            GL.Vertex3(left, bottomInner, back);
            
            GL.Vertex3(right, bottom, back);
            GL.Vertex3(right, bottomInner, back);
            
            // Parte inferior - conectores frente-atrás
            GL.Vertex3(right, bottom, front);
            GL.Vertex3(right, bottom, back);
            
            GL.Vertex3(right, bottomInner, front);
            GL.Vertex3(right, bottomInner, back);
            
            // Parte derecha - aristas verticales frontales
            GL.Vertex3(right, bottom, front);
            GL.Vertex3(right, top, front);
            
            GL.Vertex3(rightInner, bottom, front);
            GL.Vertex3(rightInner, top, front);
            
            // Parte derecha - aristas verticales traseras
            GL.Vertex3(right, bottom, back);
            GL.Vertex3(right, top, back);
            
            GL.Vertex3(rightInner, bottom, back);
            GL.Vertex3(rightInner, top, back);
            
            // Parte derecha - aristas horizontales superiores
            GL.Vertex3(right, top, front);
            GL.Vertex3(rightInner, top, front);
            
            GL.Vertex3(right, top, back);
            GL.Vertex3(rightInner, top, back);
            
            // Parte derecha - conectores frente-atrás superiores
            GL.Vertex3(right, top, front);
            GL.Vertex3(right, top, back);
            
            GL.Vertex3(rightInner, top, front);
            GL.Vertex3(rightInner, top, back);
            
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
            
            // Salir
            if (keyState.IsKeyDown(Key.Escape))
                Exit();
            
            // Rotación
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
            
            // Mostrar/ocultar ejes
            if (keyState.IsKeyDown(Key.A) && !previousKeyState.IsKeyDown(Key.A))
                showAxes = !showAxes;
                
            // Aumentar/disminuir el ancho de línea
            if (keyState.IsKeyDown(Key.Plus) || keyState.IsKeyDown(Key.KeypadPlus))
                lineWidth = Math.Min(lineWidth + 0.1f, 5.0f);
                
            if (keyState.IsKeyDown(Key.Minus) || keyState.IsKeyDown(Key.KeypadMinus))
                lineWidth = Math.Max(lineWidth - 0.1f, 0.5f);
                
            // Guardar estado de teclas para el siguiente frame
            previousKeyState = keyState;
        }
        
        private KeyboardState previousKeyState;
    }

    class Program
    {
        //static void Main(string[] args)
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Letra U en 3D c/Rotacion, Eje, solo Aristas"))
            {
                game.Run(60.0);
            }
        }
    }
}
