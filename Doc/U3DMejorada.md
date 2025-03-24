# Explicación del código 3D mejorado en OpenTK

Este código es una versión mejorada del programa anterior que utiliza **OpenTK** para crear una ventana gráfica en 3D y renderizar una letra "U" en tres dimensiones. Incluye nuevas funcionalidades como iluminación, ejes de referencia, modo de visualización (sólido o wireframe) y controles adicionales para personalizar la experiencia.

---

## Estructura general del programa

- **Clase `Game`**: Hereda de `GameWindow` (proporcionado por OpenTK) y maneja la ventana gráfica, la lógica de renderizado y la interacción con el teclado.
- **Método `Main`**: En la clase `Program`, inicia la aplicación creando una instancia de `Game` y ejecutándola con una frecuencia de actualización de 60 FPS.

---

## Inicialización y configuración

### Método `OnLoad`
Se ejecuta cuando la ventana se carga por primera vez. Configura parámetros de OpenGL:
- `GL.ClearColor`: Establece el color de fondo de la ventana (azul oscuro).
- `GL.Enable(EnableCap.DepthTest)`: Habilita la prueba de profundidad para renderizar correctamente los objetos 3D.
- `SetupLighting`: Configura la iluminación básica de la escena.

### Método `SetupLighting`
Configura la iluminación utilizando OpenGL:
- **Luz ambiental, difusa y especular**: Define los colores de la luz.
- **Material de la letra "U"**: Define cómo la luz interactúa con la superficie de la letra.

---

## Renderizado (`OnRenderFrame`)

### Limpieza del búfer
- `GL.Clear`: Limpia los búferes de color y profundidad antes de renderizar un nuevo fotograma.

### Configuración de la cámara
- `Matrix4.CreatePerspectiveFieldOfView`: Crea una matriz de proyección en perspectiva para simular una cámara 3D.
- `Matrix4.LookAt`: Define la posición de la cámara, el punto al que mira y la dirección "arriba".

### Rotación
- `Matrix4.CreateRotationX` y `Matrix4.CreateRotationY`: Crean matrices de rotación en los ejes X e Y, respectivamente. Estas rotaciones se aplican a la escena.

### Control de iluminación
- `GL.Enable(EnableCap.Lighting)` o `GL.Disable(EnableCap.Lighting)`: Activa o desactiva la iluminación según la configuración del usuario.

### Modo de visualización
- `GL.PolygonMode`: Cambia entre modo sólido (`Fill`) y modo wireframe (`Line`) según la configuración del usuario.

### Dibujo de ejes
- Si `showAxes` está habilitado, se dibujan los ejes X, Y y Z en colores rojo, verde y azul, respectivamente.

### Dibujo de la letra "U"
- Se llama al método `DrawU3D`, que utiliza `DrawCuboid` para dibujar tres partes de la letra "U" (dos lados verticales y una base horizontal) como cuboides 3D.

### Información en el título
- El título de la ventana muestra información sobre la rotación, el estado de la iluminación, el modo de visualización y la visibilidad de los ejes.

---

## Dibujo de la letra "U" (`DrawU3D` y `DrawCuboid`)

### Método `DrawU3D`
Define las dimensiones de la letra "U" (grosor, anchura, altura y profundidad) y llama a `DrawCuboid` para dibujar cada parte de la letra.

### Método `DrawCuboid`
Dibuja un cuboide (un prisma rectangular) utilizando `GL.Begin(PrimitiveType.Quads)` y `GL.Vertex3` para definir las caras. Si la iluminación está desactivada, se usa un color sólido (naranja).

---

## Interacción con el teclado (`OnUpdateFrame`)

### Rotación
- Las teclas de flecha (`Up`, `Down`, `Left`, `Right`) modifican los ángulos de rotación (`rotationX` y `rotationY`), lo que permite rotar la letra "U" en los ejes X e Y.
- La tecla `R` restablece la rotación a su estado inicial.

### Modo de visualización
- La tecla `W` alterna entre modo sólido y modo wireframe.

### Mostrar/ocultar ejes
- La tecla `A` alterna la visibilidad de los ejes.

### Activar/desactivar iluminación
- La tecla `L` activa o desactiva la iluminación.

### Modificar grosor
- Las teclas `T` y `G` aumentan o disminuyen el grosor de la letra "U".

### Salir del programa
- La tecla `Escape` cierra la ventana y finaliza el programa.

---

## Ejecución del programa

- El programa crea una ventana de 800x600 píxeles con el título "Letra U en 3D c/Rotacion, Eje, y Colores".
- La letra "U" se renderiza en 3D y puede rotarse utilizando las teclas de flecha.
- El bucle principal (`game.Run(60.0)`) asegura que la ventana se actualice a 60 FPS.

---

## Resumen

Este código es una versión mejorada del programa anterior, que incluye funcionalidades adicionales como iluminación, ejes de referencia, modo de visualización (sólido o wireframe) y controles para personalizar la experiencia. Es una excelente introducción a los conceptos avanzados de renderizado 3D, iluminación y manejo de entrada en OpenTK.