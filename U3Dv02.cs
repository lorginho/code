using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraU3Dv02
{
    public class Game : GameWindow
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private const float rotationSpeed = 1.5f;
        
        // Color del fondo y de la U
        private Color4 backgroundColor = new Color4(0.05f, 0.1f, 0.2f, 1.0f); // Azul oscuro
        private Color4 letterColor = new Color4(0.9f, 0.5f, 0.1f, 1.0f); // Naranja
        
        // Parámetros de la U
        private float thickness = 0.2f;
        private float width = 1.0f;
        private float height = 1.5f;
        private float depth = 0.4f;
        
        // Modo de visualización
        private bool wireframeMode = false;
        private bool showAxes = true;
        
        // Iluminación
        private bool lightingEnabled = true;

        // Vector de traslación para mover la U
        private Vector3 translation = new Vector3(0.0f, 0.0f, 0.0f);

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(backgroundColor);
            GL.Enable(EnableCap.DepthTest);
            SetupLighting();
        }
        
        private void SetupLighting()
        {
            // Configuración básica de iluminación
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            
            // Posición de la luz
            float[] lightPosition = { 2.0f, 2.0f, 2.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            
            // Color de la luz
            float[] lightAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] lightDiffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] lightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
            
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lightSpecular);
            
            // Material de la U
            float[] materialAmbient = { letterColor.R, letterColor.G, letterColor.B, 1.0f };
            float[] materialDiffuse = { letterColor.R, letterColor.G, letterColor.B, 1.0f };
            float[] materialSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float materialShininess = 50.0f;
            
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, materialAmbient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, materialDiffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);
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
            
            // Control de iluminación
            if (lightingEnabled)
                GL.Enable(EnableCap.Lighting);
            else
                GL.Disable(EnableCap.Lighting);
            
            // Control de modo de visualización
            if (wireframeMode)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            
            // Dibujar ejes si están habilitados 
            if (showAxes)
                DrawAxes();
            
            // Dibujar la letra U en 3D
            DrawU3D();
            
            // Mostrar información en el título de la ventana
            Title = $"Letra U 3D - Posición: X={translation.X:F1} Y={translation.Y:F1} Z={translation.Z:F1} | " +
                    $"Rotación: X={rotationX:F1}° Y={rotationY:F1}° | " +
                    $"Iluminación: {(lightingEnabled ? "ON" : "OFF")} | " +
                    $"Modo: {(wireframeMode ? "Wireframe" : "Sólido")} | " +
                    $"Ejes: {(showAxes ? "Visibles" : "Ocultos")}";
            
            SwapBuffers();
        }

        private void DrawAxes()
        {
            float axisLength = 1.5f;
            
            GL.Disable(EnableCap.Lighting);
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
            
            if (lightingEnabled)
                GL.Enable(EnableCap.Lighting);
        }

        private void DrawU3D()
        {
            // Aplicar la traslación a las coordenadas de la U
            Vector3 offset = translation;

            // Parte izquierda de la U
            DrawCuboid(
                -width/2 + offset.X, -height/2 + offset.Y, -depth/2 + offset.Z,  // Esquina inferior izquierda trasera
                -width/2 + thickness + offset.X, height/2 + offset.Y, depth/2 + offset.Z  // Esquina superior derecha frontal
            );
            
            // Parte inferior de la U
            DrawCuboid(
                -width/2 + offset.X, -height/2 + offset.Y, -depth/2 + offset.Z,  // Esquina inferior izquierda trasera
                width/2 + offset.X, -height/2 + thickness + offset.Y, depth/2 + offset.Z  // Esquina superior derecha frontal
            );
            
            // Parte derecha de la U
            DrawCuboid(
                width/2 - thickness + offset.X, -height/2 + offset.Y, -depth/2 + offset.Z,  // Esquina inferior izquierda trasera
                width/2 + offset.X, height/2 + offset.Y, depth/2 + offset.Z  // Esquina superior derecha frontal
            );
        }

        private void DrawCuboid(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            // Si la iluminación está desactivada, usar el color definido
            if (!lightingEnabled)
                GL.Color4(letterColor);
                
            // Dibujar los 6 lados del cuboide
            GL.Begin(PrimitiveType.Quads);
            
            // Cara frontal (z = z2)
            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(x1, y1, z2);
            GL.Vertex3(x2, y1, z2);
            GL.Vertex3(x2, y2, z2);
            GL.Vertex3(x1, y2, z2);
            
            // Cara trasera (z = z1)
            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x1, y2, z1);
            GL.Vertex3(x2, y2, z1);
            GL.Vertex3(x2, y1, z1);
            
            // Cara superior (y = y2)
            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(x1, y2, z1);
            GL.Vertex3(x1, y2, z2);
            GL.Vertex3(x2, y2, z2);
            GL.Vertex3(x2, y2, z1);
            
            // Cara inferior (y = y1)
            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x2, y1, z1);
            GL.Vertex3(x2, y1, z2);
            GL.Vertex3(x1, y1, z2);
            
            // Cara derecha (x = x2)
            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(x2, y1, z1);
            GL.Vertex3(x2, y2, z1);
            GL.Vertex3(x2, y2, z2);
            GL.Vertex3(x2, y1, z2);
            
            // Cara izquierda (x = x1)
            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x1, y1, z2);
            GL.Vertex3(x1, y2, z2);
            GL.Vertex3(x1, y2, z1);
            
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
            
            // Modo de visualización (wireframe)
            if (keyState.IsKeyDown(Key.W) && !previousKeyState.IsKeyDown(Key.W))
                wireframeMode = !wireframeMode;
                
            // Mostrar/ocultar ejes
            if (keyState.IsKeyDown(Key.A) && !previousKeyState.IsKeyDown(Key.A))
                showAxes = !showAxes;
                
            // Activar/desactivar iluminación
            if (keyState.IsKeyDown(Key.L) && !previousKeyState.IsKeyDown(Key.L))
                lightingEnabled = !lightingEnabled;
                
            // Modificar grosor
            if (keyState.IsKeyDown(Key.T) && thickness < 0.5f)
                thickness += 0.01f;
                
            if (keyState.IsKeyDown(Key.G) && thickness > 0.05f)
                thickness -= 0.01f;
                
            // Mover la U en el espacio 3D
            if (keyState.IsKeyDown(Key.I)) // Mover hacia arriba en Y
                translation.Y += 0.1f;
            if (keyState.IsKeyDown(Key.K)) // Mover hacia abajo en Y
                translation.Y -= 0.1f;
            if (keyState.IsKeyDown(Key.J)) // Mover hacia la izquierda en X
                translation.X -= 0.1f;
            if (keyState.IsKeyDown(Key.L)) // Mover hacia la derecha en X
                translation.X += 0.1f;
            if (keyState.IsKeyDown(Key.U)) // Mover hacia adelante en Z
                translation.Z += 0.1f;
            if (keyState.IsKeyDown(Key.O)) // Mover hacia atrás en Z
                translation.Z -= 0.1f;
                
            // Guardar estado de teclas para el siguiente frame
            previousKeyState = keyState;
        }
        
        private KeyboardState previousKeyState;
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Letra U en 3D c/Rotacion, Eje, y Colores"))
            {
                game.Run(60.0);
            }
        }
    }
}