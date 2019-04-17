# GIB2019
Demos vistas en el GAB2019-AsturiasNetConf

# LearningTransport
Todos los ejemplo de esta sección se han implementado utilizado el transporte LearningTransport por simplicidad

### Demo01. Basics
Configuración básica de endpoint.
Envíos de manera local al endpoint de CliendUI.

### Demo02. Command Processing
Configuración de envío de comandos a un nuevo enpoint, Sales.
Se crea un Handler en Sales para el procesado del comando.

### Demo03. Events Processing
Se agregan dos nuevos servicios, Billing y Shipping.
Ambos implementan handlers para manajear eventos.

### Demo04. Trobleshotting
Se introducen dos tipos de error en el servicio de Sales.
Un error sistémico que nos va a permitir verificar todos los tipos de reitentos que se realizan hasta que el error es movido a una cola de error.
Un error random para comprobar como los inmediates retres solucionan el problema.

### Demo05. Sagas
Se introduce el concepto Saga.
La implementación de la misma se realiza en el servicio de Shipping, que es quien considera la saga terminada cuando recive eventos de Sales y de Billing.

## AzureServiceBus
Todos los ejemplo de esta sección se han implementado utilizado el transporte AzureServiceBus. Para poder ejecutarlos deberás disponer de un AzureServiceBus en tu suscripcion de Azure.

### Demo01. Trobleshotting
Se introducen dos tipos de error en el servicio de Sales.
Un error sistémico que nos va a permitir verificar todos los tipos de reitentos que se realizan hasta que el error es movido a una cola de error.
Un error random para comprobar como los inmediates retres solucionan el problema.
