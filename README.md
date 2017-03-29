# AgroMarketRD

Proyecto academico que se basa en una especie de bolsa de valores pero de productos agropecuarios. Este es solo el web service.

## Modelo de la DB ##

Ver proyecto **AgroMarketRD.Core**. En todo caso, este modelo no es accesible a quienes consumirán el servicio web.
En este caso el modelo relevante son los *Contracts* en el proyecto AgroMarketRD.Service en la carpeta Contracts.

Dicho demás esta, que si se crea un proxy class (Ver [Consuming a Web Service](https://www.youtube.com/watch?v=ycKnYOlQDEE)) este generá todo lo necesario para trabajar con el servicio web (incluso los *responses*).

## TO DO ##

Queda por crear al menos dos métodos (además de los de auditoría): un método que retorna a un comprador/productor quienes han ofertado algo a su demanda y un médoto de culminación del trato que ambas partes deben hacer.

Sería algo así:
1. **Un comprador tiene una demanda**: use el método de createRequest.
2. **Un productor quiere hacer una oferta**: hace una oferta (createOffer) general, ahora decide hacerle esa oferta al comprador. La linkea con makeDeal por la offerId y el requestId.
3. **Comprador quiere ver las ofertas a su demanda**: este es el metodo que nos falta hacer.
4. **Comprador decide aceptar una oferta**: este sería otro método que nos faltaría. El comprador *firma* la compra.
5. **Productor quiere saber si han aceptado su oferta**: otro método que nos faltaría. Entonces para concretar la oferta luego de llamar ese método el productor *firmaría* la oferta. 
6. Ahí concluye la transacción y se desactiva la demanda. Asequible ahora sólo para consulta para ese productor/comprador y para auditoria en historico de ventas.

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
