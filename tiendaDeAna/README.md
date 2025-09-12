## principal structure

```
Menu Principal
│
├── View Menu and Select Products       
│   │   Menú interactivo con productos (↑↓ para navegar, X para agregar, Z para volver)
│   │
│   └── Después de seleccionar -> pregunta cantidad -> valida stock -> agrega al carrito
│
├── Confirm and Purchase Product        
│   │   Menú interactivo mostrando el carrito actual
│   │
│   ├── ↑↓ para navegar en los productos del carrito
│   │
│   ├── X -> Editar cantidad o eliminar producto
│   │
│   └── Z -> Regresar al menú principal
│
├── Pay for Order            
│   │   Muestra total + aplica descuentos (10% ≥ 10000, 20% ≥ 20000)
│   │   Pregunta cuánto dinero entrega el cliente
│   │   Calcula cambio
│   │
│   ├── Si confirma, vacía carrito y finaliza compra con mensaje de despedida.
│   │
│   └── Si cancela, regresa al menú principal
│
└── Exit                     
Sale de la aplicación y muestra agradecimiento
```