# Explicación del código 3D en OpenTK

Este código es un programa en C# que utiliza la biblioteca **OpenTK** para crear una ventana gráfica en 3D y renderizar una letra "U" en tres dimensiones. Además, permite rotar la letra "U" utilizando las teclas de flecha del teclado.

---

## Estructura general del programa

- **Clase `Game`**: Hereda de `GameWindow` (proporcionado por OpenTK) y maneja la ventana gráfica, la lógica de renderizado y la interacción con el teclado.
- **Método `Main`**: En la clase `Program`, inicia la aplicación creando una instancia de `Game` y ejecutándola con una frecuencia de actualización de 60 FPS.

---

## Inicialización y configuración

### Método `OnLoad`
Se ejecuta cuando la ventana se carga por primera vez. Configura parámetros de OpenGL:
- `GL.ClearColor`: Establece el color de fondo de la ventana (gris oscuro).
- `GL.Enable(EnableCap.DepthTest)`: Habilita la prueba de profundidad para renderizar correctamente los objetos 3D.

### Método `OnResize`
Se llama cuando la ventana cambia de tamaño. Ajusta el viewport de OpenGL para que coincida con las nuevas dimensiones de la ventana.

---

## Renderizado (`OnRenderFrame`)

### Limpieza del búfer
- `GL.Clear`: Limpia los búferes de color y profundidad antes de renderizar un nuevo fotograma.

### Configuración de la cámara
- `Matrix4.CreatePerspectiveFieldOfView`: Crea una matriz de proyección en perspectiva para simular una cámara 3D.
- `Matrix4.LookAt`: Define la posición de la cámara, el punto al que mira y la dirección "arriba".

### Rotación
- `Matrix4.CreateRotationX` y `Matrix4.CreateRotationY`: Crean matrices de rotación en los ejes X e Y, respectivamente. Estas rotaciones se aplican a la escena.

### Dibujo de la letra "U"
- Se llama al método `DrawU3D`, que utiliza `DrawCuboid` para dibujar tres partes de la letra "U" (dos lados verticales y una base horizontal) como cuboides 3D.

---

## Dibujo de la letra "U" (`DrawU3D` y `DrawCuboid`)

### Método `DrawU3D`
Define las dimensiones de la letra "U" (grosor, anchura, altura y profundidad) y llama a `DrawCuboid` para dibujar cada parte de la letra.

### Método `DrawCuboid`
Dibuja un cuboide (un prisma rectangular) utilizando `GL.Begin(PrimitiveType.Quads)` y `GL.Vertex3` para definir las caras. Cada cara se colorea con un tono de gris ligeramente diferente para dar sensación de profundidad.

---

## Interacción con el teclado (`OnUpdateFrame`)

### Rotación
- Las teclas de flecha (`Up`, `Down`, `Left`, `Right`) modifican los ángulos de rotación (`rotationX` y `rotationY`), lo que permite rotar la letra "U" en los ejes X e Y.
- La tecla `R` restablece la rotación a su estado inicial.

### Salir del programa
- La tecla `Escape` cierra la ventana y finaliza el programa.

---

## Ejecución del programa

- El programa crea una ventana de 800x600 píxeles con el título "Letra U en 3D c/Rotacion, Blanco s/ Gris".
- La letra "U" se renderiza en 3D y puede rotarse utilizando las teclas de flecha.
- El bucle principal (`game.Run(60.0)`) asegura que la ventana se actualice a 60 FPS.

---

## Resumen

Este código es un ejemplo básico de cómo usar OpenTK y OpenGL para crear una aplicación gráfica en 3D. Renderiza una letra "U" en 3D y permite al usuario rotarla utilizando el teclado. Es una buena introducción a los conceptos de renderizado 3D, transformaciones de matrices y manejo de entrada en OpenTK.

![[U3D.png]]