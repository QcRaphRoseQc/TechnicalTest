# Introduction

Ce test technique permet de contrôler vos connaissances de base en C# et .NET 6, et a été calibré pour durer 1 heure. Pour les plus aventureux, des éléments bonus sont disponibles.

# Présentation du projet

Cette solution repose sur Docker Engine on Linux et Docker Compose pour configurer et exécuter l'application `TechnicalTest.API` ASP.NET Core, ainsi qu'une base de données PostgreSQL. En somme, elle permet la gestion d'événements de santé, sécurité et environnement. De plus, un projet nommé `TechnicalTest.IntegrationTests` a été inclus pour réaliser des tests d'intégration.

# Directives de base

## Ajouter des documents

Vous devez introduire une nouvelle fonctionnalité permettant le dépôt de documents sur un événement existant au travers une API. Les règles d'affaires suivantes doivent être respectées :

- Un événement peut posséder de 0 à n documents;
- Un document doit posséder un champ représentant l'identifiant de sa clé Amazon S3 (https://docs.aws.amazon.com/AmazonS3/latest/userguide/object-keys.html explique les directives de nommage) et une description optionnelle;

## Liste des événements

Dans le fichier `EventsController.cs`, une méthode GET permet de lister l'intégralité des événements présents dans la base de données. Nos usagers aimeraient obtenir de cet API la possibilité de lister des événements par ordre croissant ou décroissant, et filtrer à l'aide des critères suivants :
- déclarés avant une date donnée, où le format de date attendu doit respecter le format ISO 8601;
- possèdent ou non des documents.

## Correction de bugs et tests unitaires

Dans le fichier `EventsController.cs`, une méthode DELETE permet de supprimer un événement spécifique dans la base de données. Cependant, un de vos collègues semble avoir fait plusieurs erreurs dans son code, pouvez-vous le corriger et le couvrir à l'aide de tests ? Voici les règles d'affaires à prendre en compte : il n'est possible de supprimer des événements que s'ils existent, et si leur description est vide.

# Bonus

Nous avons fait l'acquisition d'un nouveau client dans le domaine des mines. Cependant, le nombre d'événements santé, sécurité et environnement déclarés est assez grand chaque jour. Le gestionnaire de santé, sécurité et environnement de ce client nous informe que son entreprise possède un système qui prend en entrée un fichier CSV d'événements pour construire des indicateurs. Il n'a pas les ressources pour modifier ses outils. Néanmoins, il aimerait que nous concevions une fonctionnalité permettant d'envoyer automatiquement, chaque jour à 2h du matin, un rapport journalier (fichier CSV) à une adresse courriel fixe, contenant tous les événements déclarés depuis le dernier envoi. L'équipe OPS vous précise que, si possible, cette fonctionnalité devrait être une tâche indépendante de l'application Web.