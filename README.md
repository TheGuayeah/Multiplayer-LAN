# Multiplayer-LAN
Video: https://youtu.be/Wtf7V2vbEiE
- Iván Rodríguez Fernández
- Miquel Bellet Coll

## Notas 
Desactivar el antivirus antes de ejecutar una Build
Probar el proyecto desde una Build y desde el editor de Unity (NO DESDE 2 BUILDS) para que el PlayerPrefs no falle
En el momento de la grabación del video hemos experimentado ciertos errores provocados por el editor de Unity sin haber realizado ningún cambio desde la última publicación del proyecto en GitHub la semana anterior cuando no nos daba ningún error. Éstos errores son distintos cuando cada uno de 

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
- Si los NPCs tienen más cerca a otro NPC que a un jugador, también se atacan entre ellos para que parezca más realista
- Todos los objetos que se instancian a través del servidor (misiles, tanques y partículas) lo hacen correctamente
- La cámara muestra siempre todos los jugadores y NPCs que continúan con vida
- El círculo de salud de todos los tanques se actualiza a través del servidor para que todos los jugadores puedan verlo
- Cuando el jugador actúa como host y se desconecta durante la partida, ésta termina y todos los jugadores vuelven a la Lobby


#### Errores
- Cuando el jugador actúa como cliente y se desconecta durante la partida, aparece un error en el NetworkDiscovery
