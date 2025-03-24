using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LetraU3Dv05
{
    
    
    
//*************    
    public class LetraU
    {
        public Vector3 Posicion { get; set; }
        public Vector3 Rotacion { get; set; }

        public LetraU(Vector3 posicionInicial)
        {
            Posicion = posicionInicial;
            Rotacion = Vector3.Zero;
        }
    }

//*************************

    
    public class Game : GameWindow
    {
     
//*********************************     
        private List<LetraU> letrasU;
        private int letraSeleccionada = 0; // Indica cuál "U" se está moviendo
     
     

//********************************     
     
     
        private float rotationX = 9.0f;
        private float rotationY = -12.0f;
        private const float rotationSpeed = 1.5f;
        
        // Color del fondo y de la U
        private Color4 backgroundColor = new Color4(0.05f, 0.1f, 0.2f, 1.0f); // Azul oscuro
        private Color4 letterColor = new Color4(0.9f, 0.5f, 0.1f, 1.0f); // Naranja
        
        // Longitud de los ejes
        private float axisLength = 1.5f;

        // Tamaño inicial de la U (10% de axisLength)
        private float initialSize = 0.30f; // 10% de 1.5f

        // Tamaño de la U
        private float width;
        private float height;
        private float depth;
        private float thickness;
        
        // Modo de visualización
        private bool wireframeMode = false;
        private bool showAxes = true;
        
        // Iluminación
        private bool lightingEnabled = true;

        // Variable que representa el centro geométrico de la U
        private Vector3 CentroGeometricoU;

        // Variable para guardar la posición inicial
        private Vector3 posicionInicialU;

//*******
        // Lista de Vector3 para las posiciones de las "U"
        private List<Vector3> posicionesU;

//*******
        // Constructor modificado para aceptar la posición inicial de la U
        public Game(int width, int height, string title, Vector3 posicionInicialU) 
            : base(width, height, GraphicsMode.Default, title)
        {
            // Guardar la posición inicial
            this.posicionInicialU = posicionInicialU;
            // Inicializar la posición actual de la U
            CentroGeometricoU = posicionInicialU;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(backgroundColor);
            GL.Enable(EnableCap.DepthTest);
            SetupLighting();

            // Inicializar el tamaño de la U como el 10% de axisLength
            width = initialSize;
            height = initialSize * 1.5f; // Mantener la proporción original
            depth = initialSize * 0.4f;  // Mantener la proporción original
            thickness = initialSize * 0.2f; // Mantener la proporción original


            letrasU = new List<LetraU>
            {
                
                new LetraU(new Vector3(0.0f, 0.0f, 0.0f)),  // Primera U en el centro                
                new LetraU(new Vector3(-0.50f, -0.5f, -0.5f))   // Tercer U desplazada a la derecha
            };

/*

//*****
            // Instanciamos n U con distintas posiciones
            posicionesU = new List<Vector3>
            {
                new Vector3(0.0f, 0.0f, 0.0f),  // Centro
                new Vector3(-0.5f, -0.5f, 0.0f),  // Centro
                new Vector3(0.5f, 0.5f, 0.5f)     // A la derecha
                
            };
//******

*/

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


/*
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

            foreach (var posicion in posicionesU)
            {
                CentroGeometricoU = posicion;
                DrawU3D();  // Dibuja una U en la posición actual
            }


            // Mostrar información en el título de la ventana
            Title = $"Letra U 3D - Centro Geom.: X={CentroGeometricoU.X:F1} Y={CentroGeometricoU.Y:F1} Z={CentroGeometricoU.Z:F1} | " +
                    $"Pos. Inicial: X={posicionInicialU.X:F1} Y={posicionInicialU.Y:F1} Z={posicionInicialU.Z:F1} | " +
                    $"Rotac.: X={rotationX:F1}° Y={rotationY:F1}° ";


            SwapBuffers();
        }

*/

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

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            // Dibujar todas las letras U
            foreach (var letra in letrasU)
            {
                DrawU3D(letra.Posicion, letra.Rotacion);
            }

            SwapBuffers();
        }



        private void DrawAxes()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Begin(PrimitiveType.Lines);
            
            // Eje X - Rojo (de -axisLength a axisLength)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-axisLength, 0, 0); // Parte negativa
            GL.Vertex3(axisLength, 0, 0);  // Parte positiva
            
            // Eje Y - Verde (de -axisLength a axisLength)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0, -axisLength, 0); // Parte negativa
            GL.Vertex3(0, axisLength, 0);  // Parte positiva
            
            // Eje Z - Azul (de -axisLength a axisLength)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0, 0, -axisLength); // Parte negativa
            GL.Vertex3(0, 0, axisLength);  // Parte positiva
            
            GL.End();
            
            if (lightingEnabled)
                GL.Enable(EnableCap.Lighting);
        }


/*
        private void DrawU3D()
        {
            // Aplicar el centro geométrico a las coordenadas de la U
            Vector3 offset = CentroGeometricoU;

            // Parte izquierda de la U
            DrawCuboid(
                -width / 2 + offset.X, -height / 2 + offset.Y, -depth / 2 + offset.Z,  // Esquina inferior izquierda trasera
                -width / 2 + thickness + offset.X, height / 2 + offset.Y, depth / 2 + offset.Z  // Esquina superior derecha frontal
            );
            
            // Parte inferior de la U
            DrawCuboid(
                -width / 2 + offset.X, -height / 2 + offset.Y, -depth / 2 + offset.Z,  // Esquina inferior izquierda trasera
                width / 2 + offset.X, -height / 2 + thickness + offset.Y, depth / 2 + offset.Z  // Esquina superior derecha frontal
            );
            
            // Parte derecha de la U
            DrawCuboid(
                width / 2 - thickness + offset.X, -height / 2 + offset.Y, -depth / 2 + offset.Z,  // Esquina inferior izquierda trasera
                width / 2 + offset.X, height / 2 + offset.Y, depth / 2 + offset.Z  // Esquina superior derecha frontal
            );
        }

*/


        private void DrawU3D(Vector3 posicion, Vector3 rotacion)
        {
            GL.PushMatrix();

            // Traslación y Rotación individual
            GL.Translate(posicion.X, posicion.Y, posicion.Z);
            GL.Rotate(rotacion.X, 1.0f, 0.0f, 0.0f); // Rotación en X
            GL.Rotate(rotacion.Y, 0.0f, 1.0f, 0.0f); // Rotación en Y
            GL.Rotate(rotacion.Z, 0.0f, 0.0f, 1.0f); // Rotación en Z

            // Dibujar la U
            DrawCuboid(-width / 2, -height / 2, -depth / 2,
                    -width / 2 + thickness, height / 2, depth / 2);
            DrawCuboid(-width / 2, -height / 2, -depth / 2,
                    width / 2, -height / 2 + thickness, depth / 2);
            DrawCuboid(width / 2 - thickness, -height / 2, -depth / 2,
                    width / 2, height / 2, depth / 2);

            GL.PopMatrix();
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

            // Cambiar la U seleccionada con F1 y F2
            if (keyState.IsKeyDown(Key.F1)) letraSeleccionada = 0;
            if (keyState.IsKeyDown(Key.F2)) letraSeleccionada = 1;

            // Movimiento de la U seleccionada
            LetraU u = letrasU[letraSeleccionada];
            float moveSpeed = 0.05f;



            // Salir del programa
            if (keyState.IsKeyDown(Key.Escape)) Exit();
        }
    }




  
  
        class Program
        {
            static void Main(string[] args)
            {
                // Definir la posición inicial de la U (por ejemplo, desplazada a la derecha y arriba)
                // Rango Valores permitidos (-1.0 ; 1.0) para x,y,z
                Vector3 posicionInicialU = new Vector3(0.1f, 0.2f, 0.3f);

                // Crear la instancia de Game con la posición inicial
                using (Game game = new Game(800,600, "Letra U en 3D", posicionInicialU))
                {
                    game.Run(60.0);
                }
            }
        }
    }

