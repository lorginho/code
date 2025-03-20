Código en el archivo `U2D.cs`. 

El archivo contiene un programa en C# que utiliza OpenTK para dibujar y manipular una letra "U" en una ventana gráfica. Aquí hay un resumen del código:

1. **Clase `Game`**:
   - Hereda de `GameWindow` de OpenTK.
   - Define variables para la posición, tamaño, rotación y escala de la letra "U".
   - Configura la ventana y el color de fondo.
   - Dibuja ejes y la letra "U" en cada fotograma.
   - Permite mover, escalar y rotar la "U" usando el teclado.

2. **Métodos principales**:
   - `OnLoad`: Configura el color de fondo.
   - `OnRenderFrame`: Dibuja los ejes y la "U", aplicando las transformaciones necesarias.
   - `OnResize`: Ajusta el viewport cuando se cambia el tamaño de la ventana.
   - `OnUpdateFrame`: Actualiza la posición, escala y rotación de la "U" según las entradas del teclado.
   - `DibujarEjes`: Dibuja los ejes X e Y.
   - `DibujarLetraU`: Dibuja la letra "U" utilizando primitivas cuadradas.

3. **Clase `Program`**:
   - Contiene el método `Main` que inicia el juego y ejecuta el bucle principal a 60 FPS.
