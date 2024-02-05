# 3ASPNET Project
## Installer et lancer le projet

Cloner le projet
```
git clone git@github.com:ChapelleNathan/3ASPNET.git
```

Lancer lancer le projet
```
dotnet watch run
```

Ajouter une migration
```
dotnet ef migrations add [nom de la migration]
```

Appliquer les migrations à la db
```
dotnet ef database update
```
