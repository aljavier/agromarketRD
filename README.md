# AgroMarketRD

Proyecto académico que se basa en una especie de bolsa de valores pero de productos agropecuarios. Este es solo el web service.

Hay unos cuantos grupos que funjen algunos como **productores** (o vendedores) y otros como **compradores**.

## Requisitos ##

Estos requisitos sólo aplican si se quiere ejecutar el proyecto.

* Windows
* .Net Framework
* Microsoft SQL Server
* IIS Server 

## Interacción con el servicio web según el actor ##

**Autentificiación**: Todos los equipos (vendedores, compradores o auditoría) se autentifican usando el método ***SignIn***. 
Este retorna un ***token*** que junto al ***userName*** serán los datos de autorización para interactuar con el servicio mientras
la sesión este activa. También existe el método ***LogOff***  para terminar la sesión, si se quiere.

**Crear una oferta libre al mercado**: Esta es la forma de un **vendedor/productor** decirle al servicio web 
*tengo estos productos disponibles a este precio, esta cantidad, con esta unidad*, las ofertas se crean un producto a la vez.
Todos los productos usados (no importa el actor o método) deben estar registrados en el mercado, se verifican por el *código del producto*, el método ***GetProducts***
retorna el católogo de productos permitidos en el **mercado**.

**Crear una intención de compra**: Esta es una acción sólo para compradores. Los compradores pueden hacer una *intención de compra*
en el mercado a partir de los productos disponibles (*GetProducts*) o a partir de las ofertas disponibles ( ***GetOffers*** ). 
    1. Para crear una intención a partir de los productos disponibles se usa ***CreateIntentionToBuy***, se le pasan los productos deseados.
    2. Para crear una intención a partir de varias ofertas de **vendedores/productores** se usa ***CreateIntentionToBuyFromOffers***, que recibe
    una lista de los id de la oferta. Esto es sólo para usar en la intención los productos de esa oferta porque los **vendedores/productores** 
    para hacer una *intención de venta* a esa *intención de compra* igual tienen que llamar el método de ***CreateIntentionToSell*** (ver más delante).

**Ver intenciones de compra**: Esto retornaría también quienes han hecho una *intención de venta* a esa *intención de compra*. Se usa el método **GetIntentionToBuy**
para detalles de una intención y ***GetAllIntentionsToBuy*** para obtener todas las intenciones de compras.

*También hay métodos para cancelar una intención de esta o de venta, ver documentación técnica*.

**Crear intención de venta**: Esto es para los *vendedores/productores*, una *intención de venta* se hace directa a una *intención de compra*. Para esto
se usa el método ***CreateIntentionToSell***.

**Concretar una venta/compra**: Esto es cuando ambas partes deciden concretar el negocio. Es decir, cuando el *comprador* acepta la oferta que le hace un
*vendedor/productor*. Para esto ambas partes deben *firmar*, esto es llamar el método ***MakeDeal***. Una vez ambos hagan esto la venta deberá estar disponible en 
el histórico de ventas.

**Obtener historial de ventas concretadas**: Esto se supone es principalmente de **Auditoría** pero ahora mismo por simplicidad todos los 
*actores* pueden hacer uso de él. Este retorna todas las ventas que se han concretado (que ambas partes firmaron) y el nombre del método es ***GetAllSells***.

*** Hay otros métodos en el servicio web, cada uno tiene una breve documentación en la cabecera. Por ejemplo, esta el método para obtener las unidades 
de productos que son válidas para el sistema. Ver documentación en código o en el documento [AgroMarketRD.Service.XML](https://raw.githubusercontent.com/aljavier/agromarketRD/master/Recursos/AgroMarketRD.Service.XML).***

## Configurando y corriendo el servicio web ##

Para esto sólo tienen que cambiar el *Web.config* en el proyecto **AgroMarketRD.Service** para que apunte a una base de datos local o dónde
sea que la tenga.

Si desea puede ejecutar [el script de creación de la base de datos que esta aquí](https://github.com/aljavier/agromarketRD/blob/master/Recursos/script_database.sql) y que incluye
datos como los usuarios de los equipos, algunos productos, las unidades de medidas y algunas transacciones.

***IMPORTANTE***: Si corren el *script* mencionado más arriba ariba es bueno que sepan que las credenciales de 
todos los equipos y nombre de usuarios estan en [este archivo](https://raw.githubusercontent.com/aljavier/agromarketRD/master/Recursos/equipos_itos.txt).

Para **correr el servicio web** el proyecto a ejecutar es ***AgroMarketRD.Service***.

## Código de errores ##

El servicio web retorna algunos códigos de errores (muy pocos), importante saber:

1. **AG000**: Todo se procesó exitosamente, no hay errores. 
2. **AG001**: Error de autentificación.
3. **AG002**: Una excepción fue lanzada en el sercivio web.
4. **AG003**: La sesión no es válida. *userName* y/o *token* o no existen en ninguna sesión en la db o expiraron (no se valida expiración actualmente).
5. **AG004**: Error genérico controlado, de que algo salío mal. Por ejemplo, *tipo usuario* no es válido para la acción. Ver campo *description* retornado.

## Notas finales ##

Muchas cosas podrían ser cambiadas de cara a un servicio que se pueda usar en *real life* para esto y para
respetar mejores estándares de ingeniería de software, esto sólo *get the job done*.

El servicio tiene la estructura para manejar una especie de registros y asientos contables (en la entidad *cuenta* 
que se relaciona con el *usuario*) pero no se esta tomando en cuenta actualmente en el servicio.

Hay unos cuantos *TODO* en el código que indican cosas que podrían ser cambiadas.



