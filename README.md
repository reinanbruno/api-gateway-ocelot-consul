# API Gateway example using Ocelot + Consul in .NET 5

### :camera: Project images:

<table>
  <tr>
    <td valign="top"><img src="https://github.com/reinanbruno/api-gateway-ocelot-consul/blob/master/images/diagram.png"/></td>
  </tr>
  <tr>
    <td valign="top"><img src="https://github.com/reinanbruno/api-gateway-ocelot-consul/blob/master/images/consul.png"/></td>
  </tr>
  <tr>
    <td valign="top"><img src="https://github.com/reinanbruno/api-gateway-ocelot-consul/blob/master/images/swagger_api_gateway.png"/></td>
  </tr>
</table>

### :computer: How to run the application?

```bash
# Before executing the commands below you will need the docker installed on your machine

$ cd src
$ docker-compose up -d
```

### What does each one do?
- Consul (In consul, the services in this example are ServiceA and ServiceB are registered through the configuration of appsettings.json for each project)
- Api Gateway (Communicates directly with services through the name of the service registered at the consul)
- Api Service A and B (The services are registered at the consul and have a HealthCheck to analyze if the service is running in the configured time interval)

### Each project will be executed in its url:
- Consul(http://localhost:8500)
- Api Gateway(http://localhost:5100/swagger)
- Api Service A(http://localhost:5101/swagger)
- Api Service B(http://localhost:5102/swagger)