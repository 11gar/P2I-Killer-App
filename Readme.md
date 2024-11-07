# P2I-KILLER-APP

Ceci est une description blah blah blahhh.
Site très mignon mais pas top niveau sécurité.

## Requis

Il faut installer docker et docker-compose.
Si jamais tu peux pas l'utiliser, développe comme tu développais avant (pas de problème).


## Lancement

Pour lancer le mode développement
```bash
$ sudo docker compose -f docker-compose-dev.yml up
```

Pour lancer le mode production
```bash
$ sudo URL_BACKEND=http://localhost:8081 docker compose -f docker-compose.yml up
```


## TO DO

- Checker que aucun mot de passe ne sort par des routes
- Forcer l'utilisation de mot de passe avec une longueur minimum de 8 (sinon bruteforce facile)

- GROS POINT: Vérifier que l'utilisateur en question à le droit de faire ce qu'il fait

```c#
[Authorize] // Cette partie indique que la connexion doit être identifié (JWT)
//Toutes les connexions doivent être authentifier sauf login, register...
[HttpGet]
public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
{
    //tu peux récupérer l'id de l'utilisateur qui se connecte
    //c'est une donnée de confiance
    //contrairement à des données récupérés depuis GET/POST qui peuvent être modifié facilement
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //...
}
```
Donc si une personne récupére sa cible par exemple, il faut se baser sur le userId récupéré comme celui-ci.  
C'est le plus gros travail à faire sur le site.  
Donc vérifier chaque route une à une.  


