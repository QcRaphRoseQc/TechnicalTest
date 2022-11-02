# TechnicalTestConformit

Bonjour, merci beaucoup de m'avoir donné l'opposition de faire le test technique, ça m’a pris un peu de temps, je n'avais pas travaillé en C# et ASP.net depuis plusieurs mois hier j'ai été fouiller dans mes anciens projets pour me remettre dedans avant de commencer l'examen, j'ai réussi à tout accomplir les tâches, et même la question bonus :)

J'ai ajouté quelques routes pour pouvoir tester et pour la question bonus, j'ai utilisé Quartz un task scheduler pour .Net (www.quartz-schedulernet.net)

Merci beaucoup de prendre le temps de regarder tout ça ! ! !

------------------------------------------------------------------------------------

Pour Tester:

Dans Docker Cli Postgres
<br/>
<br/>

`INSERT INTO "Users" ("Id", "FirstName", "LastName", "HireDate") VALUES ('c0a80163-7b48-4d36-9bd7-2b0d7b3dcb6d', 'John', 'Doe', '2021-05-01 00:00:00.0000000+00:00');`
<br/>
<br/>



`INSERT INTO "Events" ("Description", "DeclarationDateTime", "DeclaredById") VALUES ('Test', '2020-11-02 00:00:00.0000000+00:00', 'c0a80163-7b48-4d36-9bd7-2b0d7b3dcb6d');`
<br/>
<br/>

`INSERT INTO "Documents" ("S3Key", "Description", "EventId") VALUES ('Test', 'Test', 2);`

Base Route : api/v1/

|ROUTE | ACTION | DESCRIPTION |
|------|--------|-------------|
| Documents                | GET        | Consultation des documents                                |
| Documents                | POST       | Ajout d'un documents                                      |
| Events                   | GET        | Consultation des Events                                   |
| Events/filter            | GET        | Consultation des Events avec filtre                       |
| Events/id                | DELETE     | Suppression d'un Event                                    |
| User                     | GET        | Consultation des users                                    |
| User                     | POST       | Ajout d’un nouveau user                                   |
