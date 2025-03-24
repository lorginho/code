# Explicación del código 3D con wireframe en OpenTK

Este código es una versión mejorada del programa anterior que utiliza **OpenTK** para crear una ventana gráfica en 3D y renderizar una letra "U" en modo **wireframe** (solo aristas). Incluye funcionalidades como rotación, ejes de referencia, ajuste del ancho de línea y controles adicionales para personalizar la experiencia.

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
- **Suavizado de líneas**: Habilita el suavizado de líneas para mejorar la apariencia del wireframe.

---

## Renderizado (`OnRenderFrame`)

### Limpieza del búfer
- `GL.Clear`: Limpia los búferes de color y profundidad antes de renderizar un nuevo fotograma.

### Configuración de la cámara
- `Matrix4.CreatePerspectiveFieldOfView`: Crea una matriz de proyección en perspectiva para simular una cámara 3D.
- `Matrix4.LookAt`: Define la posición de la cámara, el punto al que mira y la dirección "arriba".

### Rotación
- `Matrix4.CreateRotationX` y `Matrix4.CreateRotationY`: Crean matrices de rotación en los ejes X e Y, respectivamente. Estas rotaciones se aplican a la escena.

### Dibujar ejes
- Si `showAxes` está habilitado, se dibujan los ejes X, Y y Z en colores rojo, verde y azul, respectivamente.

### Dibujar la letra "U" en wireframe
- Se llama al método `DrawUWireframe`, que dibuja la letra "U" utilizando líneas para representar las aristas.

### Información en el título
- El título de la ventana muestra información sobre la rotación, el ancho de línea y la visibilidad de los ejes.

---

## Dibujo de la letra "U" en wireframe (`DrawUWireframe`)

### Coordenadas de los vértices
- Se definen las coordenadas de los vértices para la letra "U" en 3D, incluyendo las partes izquierda, inferior y derecha.

### Dibujo de líneas
- Se utiliza `GL.Begin(PrimitiveType.Lines)` para dibujar las aristas de la letra "U".
- Cada línea se define con dos vértices, y se dibujan todas las aristas necesarias para representar la letra "U" en 3D.

---

## Interacción con el teclado (`OnUpdateFrame`)

### Rotación
- Las teclas de flecha (`Up`, `Down`, `Left`, `Right`) modifican los ángulos de rotación (`rotationX` y `rotationY`), lo que permite rotar la letra "U" en los ejes X e Y.
- La tecla `R` restablece la rotación a su estado inicial.

### Mostrar/ocultar ejes
- La tecla `A` alterna la visibilidad de los ejes.

### Ajustar ancho de línea
- Las teclas `+` y `-` (tanto en el teclado principal como en el numérico) aumentan o disminuyen el ancho de las líneas del wireframe.

### Salir del programa
- La tecla `Escape` cierra la ventana y finaliza el programa.

---

## Ejecución del programa

- El programa crea una ventana de 800x600 píxeles con el título "Letra U en 3D c/Rotacion, Eje, solo Aristas".
- La letra "U" se renderiza en modo wireframe y puede rotarse utilizando las teclas de flecha.
- El bucle principal (`game.Run(60.0)`) asegura que la ventana se actualice a 60 FPS.

---

## Resumen

Este código es una versión mejorada del programa anterior, que se enfoca en el renderizado de la letra "U" en modo wireframe. Incluye funcionalidades adicionales como rotación, ejes de referencia, ajuste del ancho de línea y controles para personalizar la experiencia. Es una excelente introducción a los conceptos de renderizado 3D con OpenTK, especialmente para aplicaciones que requieren visualización de aristas.