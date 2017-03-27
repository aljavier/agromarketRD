# AgroMarketRD

Proyecto academico que se basa en una especie de bolsa de valores pero de productos agropecuarios. Este es solo el web service.

# Modelo de la DB

Ver proyecto **AgroMarketRD.Core**. En todo caso, este modelo no es accesible a quienes consumirán el servicio web.
En este caso el modelo relevante son los *Contracts* en el proyecto AgroMarketRD.Service en la carpeta Contracts.

Dicho demás esta, que si se crea un proxy class (Ver [Consuming a Web Service](https://www.youtube.com/watch?v=ycKnYOlQDEE)) este generá todo lo necesario para trabajar con el servicio web (incluso los *responses*).

# TO DO

Queda por crear al menos dos métodos (además de los de auditoría): un método que retorna a un comprador/productor quienes han ofertado algo a su demanda y un médoto de culminación del trato que ambas partes deben hacer.

Además falta la lógica de negocios de todos los métodos.

Por favor, cualquier solicitud, sugerencia o inquietud exponerla aquí en el repositorio, abrir un *issue*.
