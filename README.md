### Proyecto de Programacion Grafica ELE102

## Tarea 1: Dibujar un letra U en un Plano Tridimensional
## Tarea 2: La letra U, debe moverse
  


### Resumen del Código

### Clases

#### Game:

Hereda de GameWindow (OpenTK).


Contiene la lógica principal del programa: inicialización, renderizado y manejo de eventos.



#### Program:

  

Contiene el método Main, que es el punto de entrada del programa.

  

Crea una instancia de Game y ejecuta el bucle principal.

  

### Variables

Variables miembro de la clase Game

Variable Descripción

posX Posición en el eje X de la "U".

posY Posición en el eje Y de la "U".

ancho Ancho total de la "U".

altura Altura total de la "U".

grosor Grosor de las líneas de la "U".

escala Escala de la "U".

rotacion Rotación de la "U" en grados.

Variables locales

Variable Descripción

e Parámetro de tipo FrameEventArgs o EventArgs.

keyState Estado actual del teclado (en OnUpdateFrame).

velocidad Velocidad de movimiento de la "U".

izquierda Coordenada X del lado izquierdo de la "U".

derecha Coordenada X del lado derecho de la "U".

arriba Coordenada Y de la parte superior de la "U".

abajo Coordenada Y de la parte inferior de la "U".

#### Métodos

Clase Game

Método Descripción

Game(int width, int height, string title) Constructor de la clase. Inicializa la ventana con el tamaño y título especificados.

OnLoad(EventArgs e) Se ejecuta al cargar la ventana. Configura el color de fondo.

OnRenderFrame(FrameEventArgs e) Se ejecuta en cada fotograma. Limpia el búfer, configura la proyección y dibuja los ejes y la "U".

DibujarEjes() Dibuja los ejes X e Y en rojo y verde, respectivamente.

DibujarLetraU() Dibuja la letra "U" en la posición actual, con el tamaño y rotación especificados.

OnResize(EventArgs e) Se ejecuta al redimensionar la ventana. Ajusta el viewport de OpenGL.

OnUpdateFrame(FrameEventArgs e) Se ejecuta en cada actualización de fotograma. Maneja la entrada del usuario.

Clase Program

Método Descripción

Main(string[] args) Punto de entrada del programa. Crea una instancia de Game y ejecuta el bucle principal.

Flujo del Programa

Inicialización:

  

Se crea una instancia de Game con un tamaño de ventana de 800x800 píxeles y un título.

  

Se configura el color de fondo en OnLoad.

  

Bucle principal:

  

En cada fotograma:

  

Se limpia el búfer y se configura la proyección ortográfica.

  

Se dibujan los ejes y la "U".

  

Se maneja la entrada del usuario para mover, escalar y rotar la "U".

  

El bucle se ejecuta a 60 FPS.

  

Eventos:

  

Si el usuario presiona Escape, el programa termina.

  

Si el usuario redimensiona la ventana, se ajusta el viewport.

  

Funcionalidades

Dibujo de la "U":

  

La "U" se dibuja en una posición relativa al centro del plano.

  

Se puede mover, escalar y rotar usando el teclado.

  

Ejes fijos:

  

Los ejes X e Y se dibujan en rojo y verde, respectivamente, y permanecen fijos.

  

Interactividad:

  

Movimiento: Teclas de dirección (←, →, ↑, ↓).

  

Escalado: Teclas + y -.

  

Rotación: Teclas R y T.