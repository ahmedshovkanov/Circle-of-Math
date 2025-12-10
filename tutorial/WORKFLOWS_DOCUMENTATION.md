# ИНТЕГРАЦИЯ CI/CD И СБОРКА ПРОЕКТА:

### Добавить Секреты в Github

### APPLE_CONNECT_EMAIL - почта на которой стор 

### APPLE_DEVELOPER_EMAIL - почта на которой стор

### APPLE_TEAM_ID - айди команды

___IOS_BUNDLE_ID___ - бандл приложения    
___MATCH_PASSWORD___ - любой пароль.  
___MATCH_REPOSITORY___ - название репозитория с сертификатами. Должен выглядеть: User/RepoName.  
___IOS_APP_NAME___ - название приложения.   
___APP_VERSION___ - версия билда.


## APPSTORE_ISSUER_ID, APPSTORE_KEY_ID, APPSTORE_P8:

Необходимо перейти на сайт [Appstore Connect](https://appstoreconnect.apple.com/access/integrations/api) и выбрать Generate API Key.

Добавить в секрет APPSTORE_ISSUER_ID - полученный Issuer ID.

Добавить в секрет APPSTORE_KEY_ID - полученный KEY ID.

## GH_PAT 


