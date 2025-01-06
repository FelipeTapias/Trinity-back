# Proyecto Trinity

# Contenido

- [Versiones](#versiones)
- [Cambios](#cambios)

## Versiones

- Release 1.0.0

## Cambios
| Usuario | Fecha | Detalle del Cambio | Versión |
|---------|-------|--------------------|---------|
| Anfeta | 2024/12/16  | Crud de usuario completo y se implementa serilog. | 1.0.0
| Anfeta | 2024/12/16  | Se crean reglas de negocio para el crud. | 1.0.0
| Anfeta | 2024/12/20  | Se integra RedisCache para guardar los usuarios. | 1.0.0
| Anfeta | 2024/12/21  | Se documenta los controladores y se crea nuevo endpoint para obtener el id del usuario si tengo el documento. | 1.0.0
| Anfeta | 2024/12/26  | Se crea interceptor en el controlador para generar una respuesta generica para todas las operaciones. | 1.0.0
| Anfeta | 2024/12/30  | Se hace configuracion en el contexto de mongo para obtener n colecciones, además, se agrega la nueva clase producto y para finalizar se agregan las politicas de CORS | 1.0.0
| Anfeta | 2024/01/01  | Creación de Diagrama de clases en documentación técnica | 1.0.0
| Anfeta | 2024/01/01  | Segregación de la clase usuario con administrador y cliente, tambien se ajusta el controlador de usuario para que sea funcional solo para clientes | 1.0.0
| Anfeta | 2024/01/02  | Creacion de de productos por cliente y obtencion de productos por cliente | 1.0.0
| Anfeta | 2024/01/52  | Finalizacion servicio de producto y cambio de estado y deuda del mismo | 1.0.0