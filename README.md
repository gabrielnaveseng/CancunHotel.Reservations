# This is my solution to the proposed problem.

This code is a simple .Net 6 solution that uses hexagonal architecture (Alistair Cockburn) and Clean Architecture (Uncle Bob)
All the Core Interfaces are in the folder ports (In or Out).
Use cases are modeled oriented to CQRS (Greg Young).

There is unit tests for the Core Project of the application.
There is architecture unit tests for all projects of the application.

# How to run?

* Install .Net 6 and docker (or use IIS Express)
* Run all docker comps inside the \Dependencies folder.
* Create a database called Reservations
* Configure Sql Server to receive requests from container (or use IIS)
* Run sql in migration folder.
* If you want to use IIS, just run the application using Visual Studio.
* If you want to use docker, you need to configure the sql server to receive request from the container

# How to achieve 99.99% availability?

* Deploy the web app to a Kubernetes SAAS as Azure Container Apps. The SLA is (99.95%)
	* Make your deployment redundant across different zones of your cloud.
	* This means: create replicas of your container in different cloud regions (Datacenters)
	* Using 'N' different regions of Azure Container App and a Load balancer you can improve your SLA.
* Create your SQL Server in the Cloud.
	* Create redundant zone backup plans
	* Configure your sql server to be zone redundant (99.995% availability SLA)
	* https://techcommunity.microsoft.com/t5/azure-sql-blog/zone-redundancy-for-azure-sql-database-general-purpose-tier/ba-p/3280376

* Example: for 3 regions and sql server distributed: (1-(0,05*0,05*0,05))*0,9995 = 0.9993750625 SLA

* You can also make your architecture MultiCloud, but it's not necessary.
	* To do this you will need an Api Gateway or a Load Balancer to route the request between the different clouds.

* To deploy this application, use the Blue-Green strategy.

* There is a HLD in this repo that explain this idea (HLD.drawio)