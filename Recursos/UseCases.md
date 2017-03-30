#The great market in the sky


Proyecto final - Integracion con tecnología propietaria

Este proyecto es el examen final de esta materia. Se enfocará en la integración utilizando webservices.

--

## Actores:

  - *Productor*
  - *Comprador*
  - *Auditor*
  - *Mercado*

--

# Historias de usuarios

## *Productor*

### Colgar productos disponibles

Como **Productor** quiero poder colocar mis productos a la venta en la plataforma.

**Criterio de aceptacion:**
  
  - Los productores usan el webservice para enviar un listado de productos.
  - Los productos tendran las siguientes propiedades:
    - Cantidad
    - Precio por unidad
    - IDProductor
    - TipoDeUnidad { 1: unidad, 2: kilo, 3: saco }

*Nota: El `TipoDeUnidad` será un numero.*

---

### Ver listado de intenciones de compra

Como **Productor** quiero poder ver la lista de intenciones de compra que han colgado los compradores.

Criterio de aceptacion:

- Al consultar el servicio de intenciones de compra, este retorna un listado de intenciones de compra.

---

### Colocar intencion de venta

Como **Productor** quiero poder seleccionar una intencion de compra y ofrecer mis productos al comprador.

**Criterio de aceptacion:**

- Puedo ofrecer mas de lo que tengo en existencia en la plataforma
- Puedo ofrecer menos de lo que requiere la intencion de compra
- Puedo colocar un precio especial para los productos que quiera ofrecer

---

## *Comprador*

### Colgar intencion de compra

Como **Comprador** quiero poder colocar en el mercado mi intención de compra.

**Criterio de aceptacion:**

- Las intenciones de compra tienen:

```
  {
    ID: 000,
    Cliente: cliente,
    FechaDeCreacion: fecha,
    FechaDeExpiracion: fecha,
    ProductosAComprar: [ 
      productoA: { IDProducto, Nombre, Cantidad, Precio },
      productoB: { IDProducto, Nombre, Cantidad, Precio } ]
  }
```

---

### Comprar productos encontrados en la plataforma

Como **Comprador** quiero poder seleccionar productos para hacer una compra.

Criterio de aceptacion:

- Se puede comprar mas de un producto a la vez
- En esta etapa no es necesario manejar metodo de pago

---

## *Todos*

### Ver listado de productos disponibles

---

### Filtrar listado de productos disponibles

---
