# Contenido

- [Diagrama de clases](#diagrama-de-clases)

## Diagrama de clases
```mermaid
classDiagram

    User <|-- Administrator : Inheritance
    User <|-- Customer : Inheritance
    User : +int name
    User : +String lastname
    User : +String email
    User : +Date createDate

    Administrator : +String adminId
    Administrator : +String password
    Administrator : +login()

    Customer : +String customerId
    Customer : +String documentType
    Customer : +String documentNumber
    Customer : +String phoneNumber
    
    Product *-- Customer : composition
    Product : +String productId
    Product : +String type
    Product : +String status
    Product : +String productType
    Product : +String description
    Product : +String value
    Product : +String balance
    Product : +Date createDate

    Payment *-- Product : composition
    Payment : +String paymentId
    Payment : +String productId
    Payment : +String value
    Payment : +Date createDate

```