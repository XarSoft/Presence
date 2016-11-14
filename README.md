# Presence

Ce projet est un exemple d’implémentation de l'API Pushover.
Le projet contient un service windows (SoftFluent.Presence.Host) qui host un service à l'adresse "http://localhost:8092/PresenceService"
Lorsque l'on appelle l'URL "localhost:8092/PresenceService/set/AtWork" une notification est envoyée via l'API Pushover pour signaler qu'on arrive à un endroit, en l'occurrence ici sur le lieu du travail.
Lorsque l'on appelle l'URL "localhost:8092/PresenceService/remove/AtWork" une notification est envoyée via l'API Pushover pour signaler que l’on quitte un lieu.

Le web service est prévu pour fonctionner avec le service Maker d’IFTTT. L’idée est de le coupler avec la géolocalisation de façon à envoyer une requête Web au service Windows qui va appeler l’API Pushover dès que l’on va rentrer ou sortir d’une zone.
L’intérêt de ne pas passer directement de la géolocalisation d’IFTTT à Pushover directement via IFTTT, c’est que l’on peut mettre une intelligence entre les deux. 
Par exemple :
Lorsque personne1 ET personne2 ne sont pas présent à la maison alors on active l’alarme et on envoie une notification.
Si personne1 OU personne2 arrive à la maison alors on arrête l’alarme et on envoie une notification.

Le service Windows doit être mit dans le répertoire «C:\Program Files (x86)\SoftFluent\Presence\ »
Pour installer le service Windows il faut lancer « Install.bat » si vous voulez spécifier un compte particulier ou lancer « InstallLocalSystem.bat » pour installer le service Windows avec le compte local system. 
Il ne reste plus qu’à démarrer le service manuellement après l’installation.
Avant de déployer une nouvelle version du service Windows penser bien à le désinstaller avant avec « UnInstall.bat »
 
