# Uppgiften 
Uppgiften är att skapa en omräknare av imperiska enheter. 
De enheter som skall översättas är: 
* Thou (th) 
* Inch (in) 1000 thous 
* Foot (ft) 12 inches 
* Yard (yd) 3 feet 
*Furlong (fur) 220 yards 


Användaren matar in en sträng i formatet: [antal] [frånEnhet] in [tillEnhet]  
Programmet visar resultatet i decimal form.  
Exempel:  
3 foot in yd => 1  
25 th in Inch => 0,025  
# Krav 
* Koden skall skrivas i ett CLI-projekt  
* Inmatningssträngen skall valideras  
* Felhantering skall implementeras på lämpligt, användarvänligt sätt. Dock behöver inga loggfiler eller dylikt  skapas.  
* Man skall kunna göra flera beräkningar under programmets exekvering   
* Det skall presenteras för användaren hur inmatningen skall se ut och vilka enheter som kan användas 
* Användaren skall kunna välja mellan förkortning och fullt namn på enheterna  
* Koden skall vara välstrukturerad, uppdelad i flera klasser och lätt utbyggbar med fler imperiska enheter 
* Förbered koden för att kunna hantera vikter, som är minst lika krångliga, parallellt med längder. Du behöver dock inte implementera några vikter. 
* Skriv koden som om den är grunden till ett större system, dvs om det kommer en massa annan kod och logik runt, hur skulle du strukturera det då? 
