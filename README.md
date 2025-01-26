# LazardTest

Il s'agit d'une api gérant une cantine d'entreprise.<br/>
En l'absence d'une base de données, les données transmises dans l'exercice sont écrites en dure dans les repositories du projet Dal et ne sont donc pas modifiables

## Points d'entrées
Deux points d'entrées
- pour créditer un repas
route: https://localhost:7188/api/clients/credite-compte/{identifiant du client}<br/>
le corps de la requête doit contenir le montant à créditer<br/>
retour: un objet client contenant les informations du client crédité avec son nouveau crédit.

- pour payer un repas
route: https://localhost:7188/api/clients/payer-repas/{identifiant du client}<br/>
le corps de la requête doit contenir la liste des éventuels suppléments<br/>
retour: un objet PayerRepasResponse contenant le détail des suppléments (libellé et prix) et le total du ticket

## Tests
Il y a trois moyens de tester les points d'entrée.
- Avec Visual Studio en lançant les tests de ClientServiceTest.
- Avec le swagger qui se lance quand on lance un débogue depuis Visual Studio
- Avec Postman après avoir lancé l'API

## Exemples
Pour la méthode de crédit du compte, avec le client d'identifiant 100 (route https://localhost:7188/api/clients/credite-compte/100 pour Postman), entrez l'objet de requête
```
{
    "montant": 125
}
```
Vous obtiendrez
```
{
  "clientId": 100,
  "nom": "Fake nom 100",
  "prenom": "Fake prenom 100",
  "profilId": 1,
  "compte": 150.6
}
```

Pour la méthode de crédit du compte, avec le client d'identifiant 1 (route https://localhost:7188/api/clients/credite-compte/1 pour Postman), entrez l'objet de requête
```
{
    "montant": 125
}
```
Vous obtiendrez
```
"An error occured."
```

Pour la méthode de payer un repas, avec le client d'identifiant 100 (route https://localhost:7188/api/clients/payer-repas/100 pour Postman), entrez l'objet de requête
```
{
    "SupplementsRepasId": [8, 9]
}
```
Vous obtiendrez
```
{
    "supplements": [
        {
            "supplementLbl": "Plat supplémentaire",
            "prix": 6
        },
        {
            "supplementLbl": "Dessert supplémentaire",
            "prix": 3
        }
    ],
    "total": 11.5
}
```
Pour la méthode de payer un repas, avec le client d'identifiant 110 (route https://localhost:7188/api/clients/payer-repas/110 pour Postman), entrez l'objet de requête
```
{
    "SupplementsRepasId": [8, 9, 8, 8, 8, 8, 8, 8]
}
```
Vous obtiendrez
```
Le client n'est pas autorisé à avoir un découvert.
```

## Exception
Comme vu dans l'exemple ci-dessus, on peut avoir des exceptions provocant un retour 400 (cas du découvert non autorisé). Dans le cas d'une erreur 500 (dans l'exemple l'identifiant client ne correspond à rien en base), on reçoit un message d'erreur générique. L'erreur est loggée dans un fichier dont l'emplacement est indiqué dans appsettings.json du projet API

## Remarque
Cette API a été codée au plus vite. Elle ne contient donc pas de méthode comme par exemple la modification/ajout/suppression d'un supplément