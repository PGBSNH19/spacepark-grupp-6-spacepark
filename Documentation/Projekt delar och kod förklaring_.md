# Hur delade vi upp våra projekt?

Vi diskuterade och funderade kring hur vi skulle skapa våra Frontend och Backend projekt. 
Sen har vi bestämt att skapa tre projekt med två solutioner:

  - Backend solution har två Projekt (SpaceParkAPI + SpaceParkUnitTest):

    ![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/BE%20Sol.png?raw=true)
    
  - Frontend solution har ett projekt:
 
    ![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20Sol.png?raw=true)


# Förklaring av kod: 
 #### SpaceparkAPI: 
   ![alt BE Sol]( https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/BE%20Files.png?raw=true) 
   
   För vår API projektet valde vi att göra ett .NET Core API projekt som använder Repository mönster samt använde vi oss DTO (Data Transfer Objekt) mönster som har flera fördelar i API projekt som tar bort cirkulära referenser och dölj vissa egenskaper som klienter inte ska visa och mycket mer.
   
   Vi valde att använda Entity Framworks med kod först, så  skapade vi modell klasser som entitets .
   
   Alla metoder som pratar med databasen och med SWAPI API står i Controller (Get och Post metoder..).

Vi använde oss Swapi/Person klass som entity-modell när vi kallar Travellers från Swapi API.

Vi hade två appsettings.json filer: 
-	En vanlig appsetting fil som har moln ConnectionString.
-	En development appsetting fil som har local sql-databas ConnectionString.
#
#
 #### SpaceparkWebApp:
![alt BE Sol]( https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20Files.png?raw=true) 

Vi diskuterade kring hur vi skulle skapa våran Frontend och funderade i början på att skapa Razor Pages därför att vi har en enkel Frontend applikation. Men tyvärr lyckas vi inte att hitta tillräckligt material för att lära oss Razor Pages tekniken och kan man använda dem med Web API’s applikationer. Så vi tog äntligen beslutet att använda ett web applikation med MVC mönster som använder Razor Pages i viewen. 

I Frontend projektet handlade inte direkt med databasen eller med Swapi API.

Vi har skapat alla metoder som skickar http-requests till vår API projekt och får resposes från där i HomeController klass.

 I HomeController har vi två typer av metoder:
 1.	ActionResults som returnerar en View sida i slutet.
 
![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20HomeController1.png?raw=true) 

#
2.	Vanliga Metoder som returnerar ett objekt från våra modell klasser.

![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20HomeController2.png?raw=true) 

#
#
I Models mappen har vi alla View modeller som har ibland mer properties eller mindre än samma modeller som använde oss i vår API projektet. Det berör på hur skulle använda dem i vårt Frontend projekt. 
FrontEnd         |  BackEnd
:-------------------------:|:-------------------------:
![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/BE%20Traveller.png?raw=true)   |  ![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20Traveller.png?raw=true) 
#
#
I View mappen har vi bara två view sidor som skulle visas till användaren när vi kör programmet:
1.	Index sida var vi matar in StarWars traveller namn som skulle parkera sina skeppar i vår parkering. 
![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20Index.png?raw=true) 

#
2.	Details sida var visar vi Traveller namn och sina skeppar infos samt parkeringsstatus för varje skepp. Sen användare kan parkera eller avparkerea skeppen.  
![alt BE Sol](https://github.com/PGBSNH19/spacepark-grupp-6-spacepark/blob/master/Documentation/Bilder/FE%20Details.png?raw=true) 


   










