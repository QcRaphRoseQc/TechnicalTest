# TechnicalTestConformit

Bonjour, merci beaucoup de m'avoir donné l'opposition de faire le test technique, ça m’a pris un peu de temps, je n'avais pas travaillé en C# et ASP.net depuis plusieurs mois hier j'ai été fouiller dans mes anciens projets pour me remettre dedans avant de commencer l'examen, j'ai réussi à tout accomplir les tâches, et même la question bonus :)

J'ai ajouté quelques routes pour pouvoir tester et pour la question bonus, j'ai utilisé Quartz un task scheduler pour .Net https:/www.quartz-schedulernet/
Merci beaucoup de prendre le temps de regarder tout ça ! ! !

Base Route : api/v1/

|ROUTE | ACTION | DESCRIPTION |
|------|--------|-------------|
| Documents                | GET        | Consultation des films (sans critiques et sans acteurs)   |
| Documents                | POST       | Consultation de tous les acteurs d’un certain film        |
| Events                   | GET        | Consultation d’un certain film avec ces critiques         |
| Events/filter            | GET        | Recherche de films                                        |
| Events/id                | DELETE     | Ajout d’un film (seulement si admin)                      |
| User                     | GET        | Suppression d’un film (seulement si admin)                |
| User                     | POST       | Ajout d’un nouveau user                                   |
