# AgroMarketRD

Proyecto academico que se basa en una especie de bolsa de valores pero de productos agropecuarios. Este es solo el web service.

## Modelo de la DB ##

Ver proyecto **AgroMarketRD.Core**. En todo caso, este modelo no es accesible a quienes consumirán el servicio web.
En este caso el modelo relevante son los *Contracts* en el proyecto AgroMarketRD.Service en la carpeta Contracts.

Dicho demás esta, que si se crea un proxy class (Ver [Consuming a Web Service](https://www.youtube.com/watch?v=ycKnYOlQDEE)) este generá todo lo necesario para trabajar con el servicio web (incluso los *responses*).

## TO DO ##

**PRIORIDAD**
* Intencion de compra debe permitir mas de un producto. (cambiar)
* Intencion de venta directa a intencion de compra. 

### ASAP - CASOS DE USOS ###

**Productor**: 
* ~~Coloca productos en el mercado~~.
* Ve intenciones de compra de compradores.
* Ofrece productos a una intención de compra (crear intención de venta)
    1. Puede ofrecer más o menos de los que tiene en el mercado.
    2. Puede ofrecer menos de los que requiera la intención de compra.
    3. Puede poner precio especial a productos para una intención de compra.

**Comprador**:
* Puede colocar intención de compra. Datos mínimos:
    1. ID
    2. Cliente
    3. FechaCreacion
    4. FechaExpiracion
    5. ProductosAComprar (N productos)
        1. ID
        2. Nombre
        3. Cantidad
        4. Precio
* Puede seleccionar productos colgados en el mercado 
    1. Puede comprar más de uno a la vez.

**Otros**:
* Ver listado productos disponibles.
* Filtrar listado productos disponibles.


Puede ser que algo se nos este pasando, si es así, por favor ayudar [abriendo un issue](https://github.com/aljavier/agromarketRD/issues).

Además falta la lógica de negocios de todos los métodos.

Por favor, cualquier solicitud, sugerencia o inquietud exponerla aquí en el repositorio, [abrir un issue](https://github.com/aljavier/agromarketRD/issues).

### Métodos a crear **(proposal)** ###

**GetDeal**:       Retorna las intenciones de compras pendientes de concretarse del comprador/productor.
* IntentionId
* Comment
* Buyer
* BuyerId
* ProductorId
* Productor
  
**SignDeal**:       Firma una compra/venta de parte de comprador/productor. Ambos deben firmarla (a.k.a llamar este método).
* IntentionId
* BuyerId
* Buyer
* productorId
* Productor
* Date

**GetAlerts**:      Retorna el estado de compra/venta pendientes de concretarses (de llamar SignDeal) de un comprador/productor.
* IntentionId
* Comment
* Signers
* ProductorId
* Productor
* Pending 
* BuyerId
* Buyer
* Pending 
 
**GetFinalizedDeals**:   Retorna las compras/ventas finalizadas (a.k.a firmadas) de un comprador/productor.
* IntentionId
* OfferId
* Comment
* BuyerId
* Buyer
* ProductorId
* Productor
* Quantity
* TotalAmount

***Esto podría cambiar a la hora de hacerse.***

**Nota:** *Sabemos que estamos cortos de tiempo y todos tenemos mil y una cosa que hacer, intentaremos tener ese listo cuanto antes. Aunque no puedo comprometerme para hoy mismo*.
