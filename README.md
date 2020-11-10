# Using gRPC in Microservices for Building a high-performance Interservice Communication with .Net 5

**Check Explanation of this Repository on Medium -> https://medium.com/aspnetrun/using-grpc-in-microservices-for-building-a-high-performance-interservice-communication-with-net-5-11f3e5fa0e9d**

### Overall Picture
See the overall picture of **implementations on gRPC in Microservices for Building a high-performance Interservice Communication with .Net 5** on real-world **e-commerce microservices** project. You can see that we will have 6 microservices which we are going to develop.
We will use Worker Services and Asp.Net 5 Grpc applications to build client and server gRPC components defining proto service definition contracts.

![Overall Picture of Repository](https://user-images.githubusercontent.com/1147445/97865027-991b7200-1d1a-11eb-927e-3f5580a7f5b5.png)

Basically we will implement e-commerce logic with only gRPC communication. We will have 3 gRPC server applications which are Product — ShoppingCart and Discount gRPC services. And we will have 2 worker services which are Product and ShoppingCart Worker Service. Worker services will be client and perform operations over the gRPC server applications. And we will secure the gRPC services with standalone Identity Server microservices with OAuth 2.0 and JWT token.

### ProductGrpc Server Application
First of all, we are going to develop ProductGrpc project. This will be asp.net gRPC server web application and expose apis for Product Crud operations.

### Product Worker Service
After that, we are going to develop Product Worker Service project for consuming ProductGrpc services. This product worker service project will be the client of ProductGrpc application and generate products and insert bulk product records into Product database by using client streaming gRPC proto services of ProductGrpc application. This operation will be in a time interval and looping as a service application.

### ShoppingCartGrpc Server Application
After that, we are going to develop ShoppingCartGrpc project. This will be asp.net gRPC server web application and expose apis for SC and SC items operations. The grpc services will be create sc and add or remove item into sc.

### ShoppingCart Worker Service
After that, we are going to develop ShoppingCart Worker Service project for consuming ShoppingCartGrpc services. This ShoppingCart worker service project will be the client of both ProductGrpc and ShoppingCartGrpc application. This worker service will read the products from ProductGrpc and create sc and add product items into sc by using gRPC proto services of ProductGrpc and ShoppingCartGrpc application. This operation will be in a time interval and looping as a service application.

### DiscountGrpc Server Application
When adding product item into SC, it will retrieve the discount value and calculate the final price of product. This communication also will be gRPC call with SCGrpc and DiscountGrpc application.

### Identity Server
Also, we are going to develop centralized standalone Authentication Server with implementing IdentityServer4 package and the name of microservice is Identity Server.
Identity Server4 is an open source framework which implements OpenId Connect and OAuth2 protocols for .Net Core.
With IdentityServer, we can provide protect our SC gRPC services with OAuth 2.0 and JWT tokens. SC Worker will get the token before send request to SC Grpc server application.


### Movies.API
First of all, we are going to develop **Movies.API** project and protect this API resources with **IdentityServer4 OAuth 2.0 implementation**. Generate **JWT Token** with client_credentials from IdentityServer4 and will use this token for securing Movies.API protected resources.

### Movies.MVC
After that, we are going to develop Movies.MVC Asp.Net project for Interactive Client of our application. This Interactive Movies.MVC Client application will be secured with OpenID Connect in IdentityServer4. Our client application pass credentials with logging to an Identity Server and receive back a JSON Web Token (JWT).

### Identity Server
Also, we are going to develop centralized standalone **Authentication Server** and **Identity Provider** with implementing IdentityServer4 package and the name of microservice is Identity Server.
Identity Server4 is an open source framework which implements **OpenId Connect and OAuth2 protocols** for .Net Core.
With Identity Server, we can provide authentication and access control for our web applications or Web APIs from a single point between applications or on a user basis.

### Ocelot API Gateway
Lastly, we are going to develop **Ocelot API Gateway** and make secure protected API resources over the Ocelot API Gateway with transferring **JWT web tokens**.
Once the client has a bearer token it will call the API endpoint which is fronted by Ocelot. Ocelot is working as a reverse proxy.
After Ocelot reroutes the request to the internal API, it will present the token to Identity Server in the **authorization pipeline**. If the client is authorized the request will be processed and a list of movies will be sent back to the client.

Also over these picture, we have also apply the **claim based authentications**.
