# HealthcareSystem

## Our members

| DevOps               | Web App          | Integration Adapters | Graphical Editor  |
| -------------------- | ---------------- | -------------------- | ----------------- | 
| Ana Perišić          | Jelena Budiša    | Borislav Puzić   	 | Ognjen Kuzmanović |
| Milana Todorović     | Dragana Čarapić  | Stefan Kitanović 	 | Vasilije Bursać   |
| Nemanja Pualić       | Slađana Savković | Dionizij Malačko 	 | Milena Kovačević  |
| Vladislav Maksimović | Jelena Zeljko    | Darijan Mićić    	 | Marko Đurišić     |


## Running the project locally
<details>
  <summary><b>PatientWebApp</b> </summary> 
<br>  
<i>Necessary components:</i> PatientWebApp, PatientService, database (MySQL for Development environment, Postgres for Test environment)<br><br>
  
Quick launch methods are currently provided for the Test environment (both will run the PatientWebApp on port 8181):
- <b>PowerShell scripts</b><br>
  Scripts for launch (<i>pwa-up.ps1</i>) and shutdown (<i>pwa-down.ps1</i>) are in the repository root.
  - <i>Prerequisites:</i>  PowerShell, Docker, Docker Compose, .NET Core CLI, free port 8181 (or edit Compose file locally to change port)
  - <i>Usage</i>:  <br><code>.\pwa-up [-noDbBuild] [-noServiceBuild]</code>
  <br>Use the <code>-noDbBuild</code> flag if the Docker image for the initialized database has already been built and there have been no changes to the data model. It's recommended to use this whenever possible since building the database image slows down the launch significantly. 
  <br>Use the <code>-noServiceBuild</code> flag if the service Docker images have already been built and there have been no changes.
  <br><code>.\pwa-down [-rmi]</code>
  <br>Use the <code>-rmi</code> flag to remove the service Docker images during shutdown.
- <b>Docker Compose file</b> (<i>HealthcareSystem/docker-compose.pwa.yaml</i>)
  <br>If the PowerShell scripts are inconvenient, use the Compose file directly.
  - <i>Prerequisites:</i> Docker, Docker Compose, free port 8181 (or edit Compose file locally to change port)
  - <i>Usage</i>: See the PowerShell scripts for an example of usage. In order for the Compose file to work correctly, the solution needs to be published with the Release configuration, and the Docker image for the database (<i>HealthcareSystem/Dockerfile.postgre</i>) with needs to be built separately with the name <i>seeded-database</i>.
</details> 

## Help
Aditional help find [here](https://github.com/PSW-2020-ORG4/Organization-Help)

