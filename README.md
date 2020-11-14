# Multiplayer-LAN
- Iván Rodríguez Fernández
- Miquel Bellet Coll

## Controles
### Teclado
- Movimiento: WASD / 🡡🡠🡣🡢
- Disparo: E

### X Box Controller
- Movimiento: Joystick izquierdo
- Disparo: A

### PS4 Controller
- Movimiento: Joystick izquierdo
- Disparo: X

## Canvas
- Se adaptan al tamaño del dispositivo o al aspect ratio que se escoja para la build

## Escenas
### Lobby
- Uso de Network Discovery para mostrar una lista de servidores activos
- Acciones del usuario:
  - Iniciar Servidor
  - Crear Partida
  - Unirse a Partida
- Escoger el color del Player
- Cambiar el nombre del Player

### Game
- 4 NPCs de color amarillo y con spawn aleatorio
- Los jugadores utilizan el mecanismo Round Robin para spawnear al principio de la partida
- El jugador aparece de color azul por defecto o del colo escogido en la lobby
- El jugador enemigo aparece siempre de color rojo
- Todos los jugadores no NPCs tienen el nombre encima del tanque
- Todos los objetos que se instancian a través del servidor (misiles, tanques y partículas) lo hacen correctamente
- La cámara muestra siempre todos los jugadores y NPCs que continuan con vida
- El círculo de salud de todos los tanques se actualiza a través del servidor para que todos los jugadores puedan verlo
- Cuando el jugador actúa como host y se desconecta durante la partida, ésta termina y todos los jugadores vuelven a la Lobby

#### Errores
- Cuando el jugador actúa como cliente y se desconecta durante la partida, aparece un error en el NetworkDiscovery
